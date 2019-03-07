using ImageViewer.Data.Models;
using System;

namespace ImageViewer.Models
{
    internal class ImageEventArgs : EventArgs
    {
        public ImageEventArgs(ImageModel image)
        {
            Image = image;
        }

        public ImageModel Image { get; }
    }

    internal class ImageRemovedEventArgs : ImageEventArgs
    {
        public ImageRemovedEventArgs(ImageModel image, string path) : base(image)
        {
            Path = path;
        }

        public string Path { get; }
    }

}
