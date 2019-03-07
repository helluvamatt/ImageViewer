using ImageViewer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;

namespace ImageViewer.Data
{
    public class ImageDatabase
    {
        private readonly string _DbPath;
        private readonly Action<string> _Tracer;

        static ImageDatabase()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }

        public ImageDatabase(string dbPath, Action<string> tracer = null)
        {
            _DbPath = dbPath;
            _Tracer = tracer;
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
            using (var db = GetConnection())
            {
                Migrations.ImageDatabase.Migrate(db);
            }
        }

        #region Images

        public ImageModel GetImageByPath(string path)
        {
            using (var db = GetConnection())
            {
                string folderPath = Path.GetDirectoryName(path);
                string fileName = Path.GetFileName(path);
                var img = db.Table<ImageModel>().Where(m => m.FolderPath == folderPath && m.Name == fileName).SingleOrDefault();
                if (img != null && img.IsDeleted == false)
                {
                    GetImageTags(db, img);
                    GetImageMetadata(db, img);
                }
                return img;
            }
        }

        public ImageModel GetImageById(long id)
        {
            using (var db = GetConnection())
            {
                var img = db.Table<ImageModel>().Where(m => m.ID == id).SingleOrDefault();
                if (img != null)
                {
                    GetImageTags(db, img);
                    GetImageMetadata(db, img);
                }
                return img;
            }
        }

