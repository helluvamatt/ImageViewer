using SQLite;
using System;

namespace ImageViewer.Data.Models
{
    [Table("image_tags")]
    internal class ImageTagModel
    {
        public ImageTagModel(long imageId, long tagId)
        {
            ImageId = imageId;
            TagId = tagId;
            CreatedDate = DateTime.Now;
        }

        [Column("image_id")]
        public long ImageId { get; set; }

        [Column("tag_id")]
        public long TagId { get; set; }

        [Column("created")]
        public DateTime CreatedDate { get; set; }
    }
}
