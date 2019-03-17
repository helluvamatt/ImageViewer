using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace PixelStudio.Models
{
    internal class ProjectManager
    {
        private readonly History _History;
        private readonly ImageCache _ImageCache;

        private ProjectModel _Project;

        public ProjectManager(int historySize)
        {
            _History = new History(historySize);
            _ImageCache = new ImageCache();
            _ImageCache.ImageLoadComplete += OnImageCacheLoadComplete;
        }

        #region IsDirty property

        public bool IsDirty => _Project?.IsDirty ?? false;

        public event EventHandler IsDirtyChanged;

        #endregion

        #region Project property

        public ProjectModel Project
        {
            get => _Project;
            set
            {
                if (_Project != value)
                {
                    if (_Project != null)
                    {
                        _Project.PropertyChanged -= OnProjectPropertyChanged;
                        _Project.ImageReferences.ListChanged -= OnProjectImageReferenceListChanged;
                        _Project.ImageReferences.ListItemRemoving -= OnProjectImageReferencesListItemRemoving;
                    }
                    _Project = value;
                    if (_Project != null)
                    {
                        _Project.PropertyChanged += OnProjectPropertyChanged;
                        _Project.ImageReferences.ListChanged += OnProjectImageReferenceListChanged;
                        _Project.ImageReferences.ListItemRemoving += OnProjectImageReferencesListItemRemoving;
                    }
                    _ImageCache.Clear();
                    OnProjectChanged();
                }
            }
        }

        private void OnProjectChanged()
        {
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(ProjectModel.IsDirty) == e.PropertyName)
            {
                IsDirtyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnProjectImageReferencesListItemRemoving(object sender, ListItemRemovingEventArgs<ImageReferenceModel> e)
        {
            _ImageCache.Remove(e.OldItem);
        }

        private void OnProjectImageReferenceListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    _ImageCache.QueueLoad(Project.ImageReferences.ImageReferences[e.NewIndex]);
                    break;
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted: // This is handled elsewhere
                case ListChangedType.ItemMoved:
                    // Ignore
                    break;
                default:
                    _ImageCache.Clear();
                    foreach (var reference in Project.ImageReferences.ImageReferences) _ImageCache.QueueLoad(reference);
                    break;
            }
        }

        public event EventHandler ProjectChanged;

        public bool HasProject => Project != null;

        #endregion

        #region Project loading/saving

        public void NewProject()
        {
            _History.Reset();
            Project = new ProjectModel();
        }

        public void CloseProject()
        {
            Project = null;
        }

        public void LoadProject(string projectFile)
        {
            var xml = new XmlSerializer(typeof(ProjectModel));
            using (var stream = new FileStream(projectFile, FileMode.Open, FileAccess.Read))
            {
                Project = (ProjectModel)xml.Deserialize(stream);
            }

            // TODO Debugging
            if (Project.IsDirty && System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();

            _History.Reset();
        }

        public void SaveProject(string projectFile)
        {
            if (Project != null)
            {
                var xml = new XmlSerializer(typeof(ProjectModel));
                using (var stream = new FileStream(projectFile, FileMode.Create, FileAccess.Write))
                {
                    xml.Serialize(stream, Project);
                }
                Project.IsDirty = false;
                _History.Reset();
            }
        }

        #endregion

        #region History

        public event PropertyChangedEventHandler HistoryPropertyChanged
        {
            add => _History.PropertyChanged += value;
            remove => _History.PropertyChanged -= value;
        }

        public bool HistoryCanUndo => _History.CanUndo;
        public bool HistoryCanRedo => _History.CanRedo;

        public void HistoryUndo()
        {
            _History.Undo();
        }

        public void HistoryRedo()
        {
            _History.Redo();
        }

        #endregion

        private void OnImageCacheLoadComplete(object sender, ImageLoadCompleteEventArgs e)
        {
            if (Project.ImageReferences.ImageReferences.Contains(e.ImageReference))
            {
                e.ImageReference.IsValid = string.IsNullOrEmpty(e.Error);
            }
        }
    }
}
