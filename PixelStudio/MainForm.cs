using PixelStudio.Controls;
using PixelStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = PixelStudio.Properties.Resources;

namespace PixelStudio
{
    public partial class MainForm : Form
    {
        private readonly ProjectManager _ProjectManager;

        private string _CurrentFile;

        public MainForm()
        {
            InitializeComponent();
            _ProjectManager = new ProjectManager(20);

            menuItemUndo.Enabled = _ProjectManager.HistoryCanUndo;
            menuItemRedo.Enabled = _ProjectManager.HistoryCanRedo;

            _ProjectManager.HistoryPropertyChanged += OnHistoryPropertyChanged;
            _ProjectManager.ProjectChanged += OnProjectChanged;
            _ProjectManager.IsDirtyChanged += OnProjectIsDirtyChanged;

            _ProjectManager.NewProject();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_ProjectManager.HasProject && _ProjectManager.IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) e.Cancel = true;
            }

            base.OnFormClosing(e);
        }

        #region Event handlers

        #region File menu

        private void OnNewClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject && _ProjectManager.IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }

            _ProjectManager.NewProject();
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject && _ProjectManager.IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _ProjectManager.LoadProject(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(R.ErrorFailedToLoadProject, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnCloseClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject)
            {
                if (_ProjectManager.IsDirty)
                {
                    var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
                }

                _ProjectManager.CloseProject();
            }
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            TrySave();
        }

        private void OnSaveAsClick(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void OnImportImagesClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject)
            {
                if (openFileDialogImport.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (var file in openFileDialogImport.FileNames)
                    {
                        _ProjectManager.Project.ImageReferences.Add(file);
                    }
                }
            }
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Edit menu

        private void OnUndoClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject) _ProjectManager.HistoryUndo();
        }

        private void OnRedoClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject) _ProjectManager.HistoryRedo();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            // TODO Cut
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            // TODO Copy
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            // TODO Paste
        }

        private void OnSelectAllClick(object sender, EventArgs e)
        {
            
        }

        private void OnSelectNoneClick(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Project menu

        private void OnExportVideoClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject)
            {
                // TODO Export video dialog
            }
        }

        private void OnProjectPropertiesClick(object sender, EventArgs e)
        {
            if (_ProjectManager.HasProject)
            {
                // TODO Project properties dialog
            }
        }

        #endregion

        #region Form controls

        private void OnImageListDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < imageReferenceModelBindingSource.Count && imageReferenceModelBindingSource[e.Index] is ImageReferenceModel imageReference)
            {
                e.Graphics.DrawListItem(imageReference.IsValid ? R.picture_16 : R.picture_error_16, imageReference.FilePath, e.Font, e.ForeColor, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void OnTimelineZoomChanged(object sender, EventArgs e)
        {
            //btnTimelineZoomIn.Enabled = timelineControl.ZoomInEnabled;
            //btnTimelineZoomOut.Enabled = timelineControl.ZoomOutEnabled;
        }

        private void OnTimelineZoomInClick(object sender, EventArgs e)
        {
            timelineControl.ZoomIn();
        }

        private void OnTimelineZoomOutClick(object sender, EventArgs e)
        {
            timelineControl.ZoomOut();
        }

        private void OnTimelineZoomToFitClick(object sender, EventArgs e)
        {
            timelineControl.ZoomToFit();
        }

        private void OnTransportPreviousClick(object sender, EventArgs e)
        {

        }

        private void OnTransportNextClick(object sender, EventArgs e)
        {

        }

        private void OnTransportPlayPauseClick(object sender, EventArgs e)
        {

        }
        
        #endregion

        #region Data

        private void OnHistoryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(History.CanRedo) == e.PropertyName)
            {
                menuItemRedo.Enabled = _ProjectManager.HistoryCanRedo;
            }
            else if (nameof(History.CanUndo) == e.PropertyName)
            {
                menuItemUndo.Enabled = _ProjectManager.HistoryCanUndo;
            }
        }

        private void OnProjectChanged(object sender, EventArgs e)
        {
            imageReferenceModelBindingSource.DataSource = _ProjectManager.Project?.ImageReferences?.ImageReferences;
            timelineControl.Timeline = _ProjectManager.Project?.Timeline;
            SetTitle();

            menuItemImportImages.Enabled = menuItemSaveAs.Enabled
                = menuItemUndo.Enabled = menuItemRedo.Enabled = menuItemCut.Enabled = menuItemCopy.Enabled = menuItemPaste.Enabled = menuItemSelectAll.Enabled = menuItemSelectNone.Enabled
                = menuItemExportVideo.Enabled = menuItemProjectProperties.Enabled
                = _ProjectManager.HasProject;

            menuItemSave.Enabled = _ProjectManager.HasProject && _ProjectManager.IsDirty;
        }

        private void OnProjectIsDirtyChanged(object sender, EventArgs e)
        {
            menuItemSave.Enabled = _ProjectManager.HasProject && _ProjectManager.IsDirty;
            SetTitle();
        }

        #endregion

        #endregion

        private void SetTitle()
        {
            Text = string.IsNullOrEmpty(_ProjectManager.Project?.Name) ? R.AppTitle : string.Format("{0} - {1}{2}", R.AppTitle, _ProjectManager.Project.Name, _ProjectManager.IsDirty ? "*" : "");
        }

        private bool TrySave()
        {
            if (string.IsNullOrEmpty(_CurrentFile))
            {
                return DoSaveAs();
            }
            else
            {
                return DoSave();
            }
        }

        private bool DoSaveAs()
        {
            if (_ProjectManager.HasProject)
            {
                if (string.IsNullOrEmpty(_CurrentFile))
                {
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog.FileName = _ProjectManager.Project.Name + ".psproj";
                }
                else
                {
                    saveFileDialog.InitialDirectory = Path.GetDirectoryName(_CurrentFile);
                    saveFileDialog.FileName = Path.GetFileName(_CurrentFile);
                }
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _CurrentFile = saveFileDialog.FileName;
                    _ProjectManager.Project.Name = Path.GetFileNameWithoutExtension(_CurrentFile);
                    return DoSave();
                }
                return false;
            }
            return true;
        }

        private bool DoSave()
        {
            if (_ProjectManager.HasProject)
            {
                try
                {
                    _ProjectManager.SaveProject(_CurrentFile);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(R.ErrorFailedToSaveProject, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            return true;
        }
    }
}
