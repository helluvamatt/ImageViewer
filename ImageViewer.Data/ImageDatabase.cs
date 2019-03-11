using ImageViewer.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageViewer.Data
{
    public class ImageDatabase
    {
        private readonly string _DbPath;
        private readonly Action<string> _Tracer;
        private readonly SemaphoreSlim _Lock;

        static ImageDatabase()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }

        public ImageDatabase(string dbPath, Action<string> tracer = null)
        {
            _DbPath = dbPath;
            _Tracer = tracer;
            _Lock = new SemaphoreSlim(1);
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
            using (var db = GetConnection())
            {
                Migrations.ImageDatabase.Migrate(db.Connection);
            }
        }

        #region Images

        public ImageModel GetImageByPath(string path)
        {
            using (var db = GetConnection())
            {
                string folderPath = Path.GetDirectoryName(path);
                string fileName = Path.GetFileName(path);
                var img = db.Connection.Table<ImageModel>().Where(m => m.FolderPath == folderPath && m.Name == fileName).SingleOrDefault();
                if (img != null && img.IsDeleted == false)
                {
                    GetImageTags(db.Connection, img);
                }
                return img;
            }
        }

        public ImageModel GetImageById(long id)
        {
            using (var db = GetConnection())
            {
                var img = db.Connection.Table<ImageModel>().Where(m => m.ID == id).SingleOrDefault();
                if (img != null)
                {
                    GetImageTags(db.Connection, img);
                }
                return img;
            }
        }

        public IEnumerable<ImageModel> GetImages(int page = 1, int pageSize = 50)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<ImageModel>().Where(m => m.IsDeleted == false).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<ImageModel>().Where(m => m.IsDeleted == false).ToList();
            }
        }

        public int CountImagesWithTag(TagModel tag)
        {
            using (var db = GetConnection())
            {
                return db.Connection.ExecuteScalar<int>("SELECT COUNT(*) FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ?", tag.ID);
            }
        }

        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Query<ImageModel>("SELECT i.* FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ?", tag.ID).ToList();
            }
        }

        public IEnumerable<ImageModel> GetImagesWithTag(TagModel tag, int limit)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Query<ImageModel>("SELECT i.* FROM image_tags t JOIN images i ON i.id = t.image_id WHERE t.tag_id = ? ORDER BY i.modified DESC LIMIT ?", tag.ID, limit).ToList();
            }
        }

        public int CountImagesInFolder(string folder)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).Count();
            }
        }

        public IEnumerable<ImageModel> GetImagesInFolder(string folder)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).ToList();
            }
        }

        public IEnumerable<ImageModel> GetImagesInFolder(string folder, int limit)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<ImageModel>().Where(m => m.FolderPath == folder && m.IsDeleted == false).OrderBy(m => m.ModifiedDate).Take(limit).ToList();
            }
        }

        public void SaveImage(ImageModel model)
        {
            using (var db = GetConnection())
            {
                if (model.ID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    db.Connection.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    db.Connection.Insert(model);
                }
            }
        }

        public ImageModel DeleteImage(string path)
        {
            using (var db = GetConnection())
            {
                var img = GetImageByPath(path);
                if (img != null)
                {
                    db.Connection.RunInTransaction(() =>
                    {
                        // Delete metadata
                        db.Connection.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Connection.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, delete the object
                        db.Connection.Delete(img);
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
                    db.Connection.RunInTransaction(() =>
                    {
                        // Delete metadata
                        db.Connection.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Connection.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, mark the image as deleted
                        db.Connection.Execute("UPDATE images SET is_deleted = 1 WHERE id = ?", img.ID);
                    });
                }
            }
        }

        public int DeleteImagesRecursive(string folderPath)
        {
            using (var db = GetConnection())
            {
                // Since we are limited by SQLite, just get the affected images up front and loop over them
                var images = db.Connection.Query<ImageModel>("SELECT * FROM images WHERE file_path LIKE (? || '%')", folderPath).ToList();
                db.Connection.RunInTransaction(() =>
                {
                    foreach (var img in images)
                    {
                        // Delete metadata
                        db.Connection.Execute("DELETE FROM image_metadata WHERE image_id = ?", img.ID);

                        // Delete tags
                        db.Connection.Execute("DELETE FROM image_tags WHERE image_id = ?", img.ID);

                        // Finally, mark the image as deleted
                        db.Connection.Execute("UPDATE images SET is_deleted = 1 WHERE id = ?", img.ID);
                    }
                });
                return images.Count;
            }
        }

        public int RemoveDeletedImages()
        {
            using (var db = GetConnection())
            {
                return db.Connection.Execute("DELETE FROM images WHERE is_deleted = 1");
            }
        }

        #endregion

        #region Folders

        public IEnumerable<string> GetFolders(string root)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Query<ImageFilePath>("SELECT DISTINCT file_path FROM images WHERE is_deleted = 0 AND file_path LIKE (? || '%')", root)
                    .ToList() // Break out of SQLite
                    .Where(m => m.Path.Length > root.Length)
                    .Select(m => Path.Combine(root, m.Path.Substring(root.Length + 1).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0]))
                    .Distinct()
                    .ToList();
            }
        }

        public int CountFolders(string root)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Query<ImageFilePath>("SELECT DISTINCT file_path FROM images WHERE is_deleted = 0 AND file_path LIKE (? || '%')", root)
                    .ToList() // Break out of SQLite
                    .Where(m => m.Path.Length > root.Length)
                    .Select(m => Path.Combine(root, m.Path.Substring(root.Length + 1).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0]))
                    .Distinct()
                    .Count();
            }
        }

        public DateTime GetFolderLastModified(string root)
        {
            using (var db = GetConnection())
            {
                return db.Connection.ExecuteScalar<DateTime>("SELECT MAX(file_modified_date) FROM images WHERE is_deleted = 0 AND file_path LIKE (? || '%')", root);
            }
        }

        #endregion

        #region Tags

        public IEnumerable<TagModel> GetTags()
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<TagModel>().ToList();
            }
        }

        public IEnumerable<TagModel> GetTags(int limit)
        {
            using (var db = GetConnection())
            {
                return db.Connection.Table<TagModel>().OrderByDescending(m => m.LastUsedDate).Take(limit).ToList();
            }
        }

        public IEnumerable<TagModel> GetTagsForAutoComplete(string startsWith)
        {
            if (string.IsNullOrEmpty(startsWith)) return new List<TagModel>();
            using (var db = GetConnection())
            {
                return db.Connection.Query<TagModel>("SELECT * FROM tags WHERE name LIKE (? || '%') ORDER BY last_used DESC", startsWith).ToList();
            }
        }

        public void SaveTag(TagModel model)
        {
            using (var db = GetConnection())
            {
                if (model.ID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    db.Connection.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    db.Connection.Insert(model);
                }
            }
        }

        public void GetImageTags(ImageModel model)
        {
            if (model.ID > 0)
            {
                using (var db = GetConnection())
                {
                    GetImageTags(db.Connection, model);
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
                    return db.Connection.ExecuteScalar<int>("SELECT COUNT(*) FROM image_tags WHERE tag_id = ? AND image_id = ?", image.ID, tag.ID) > 0;
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
                    db.Connection.Insert(new ImageTagModel(image.ID, tag.ID), "OR IGNORE");
                }
            }
        }

        public void RemoveTagImage(ImageModel image, TagModel tag)
        {
            if (image.ID > 0 && tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.Connection.Execute("DELETE FROM image_tags WHERE image_id = ? AND tag_id = ?", image.ID, tag.ID);
                }
            }
        }

        public void RemoveTag(TagModel tag)
        {
            if (tag.ID > 0)
            {
                using (var db = GetConnection())
                {
                    db.Connection.RunInTransaction(() =>
                    {
                        // Delete tag mappings
                        db.Connection.Execute("DELETE FROM image_tags WHERE tag_id = ?", tag.ID);

                        // Finally, delete the object
                        db.Connection.Delete(tag);
                    });
                }
            }
        }

        #endregion

        public void ResetDatabase()
        {
            // This should only be called after the user has clicked a 'Yes' prompt at least once
            using (var db = GetConnection())
            {
                db.Connection.DeleteAll<ImageTagModel>();
                db.Connection.DeleteAll<TagModel>();
                db.Connection.DeleteAll<ImageModel>();
            }
        }

        #region Multithreaded protection

        private SQLiteConnectionResource GetConnection()
        {
            _Lock.Wait();
            var conn = new SQLiteConnection(_DbPath);
            if (_Tracer != null)
            {
                conn.Tracer = _Tracer;
                conn.Trace = true;
            }
            var resource = new SQLiteConnectionResource(conn);
            resource.ResourceDisposed += OnResourceDisposed;
            return resource;
        }

        private void OnResourceDisposed()
        {
            _Lock.Release();
        }

        private class SQLiteConnectionResource : IDisposable
        {
            public SQLiteConnectionResource(SQLiteConnection conn)
            {
                Connection = conn ?? throw new ArgumentNullException(nameof(conn));
            }

            public SQLiteConnection Connection { get; }

            public event Action ResourceDisposed;

            public void Dispose()
            {
                Connection.Dispose();
                ResourceDisposed?.Invoke();
            }
        }

        #endregion
    }
}
