using PixelStudio.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelStudio.Models
{
    internal class ImageCache
    {
        private readonly ConcurrentDictionary<string, Image> _Cache;
        private readonly ConcurrentQueue<ImageLoadTask> _ImageLoadQueue;
        private readonly SynchronizationContext _SyncContext;
        private readonly ManualResetEventSlim _LoadTrigger;

        public ImageCache()
        {
            _Cache = new ConcurrentDictionary<string, Image>();
            _ImageLoadQueue = new ConcurrentQueue<ImageLoadTask>();
            _SyncContext = SynchronizationContext.Current;
            _LoadTrigger = new ManualResetEventSlim();
        }

        public event EventHandler<ImageLoadCompleteEventArgs> ImageLoadComplete;

        public void Clear()
        {
            foreach (var kvp in _Cache)
            {
                kvp.Value.Dispose();
            }
            _Cache.Clear();
            foreach (var queued in _ImageLoadQueue)
            {
                queued.Dispose();
            }
        }

        public void QueueLoad(ImageReferenceModel model)
        {
            _ImageLoadQueue.Enqueue(new ImageLoadTask(model));
            if (!_LoadTrigger.IsSet)
            {
                _LoadTrigger.Set();
                Task.Run(new Action(ProcessLoad));
            }
        }

        public void Remove(ImageReferenceModel model)
        {
            if (_Cache.TryRemove(model.FilePath, out Image image))
            {
                image.Dispose();
            }
        }

        public bool TryGetImage(ImageReferenceModel imageReference, out Image image) => _Cache.TryGetValue(imageReference.FilePath, out image);
        
        private void ProcessLoad()
        {
            try
            {
                while (_ImageLoadQueue.TryDequeue(out ImageLoadTask task))
                {
                    if (task.IsDisposed) continue;
                    ImageLoadCompleteEventArgs result;
                    try
                    {
                        var image = Image.FromFile(task.ImageReference.FilePath);
                        result = new ImageLoadCompleteEventArgs(task.ImageReference, image);
                    }
                    catch (Exception ex)
                    {
                        result = new ImageLoadCompleteEventArgs(task.ImageReference, ex.Message);
                    }
                    if (task.IsDisposed) result.Dispose();
                    else
                    {
                        _Cache[task.ImageReference.FilePath] = result.Image;
                        _SyncContext.Post(state => ImageLoadComplete?.Invoke(this, result), null);
                    }
                }
            }
            finally
            {
                _LoadTrigger.Reset();
            }
        }

        private class ImageLoadTask : IDisposable
        {
            public ImageLoadTask(ImageReferenceModel imageReference)
            {
                ImageReference = imageReference;
            }

            public ImageReferenceModel ImageReference { get; }

            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }

    internal class ImageLoadCompleteEventArgs : EventArgs, IDisposable
    {
        public ImageLoadCompleteEventArgs(ImageReferenceModel imageReference, Image image)
        {
            ImageReference = imageReference ?? throw new ArgumentNullException(nameof(imageReference));
            Image = image;
        }

        public ImageLoadCompleteEventArgs(ImageReferenceModel imageReference, string error)
        {
            ImageReference = imageReference ?? throw new ArgumentNullException(nameof(imageReference));
            Error = error;
        }

        public ImageReferenceModel ImageReference { get; }

        public Image Image { get; private set; }

        public string Error { get; private set; }

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
                Error = "Disposed";
            }
        }
    }
}
