using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageViewer.Data.Models
{
    [Table("images")]
    public class ImageModel : IEquatable<ImageModel>
    {
        public ImageModel()
        {
            Tags = new List<TagModel>();
        }

        public ImageModel(string path) : this()
        {
            FilePath = path;
            Title = Name;
        }
        
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long ID { get; set; }

        [Column("file_path")]
        [MaxLength(65536)]
        public string FolderPath { get; set; }

        [Column("file_name")]
        [MaxLength(512)]
        public string Name { get; set; }

        [Column("title")]
        [MaxLength(512)]
        public string Title { get; set; }

        [Column("file_hash")]
        [Indexed("ix_images_file_hash", 1)]
        [MaxLength(255)]
        public string Hash { get; set; }

        [Column("file_size")]
        public long FileSize { get; set; }

        [Column("file_created_date")]
        public DateTime FileCreatedDate { get; set; }

        [Column("file_modified_date")]
        public DateTime FileModifiedDate { get; set; }

        [Column("width")]
        public int Width { get; set; }

        [Column("height")]
        public int Height { get; set; }

        [Column("bits_per_pixel")]
        public int BitsPerPixel { get; set; }

        [Column("format")]
        [MaxLength(16)]
        public string Format { get; set; }

        [Column("created")]
        public DateTime CreatedDate { get; set; }

        [Column("modified")]
        public DateTime ModifiedDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Ignore]
        public List<TagModel> Tags { get; set; }

        [Ignore]
        public string FilePath
        {
            get => Path.Combine(FolderPath, Name);
            set
            {
                FolderPath = Path.GetDirectoryName(value);
                Name = Path.GetFileName(value);
            }
        }

        public override bool Equals(object obj) => obj is ImageModel other && Equals(this, other);

        public bool Equals(ImageModel other) => Equals(this, other);

        public override int GetHashCode() => ID.GetHashCode();

        public override string ToString() => FilePath;

        public static bool Equals(ImageModel a, ImageModel b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            return a.ID == b.ID;
        }

        public static bool operator ==(ImageModel a, ImageModel b) => Equals(a, b);

        public static bool operator !=(ImageModel a, ImageModel b) => !(a == b);

    }
}
