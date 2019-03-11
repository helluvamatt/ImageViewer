using ImageViewer.Data;
using ImageViewer.Data.Models;
using log4net;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using R = ImageViewer.Properties.Resources;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer.Models
{
    // TODO Investigate using ImageMagick.NET to handle image reading (and possibly manipulation)
    // TODO SVG support (IMAGE_EXTS, LoadImage, etc...)
    internal class ImageBrowser
    {
        private static readonly string[] IMAGE_EXTS = new string[] { "bmp","gif", "jpg", "jpeg", "jpe", "jif", "jfif", "jfi", "png", "tiff", "tif" };

        private readonly FileWatcherManager _WatcherManager;
        private readonly SynchronizationContext _SyncContext;
        private readonly ImageDatabase _ImageDatabase;

        private readonly ILog _Logger;
        private readonly ILog _ScannerLog;

        private bool _FullScan;

        public ImageBrowser(string appPath)
        {
            _Logger = LogManager.GetLogger(GetType());
            _ScannerLog = LogManager.GetLogger("Scanner");

            _WatcherManager = new FileWatcherManager(p => IMAGE_EXTS.Any(ext => Path.GetExtension(p).ToLower() == "." + ext), Settings.Default.LibraryAutoScan);
            _SyncContext = SynchronizationContext.Current;
            _ImageDatabase = new ImageDatabase(Path.Combine(appPath, "Images.db"), TraceSQL);
            
            _WatcherManager.IsScanningChanged += OnIsScanningChanged;
            _WatcherManager.Error += OnError;
            _WatcherManager.FileAdded += OnFileAdded;
            _WatcherManager.FileChanged += OnFileChanged;
            _WatcherManager.FileDeleted += OnFileDeleted;

            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
            _FullScan = Settings.Default.LibraryFullScan;

            _WatcherManager.StartWatching();
        }

        #region Error event

        public event EventHandler<ComponentErrorEventArgs> Error;

        private void OnError(string message, Exception ex, string component) => _SyncContext.Post(state => Error?.Invoke(this, new ComponentErrorEventArgs(message, ex, component)), null);

        private void OnError(object sender, ErrorEventArgs e)
        {
            var ex = e.GetException();
            OnError(ex.Message, ex, "Image Scanner");
        }

        #endregion

        #region IsScanning property

        public bool IsScanning => _WatcherManager.IsScanning;

        public string CurrentScanPath => _WatcherManager.CurrentScanPath;

        public event EventHandler IsScanningChanged;

        private void OnIsScanningChanged(object sender, EventArgs e) => _SyncContext.Post(state => IsScanningChanged?.Invoke(this, EventArgs.Empty), null);

        #endregion

        #region Image events

        public event EventHandler<ImageEventArgs> ImageAdded;

        private void OnImageAdded(ImageModel image)
        {
            _SyncContext.Post(state =>
            {
                if (state is ImageModel img) ImageAdded?.Invoke(this, new ImageEventArgs(img));
            }, image);
        }

        public event EventHandler<ImageEventArgs> ImageChanged;

        private void OnImageChanged(ImageModel image)
        {
            _SyncContext.Post(state =>
            {
                if (state is ImageModel img) ImageChanged?.Invoke(this, new ImageEventArgs(img));
            }, image);
        }

        public event EventHandler<ImageRemovedEventArgs> ImageRemoved;

        private void OnImageRemoved(ImageModel image, string path)
        {
            _SyncContext.Post(state =>
            {
                ImageRemoved?.Invoke(this, (ImageRemovedEventArgs)state);
            }, new ImageRemovedEventArgs(image, path));
        }

        #endregion

        public event EventHandler<TagEventArgs> TagChanged;

        public event EventHandler DatabaseReset;

        public ImageLoadResult LoadImage(ImageModel model, bool thumbnail)
        {
            Image image = null;
            try
            {
                if (thumbnail)
                {
                    image = LoadImageThumbnail(model);
                }
                if (image == null)
                {
                    image = Image.FromFile(model.FilePath);
                }

                var rot = RotateFlipType.RotateNoneFlipNone;
                var metadata = ImageMetadataReader.ReadMetadata(model.FilePath);
                var ifd0 = metadata.OfType<ExifIfd0Directory>().FirstOrDefault();
                if (ifd0 != null && ifd0.TryGetUInt16(ExifDirectoryBase.TagOrientation, out ushort orientation))
                {
                    if (orientation == 3 || orientation == 4)
                        rot = RotateFlipType.Rotate180FlipNone;
                    else if (orientation == 5 || orientation == 6)
                        rot = RotateFlipType.Rotate90FlipNone;
                    else if (orientation == 7 || orientation == 8)
                        rot = RotateFlipType.Rotate270FlipNone;

                    if (orientation == 2 || orientation == 4 || orientation == 5 || orientation == 7)
                        rot |= RotateFlipType.RotateNoneFlipX;
                }
                if (rot != RotateFlipType.RotateNoneFlipNone)
                {
                    image.RotateFlip(rot);
                    var rotated = new Bitmap(image.Width, image.Height);
                    using (var g = Graphics.FromImage(rotated))
                    {
                        g.Clear(Color.Transparent);
                        g.DrawImage(image, Point.Empty);
                    }
                    image.Dispose();
                    image = rotated;
                }

                string format;
                using (var stream = File.OpenRead(model.FilePath)) format = MetadataExtractor.Util.FileTypeDetector.DetectFileType(stream).ToString().ToLower();

                return new ImageLoadResult(image, metadata, format);
            }
            catch (Exception ex)
            {
                OnError($"Failed to load image \"{model.FilePath}\": {ex.Message}", ex, "Image Loader");
                return new ImageLoadResult(ex.Message);
            }
        }

        public Image LoadImageThumbnail(ImageModel model)
        {
            byte[] thumbnailData = null;
            using (var stream = new FileStream(model.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var img = Image.FromStream(stream, false, false))
                {
                    if (img.PropertyIdList.Contains(0x501B)) // Thumbnail metadata
                    {
                        var p = img.GetPropertyItem(0x501B);
                        thumbnailData = p.Value;
                    }
                }
            }
            if (thumbnailData != null)
            {
                using (var thumbnailStream = new MemoryStream(thumbnailData))
                {
                    return Image.FromStream(thumbnailStream);
                }
            }
            return null;
        }

        #region Root paths
        public bool AddPath(string path) => _WatcherManager.AddPath(path);
        public bool RemovePath(string path) => _WatcherManager.RemovePath(path);
        public IEnumerable<string> LibraryPaths => _WatcherManager.Paths;
        public event EventHandler<PathEventArgs> LibraryPathAdded
        {
            add => _WatcherManager.PathAdded += value;
            remove => _WatcherManager.PathAdded -= value;
        }
        public event EventHandler<PathEventArgs> LibraryPathRemoved
        {
            add => _WatcherManager.PathRemoved += value;
            remove => _WatcherManager.PathRemoved -= value;
        }
        public string GetRoot(string path) => _WatcherManager.GetRoot(path);
        #endregion

        #region Database calls
        
        public void SaveImage(ImageModel img)
        {
            _ImageDatabase.SaveImage(img);
            ImageChanged?.Invoke(this, new ImageEventArgs(img));
        }

        public void RemoveImage(ImageModel img) => _ImageDatabase.DeleteImage(img);
        public int RemoveImagesRecursive(string folderPath) => _ImageDatabase.DeleteImagesRecursive(folderPath);
        public int RemoveDeletedImages() => _ImageDatabase.RemoveDeletedImages();

        public int CountImagesWithTag(TagModel tag) => _ImageDatabase.CountImagesWithTag(tag);
        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag) => _ImageDatabase.GetImagesWithTag(tag);
        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag, int limit) => _ImageDatabase.GetImagesWithTag(tag, limit);

        public int CountImagesInFolder(string path) => _ImageDatabase.CountImagesInFolder(path);
        public IEnumerable<ImageModel> GetImagesInFolder(string path) => _ImageDatabase.GetImagesInFolder(path);
        public IEnumerable<ImageModel> GetImagesInFolder(string path, int limit) => _ImageDatabase.GetImagesInFolder(path, limit);

        public IEnumerable<string> GetFolders(string path) => _ImageDatabase.GetFolders(path);
        public int CountFolders(string path) => _ImageDatabase.CountFolders(path);

        public DateTime GetFolderLastModified(string path) => _ImageDatabase.GetFolderLastModified(path);

        public IEnumerable<TagModel> GetTags() => _ImageDatabase.GetTags();
        public IEnumerable<TagModel> GetTagsForAutoComplete(string startsWith) => _ImageDatabase.GetTagsForAutoComplete(startsWith);

        public void RemoveTag(TagModel tag)
        {
            _ImageDatabase.RemoveTag(tag);
            TagChanged?.Invoke(this, new TagEventArgs(tag));
        }

        public void SaveTag(TagModel tag)
        {
            _ImageDatabase.SaveTag(tag);
            TagChanged?.Invoke(this, new TagEventArgs(tag));
        }

        public bool GetImageHasTag(ImageModel image, TagModel tag) => _ImageDatabase.GetImageHasTag(image, tag);
        public void GetImageTags(ImageModel model) => _ImageDatabase.GetImageTags(model);

        public void AddImageTag(ImageModel image, TagModel tag)
        {
            _ImageDatabase.AddTagImage(image, tag);
            ImageChanged?.Invoke(this, new ImageEventArgs(image));
        }

        public void RemoveImageTag(ImageModel image, TagModel tag)
        {
            _ImageDatabase.RemoveTagImage(image, tag);
            ImageChanged?.Invoke(this, new ImageEventArgs(image));
        }

        #endregion

        public void Rescan() => _WatcherManager.Rescan();

        public void Close() => _WatcherManager.Close();

        public void ResetDatabase()
        {
            // Danger zone: This will stop any current scan, and then reset the database. This is an asynchronous process that will fire deletion events for all images.
            _ScannerLog.Info("User requested database reset.");
            Task.Run(() =>
            {
                // Stop any running scan
                _WatcherManager.StopScanNow();
                
                // Reset the database proper
                _ImageDatabase.ResetDatabase();

                // Notify listeners that the database is reset
                _SyncContext.Post(state => DatabaseReset?.Invoke(this, EventArgs.Empty), null);

            });
        }

        #region Event handlers

        private void OnFileAdded(object sender, FileSystemEventArgs e)
        {
            _ScannerLog.InfoFormat("Image added: {0}", e.FullPath);
            var img = _ImageDatabase.GetImageByPath(e.FullPath);
            bool isNew = img == null;
            if (isNew) img = new ImageModel(e.FullPath);
            else if (img.IsDeleted) return;
            try
            {
                ScanImage(img);
                _ImageDatabase.SaveImage(img);
            }
            catch (Exception ex)
            {
                var msg = $"Failed to scan image \"{e.FullPath}\": {ex.Message}";
                _ScannerLog.Error(msg, ex);
                OnError(msg, ex, "Image Scanner");
                return;
            }
            if (isNew) OnImageAdded(img);
            else OnImageChanged(img);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            _ScannerLog.InfoFormat("Image changed: {0}", e.FullPath);
            var img = _ImageDatabase.GetImageByPath(e.FullPath);
            if (img == null) img = new ImageModel(e.FullPath);
            else if (img.IsDeleted) return;
            try
            {
                ScanImage(img);
                _ImageDatabase.SaveImage(img);
            }
            catch (Exception ex)
            {
                var msg = $"Failed to scan image \"{e.FullPath}\": {ex.Message}";
                _ScannerLog.Error(msg, ex);
                OnError(msg, ex, "Image Scanner");
                return;
            }
            OnImageChanged(img);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            _ScannerLog.InfoFormat("Image deleted: {0}", e.FullPath);
            var oldImage = _ImageDatabase.DeleteImage(e.FullPath);
            OnImageRemoved(oldImage, e.FullPath);
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.LibraryAutoScan) == e.PropertyName)
            {
                _WatcherManager.WatchFolders = Settings.Default.LibraryAutoScan;
            }
            else if (nameof(Settings.LibraryFullScan) == e.PropertyName)
            {
                _FullScan = Settings.Default.LibraryFullScan;
            }
        }

        #endregion

        private void ScanImage(ImageModel model)
        {
            var fi = new FileInfo(model.FilePath);
            if (model.FileSize != fi.Length)
            {
                model.FileSize = fi.Length;
                model.FileCreatedDate = fi.CreationTime;
                model.FileModifiedDate = fi.LastWriteTime;
                model.Hash = null;
            }
            string hash;
            using (SHA256 sha = SHA256.Create())
            {
                using (var stream = fi.OpenRead())
                {
                    hash = sha.ComputeHash(stream).ToHexString();
                }
            }
            if (hash != model.Hash || _FullScan)
            {
                model.Hash = hash;
                using (var result = LoadImage(model, false))
                {
                    if (string.IsNullOrEmpty(result.Error))
                    {
                        model.Width = result.Image.Width;
                        model.Height = result.Image.Height;
                        model.BitsPerPixel = Image.GetPixelFormatSize(result.Image.PixelFormat);
                        model.Format = result.Format ?? R.Unknown;
                    }
                    else
                    {
                        model.IsDeleted = true;
                    }
                }
            }
        }

        private void TraceSQL(string sql) => _Logger.DebugFormat("SQL: {0}", sql);
    }

    internal enum Sort
    {
        [DescriptionRes(nameof(R.Sort_Name))]
        Name,

        [DescriptionRes(nameof(R.Sort_ModifiedDate))]
        ModifiedDate,

        [DescriptionRes(nameof(R.Sort_CreatedDate))]
        CreatedDate,

        [DescriptionRes(nameof(R.Sort_FileSize))]
        FileSize,
    }

    internal class ImageLoadResult : IDisposable
    {
        public ImageLoadResult(string error)
        {
            Error = error;
        }

        public ImageLoadResult(Image image, IReadOnlyList<MetadataExtractor.Directory> metadata, string format)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Metadata = metadata;
            Format = format;
        }

        public string Error { get; }

        public Image Image { get; }

        public IReadOnlyList<MetadataExtractor.Directory> Metadata { get; }

        public string Format { get; }

        public void Dispose()
        {
            Image?.Dispose();
        }
    }

    internal static class DataUtils
    {
        public static string ToHexString(this byte[] data)
        {
            if (data == null) return null;
            return string.Join("", data.Select(b => $"{b:X2}"));
        }
    }
}
