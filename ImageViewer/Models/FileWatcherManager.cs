using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer.Models
{
    internal class FileWatcherManager
    {
        private readonly Dictionary<string, FileSystemWatcher> _Watchers;
        private readonly HashSet<string> _CurrentFiles;
        private readonly Func<string, bool> _FilterCallback;
        private readonly ConcurrentQueue<string> _PathScanQueue;
        private readonly Thread _ScannerThread;
        private readonly ManualResetEventSlim _ScannerTrigger;
        private readonly ManualResetEventSlim _ScannerDoneTrigger;

        private readonly ILog _ScannerLog;

        private bool _Watching;

        public FileWatcherManager(Func<string, bool> filterCallback, bool watchFolders)
        {
            _Watchers = new Dictionary<string, FileSystemWatcher>();
            _CurrentFiles = new HashSet<string>();
            _FilterCallback = filterCallback;
            _PathScanQueue = new ConcurrentQueue<string>();
            _WatchFolders = watchFolders;
            _ScannerLog = LogManager.GetLogger("Scanner");
            _ScannerTrigger = new ManualResetEventSlim();
            _ScannerDoneTrigger = new ManualResetEventSlim(true);
            _ScannerThread = new Thread(Scan)
            {
                IsBackground = false,
                Name = "Library Scanner"
            };
            _ScannerThread.Start();
        }

        public void StartWatching()
        {
            bool savePaths = false;
            foreach (var path in Settings.Default.LibraryPaths)
            {
                if (!AddPathCore(path)) savePaths = true;
            }
            if (savePaths) SavePaths();
        }

        #region Events

        public event ErrorEventHandler Error;

        private void OnError(Exception ex)
        {
            Error?.Invoke(this, new ErrorEventArgs(ex));
        }

        public event FileSystemEventHandler FileAdded;

        private void OnFileAdded(string path)
        {
            FileAdded?.Invoke(this, new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetDirectoryName(path), Path.GetFileName(path)));
        }

        public event FileSystemEventHandler FileDeleted;

        private void OnFileDeleted(string path)
        {
            FileDeleted?.Invoke(this, new FileSystemEventArgs(WatcherChangeTypes.Deleted, Path.GetDirectoryName(path), Path.GetFileName(path)));
        }

        public event FileSystemEventHandler FileChanged;

        private void OnFileChanged(string path)
        {
            FileChanged?.Invoke(this, new FileSystemEventArgs(WatcherChangeTypes.Changed, Path.GetDirectoryName(path), Path.GetFileName(path)));
        }

        #endregion

        #region WatchFolders property

        private bool _WatchFolders;
        public bool WatchFolders
        {
            get => _WatchFolders;
            set
            {
                if (_WatchFolders != value)
                {
                    _WatchFolders = value;
                    OnWatchFoldersChanged();
                }
            }
        }

        private void OnWatchFoldersChanged()
        {
            foreach (var kvp in _Watchers)
            {
                kvp.Value.EnableRaisingEvents = WatchFolders;
            }
            if (WatchFolders) Rescan();
        }

        #endregion

        #region IsScanning property

        private bool _IsScanning;
        public bool IsScanning
        {
            get => _IsScanning;
            set
            {
                if (_IsScanning != value)
                {
                    _IsScanning = value;
                    IsScanningChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private string _CurrentScanPath;
        public string CurrentScanPath
        {
            get => _CurrentScanPath;
            private set
            {
                if (_CurrentScanPath != value)
                {
                    _CurrentScanPath = value;
                    IsScanningChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsScanningChanged;

        #endregion

        public bool AddPath(string path)
        {
            var result = AddPathCore(path);
            if (result) SavePaths();
            return result;
        }

        private bool AddPathCore(string path)
        {
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var pathTest = path.ToLower();

            // Ensure this path isn't already tracked
            if (_Watchers.ContainsKey(pathTest))
            {
                return false;
            }

            // Ensure no current root paths are parents of this new path
            foreach (var kvp in _Watchers)
            {
                if (pathTest.StartsWith(kvp.Key + Path.DirectorySeparatorChar))
                {
                    return false;
                }
            }

            _ScannerLog.InfoFormat("Watching directory: {0}", path);
            var watcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size,
                IncludeSubdirectories = true
            };
            watcher.Changed += OnFileChanged;
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
            watcher.Error += OnWatcherError;
            watcher.EnableRaisingEvents = _WatchFolders;
            _Watchers.Add(pathTest, watcher);
            PathAdded?.Invoke(this, new PathEventArgs(path));
            QueueNodeAdded(path);

            return true;
        }

        public bool RemovePath(string path)
        {
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var pathTest = path.ToLower();
            if (!_Watchers.ContainsKey(pathTest)) return false;
            
            _ScannerLog.InfoFormat("No longer watching directory: {0}", path);
            _Watchers[pathTest].Dispose();
            _Watchers.Remove(pathTest);
            PathRemoved?.Invoke(this, new PathEventArgs(path));
            QueueNodeRemoved(path);
            return true;
        }
        
        public IEnumerable<string> Paths => _Watchers.Select(w => w.Value.Path);

        public event EventHandler<PathEventArgs> PathAdded;
        public event EventHandler<PathEventArgs> PathRemoved;

        public string GetRoot(string path)
        {
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var root = _Watchers.FirstOrDefault(kvp => path.ToLower().StartsWith(kvp.Key + Path.DirectorySeparatorChar));
            if (root.Value != null) return root.Value.Path;
            return null;
        }

        public void Rescan()
        {
            _CurrentFiles.Clear();
            foreach (var watcher in _Watchers)
            {
                _PathScanQueue.Enqueue(watcher.Value.Path);
            }
            _ScannerTrigger.Set();
        }

        public void StopScanNow()
        {
            // Tell the scanner to stop, releasing it to check the queue if necessary
            _ScannerDoneTrigger.Reset();
            _ScannerTrigger.Set();

            // Wait for it to stop
            _ScannerDoneTrigger.Wait();

            // Dump the queue
            while (_PathScanQueue.TryDequeue(out string result)) { }

            // Finally, clear known paths. Since we cleared the queue, any new scans will need to scan full again.
            _CurrentFiles.Clear();
        }

        public void Close()
        {
            // Notify the thread that we are closing
            _Watching = false;

            // Allow the thread to proceed if necessary
            _ScannerTrigger.Set();
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            QueueNodeRemoved(e.OldFullPath);
            QueueNodeAdded(e.FullPath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            QueueNodeRemoved(e.FullPath);
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            QueueNodeAdded(e.FullPath);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (_CurrentFiles.Contains(e.FullPath))
            {
                OnFileChanged(e.FullPath);
            }
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            var watcher = (FileSystemWatcher)sender;
            var ex = e.GetException();
            _ScannerLog.Error(string.Format("Error in watcher for {0}", watcher.Path), ex);
            OnError(ex);
        }

        private void QueueNodeAdded(string path)
        {
            _PathScanQueue.Enqueue(path);
            _ScannerTrigger.Set();
        }

        private void QueueNodeRemoved(string path)
        {
            foreach (var file in _CurrentFiles.Where(p => p.StartsWith(path)))
            {
                _ScannerLog.InfoFormat("File removed: {0}", file);
                OnFileDeleted(file);
                _CurrentFiles.Remove(file);
            }
        }

        private void SavePaths()
        {
            var coll = new StringCollection();
            coll.AddRange(Paths.ToArray());
            Settings.Default.LibraryPaths = coll;
        }

        #region Async scanning methods

        private void Scan()
        {
            _Watching = true;
            while (_Watching)
            {
                _ScannerTrigger.Wait();

                // If we are closing or we have been request to stop scanning, then do so
                if (!_Watching || !_ScannerDoneTrigger.IsSet) break;

                IsScanning = true;
                while (_PathScanQueue.TryDequeue(out string path))
                {
                    // If we are closing or we have been request to stop scanning, then do so
                    if (!_Watching || !_ScannerDoneTrigger.IsSet) break;

                    _ScannerLog.InfoFormat("Scanning path: {0}", path);
                    CurrentScanPath = Path.GetDirectoryName(path);
                    try
                    {
                        if (Directory.Exists(path))
                        {
                            // Recurse into directories
                            foreach (var subPath in Directory.EnumerateFiles(path)) _PathScanQueue.Enqueue(subPath);
                            foreach (var subPath in Directory.EnumerateDirectories(path)) _PathScanQueue.Enqueue(subPath);
                        }
                        else if (File.Exists(path) && (_FilterCallback == null || _FilterCallback.Invoke(path)))
                        {
                            // If we haven't already scanned this file, add it and notify
                            if (_CurrentFiles.Add(path))
                            {
                                _ScannerLog.InfoFormat("New file found: {0}", path);
                                OnFileAdded(path);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _ScannerLog.Error("Error during scan.", ex);
                        OnError(ex);
                    }
                }
                IsScanning = false;
                _ScannerTrigger.Reset();

                // If a thread is waiting on the scan to finish, signal now
                _ScannerDoneTrigger.Set();
            }
        }

        #endregion

    }
}
