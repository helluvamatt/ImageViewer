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
        private readonly History _History;

        private bool _IsDirty;

        public MainForm()
        {
            InitializeComponent();
            _History = new History(20);
            

            _History.PropertyChanged += OnHistoryPropertyChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_IsDirty)
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
            if (_IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }

            // TODO Launch new dialog
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            if (_IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedProject, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    // TODO Load project
                    //_CurrentFile = openFileDialog.FileName;
                    _History.Reset();
                    
                    _IsDirty = false;
                    SetTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(R.ErrorFailedToLoadProject, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void OnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Edit menu

        private void OnUndoClick(object sender, EventArgs e)
        {
            _History.Undo();
        }

        private void OnRedoClick(object sender, EventArgs e)
        {
            _History.Redo();
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

        #region Image menu

        // TODO Event handlers for Image menu

        #endregion

        #region Form controls

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
                menuItemRedo.Enabled = _History.CanRedo;
            }
            else if (nameof(History.CanUndo) == e.PropertyName)
            {
                menuItemUndo.Enabled = _History.CanUndo;
            }
        }

        #endregion

        #endregion

        private void SetTitle()
        {
            //Text = string.IsNullOrEmpty(_CurrentFile) ? R.AppTitle : string.Format("{0} - {1}{2}", R.AppTitle, Path.GetFileName(_CurrentFile), _IsDirty ? "*" : "");
        }

        private bool TrySave()
        {
            //if (string.IsNullOrEmpty(_CurrentFile))
            //{
            //    return DoSaveAs();
            //}
            //else
            //{
            //    DoSave();
                return true;
            //}
        }

        private bool DoSaveAs()
        {
            //saveFileDialog.FileName = _CurrentFile;
            //if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    _CurrentFile = saveFileDialog.FileName;
            //    DoSave();
            //    SetTitle();
            //    return true;
            //}
            return false;
        }

        private void DoSave()
        {
            //try
            //{
            //    var img = canvasControl.RenderDrawing();
            //    if (img != null)
            //    {
            //        // TODO Format?
            //        img.Save(_CurrentFile);
            //        _IsDirty = false;
            //        SetTitle();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, string.Format(R.ErrorFailedToSaveProject, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
