using System;

namespace ImageViewer.Models
{
    internal class PathEventArgs : EventArgs
    {
        public PathEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
