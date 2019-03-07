using ImageViewer.Data.Models;
using SQLite;

namespace ImageViewer.Data.Migrations
{
    internal static class ImageDatabase
    {
        public static void Migrate(SQLiteConnection db)
        {
            db.CreateTable<ImageModel>();
            db.CreateTable<TagModel>();
            db.CreateTable<ImageTagModel>();
            db.CreateTable<ImageMetadata>();

            db.CreateIndex("ix_images_file_hash", "images", new string[] { "file_hash" });
            db.CreateIndex("ux_image_metadata", "image_metadata", new string[] { "image_id", "name" }, true);
            db.CreateIndex("ux_image_tags", "image_tags", new string[] { "image_id", "tag_id" }, true);
            db.CreateIndex("ux_images_file_path_name", "images", new string[] { "file_path", "file_name" }, true);

            SetVersion(db, 1);
        }

        public static void SetVersion(SQLiteConnection db, int version)
        {
            db.Execute($"PRAGMA user_version = {version};");
        }

        public static int GetVersion(SQLiteConnection db)
        {
            return db.ExecuteScalar<int>("PRAGMA user_version;");
        }
    }
}
