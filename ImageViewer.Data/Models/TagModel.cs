using SQLite;
using System;

namespace ImageViewer.Data.Models
{
    [Table("tags")]
    public class TagModel
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public long ID { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("color")]
        public int Color { get; set; }

        [Column("created")]
        public DateTime CreatedDate { get; set; }

        [Column("modified")]
        public DateTime ModifiedDate { get; set; }

        [Column("last_used")]
        public DateTime LastUsedDate { get; set; }
    }
}
