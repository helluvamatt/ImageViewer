using SQLite;
using System;

namespace ImageViewer.Data.Models
{
    [Table("image_metadata")]
    public class ImageMetadata
    {
        [Column("image_id")]
        public long ImageId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("value")]
        [MaxLength(255)]
        public string Value { get; set; }

        [Column("modified")]
        public DateTime ModifiedDate { get; set; }
    }
}