        public IEnumerable<ImageModel> GetImages(int page = 1, int pageSize = 50)
        {
            using (var db = GetConnection())
            {
                return db.Table<ImageModel>().Where(m => m.IsDeleted == false).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            using (var db = GetConnection())
            {
                return db.Table<ImageModel>().Where(m => m.IsDeleted == false).ToList();
            }
        }

        public int CountImagesWithTag(TagModel tag)
        {
            using (var db = GetConnection())
            {
                return db.ExecuteScalar<int>("SELECT COUNT(*) FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ?", tag.ID);
            }
        }

        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag)
        {
            using (var db = GetConnection())
            {
                return db.Query<ImageModel>("SELECT i.* FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ?", tag.ID).ToList();
            }
        }

        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag, int limit)
        {
            using (var db = GetConnection())
            {
                return db.Query<ImageModel>("SELECT i.* FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ? ORDER BY i.modified DESC LIMIT ?", tag.ID, limit).ToList();
            }
        }

        public int CountImagesInFolder(string folder)
        {
            using (var db = GetConnection())
            {
                return db.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).Count();
            }
        }

        public IEnumerable<ImageModel> GetImagesInFolder(string folder)
        {
            using (var db = GetConnection())
            {
                return db.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).ToList();
            }
        }

        public IEnumerable<ImageModel> GetImagesInFolder(string folder, int limit)
        {
            using (var db = GetConnection())
            {
                return db.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).OrderBy(m => m.ModifiedDate).Take(limit).ToList();
            }
        }

        public void SaveImage(ImageModel model)
        {
            using (var db = GetConnection())
            {
                try
                {
                    if (model.ID > 0)
                    {
                        model.ModifiedDate = DateTime.Now;
                        db.Update(model);
                    }
                    else
                    {
                        model.CreatedDate = DateTime.Now;
                        db.Insert(model);
                    }
                }
                catch (SQLiteException ex)
                {
                    var msg = SQLite3.GetErrmsg(db.Handle);
                    throw SQLiteException.New(ex.Result, msg);
                }
                SaveImageMetadata(db, model);
            }
        }

        public ImageModel DeleteImage(string path)
        {
            using (var db = GetConnection())
            {
                var img = GetImageByPath(path);
                if (img != null)
                {
                    db.RunInTransaction(() =>
                    {
                        // Delete metadata
                        db.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, delete the object
                        db.Delete(img);
                    });
                }
                return img;
            }
        }

        public void DeleteImage(ImageModel img)
        {
            if (img.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.RunInTransaction(() =>
                    {
                        // Delete metadata
                        db.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, mark the image as deleted
                        db.Execute("UPDATE images SET is_deleted = 1 WHERE id = ?", img.ID);
                    });
                }
            }
        }

        public int DeleteImagesRecursive(string folderPath)
        {
            using (var db = GetConnection())
            {
                // Since we are limited by SQLite, just get the affected images up front and loop over them
                var images = db.Query<ImageModel>("SELECT * FROM images WHERE file_path LIKE (? || '%')", folderPath).ToList();
                db.RunInTransaction(() =>
                {
                    foreach (var img in images)
                    {
                        // Delete metadata
                        db.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, mark the image as deleted
                        db.Execute("UPDATE images SET is_deleted = 1 WHERE id = ?", img.ID);
                    }
                });
                return images.Count;
            }
        }

        public int RemoveDeletedImages()
        {
            using (var db = GetConnection())
            {
                return db.Execute("DELETE FROM images WHERE is_deleted = 1");
            }
        }

        #endregion

        #region Folders

        public IEnumerable<string> GetFolders(string root)
        {
            using (var db = GetConnection())
            {
                return db.Query<ImageFilePath>("SELECT DISTINCT file_path FROM images WHERE is_deleted = 0 AND file_path LIKE (? || '%')", root)
                    .ToList() // Break out of SQLite
                    .Where(m => m.Path.Length > root.Length)
                    .Select(m => Path.Combine(root, m.Path.Substring(root.Length + 1).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0]))
                    .Distinct()
                    .ToList();
            }
        }

        #endregion

        #region Image Metadata

        public void GetImageMetadata(ImageModel model)
        {
            if (model.ID > 0)
            {
                using (var db = GetConnection())
                {
                    GetImageMetadata(db, model);
                }
            }
        }

        private void GetImageMetadata(SQLiteConnection db, ImageModel model)
        {
            model.Metadata = db.Table<ImageMetadata>().Where(m => m.ImageId == model.ID).ToList();
        }

        public void SaveImageMetadata(ImageModel model)
        {
            if (model.ID > 0)
            {
                using (var db = GetConnection())
                {
                    SaveImageMetadata(db, model);
                }
            }
        }

        private void SaveImageMetadata(SQLiteConnection db, ImageModel model)
        {
            db.RunInTransaction(() =>
            {
                db.Execute("DELETE FROM image_metadata WHERE image_id = ?", model.ID);
                db.InsertAll(model.Metadata, false);
            });
            
        }

        #endregion

        #region Tags

        public IEnumerable<TagModel> GetTags()
        {
            using (var db = GetConnection())
            {
                return db.Table<TagModel>().ToList();
            }
        }

        public IEnumerable<TagModel> GetTags(int limit)
        {
            using (var db = GetConnection())
            {
                return db.Table<TagModel>().OrderByDescending(m => m.LastUsedDate).Take(limit).ToList();
            }
        }

        public IEnumerable<TagModel> GetTagsForAutoComplete(string startsWith)
        {
            if (string.IsNullOrEmpty(startsWith)) return new List<TagModel>();
            using (var db = GetConnection())
            {
                return db.Query<TagModel>("SELECT * FROM tags WHERE name LIKE (? || '%') ORDER BY last_used DESC", startsWith).ToList();
            }
        }

        public void SaveTag(TagModel model)
        {
            using (var db = GetConnection())
            {
                if (model.ID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    db.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    db.Insert(model);
                }
            }
        }

        public void GetImageTags(ImageModel model)
        {
            if (model.ID > 0)
            {
                using (var db = GetConnection())
                {
                    GetImageTags(db, model);
                }
            }
        }

        private void GetImageTags(SQLiteConnection db, ImageModel model)
        {
            model.Tags = db.Query<TagModel>("SELECT t.* FROM image_tags it JOIN tags t ON t.id = it.tag_id WHERE it.image_id = ?", model.ID).ToList();
        }

        public bool GetImageHasTag(ImageModel image, TagModel tag)
        {
            if (image.ID > 0 && tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    return db.ExecuteScalar<int>("SELECT COUNT(*) FROM image_tags WHERE tag_id = ? AND image_id = ?", image.ID, tag.ID) > 0;
                }
            }
            return false;
        }

        public void AddTagImage(ImageModel image, TagModel tag)
        {
            if (image.ID > 0 && tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.Insert(new ImageTagModel(image.ID, tag.ID), "OR IGNORE");
                }
            }
        }

        public void RemoveTagImage(ImageModel image, TagModel tag)
        {
            if (image.ID > 0 && tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.Execute("DELETE FROM image_tags WHERE image_id = ? AND tag_id = ?", image.ID, tag.ID);
                }
            }
        }

        public void RemoveTag(TagModel tag)
        {
            if (tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.RunInTransaction(() =>
                    {
                        // Delete tag mappings
                        db.Execute("DELETE FROM image_tags WHERE tag_id = ?", tag.ID);

                        // Finally, delete the object
                        db.Delete(tag);
                    });
                }
            }
        }

        #endregion

        private SQLiteConnection GetConnection()
        {
            var db = new SQLiteConnection(_DbPath);
            if (_Tracer != null)
            {
                db.Tracer = _Tracer;
                db.Trace = true;
            }
            return db;
        }
    }
}
