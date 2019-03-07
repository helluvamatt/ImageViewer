using System;

namespace ImageViewer.Models
{
    internal class FolderEventArgs : EventArgs
    {
        public FolderEventArgs(string path)
        {
            FolderPath = path;
        }

        public string FolderPath { get; }
    }
}
