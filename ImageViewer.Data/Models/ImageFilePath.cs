using SQLite;

namespace ImageViewer.Data.Models
{
    internal class ImageFilePath
    {
        [Column("file_path")]
        public string Path { get; set; }
    }
}
