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
        private readonly BindingList<LayerModel> _Layers;

        private bool _IsDirty;
        private string _CurrentFile;

        public MainForm()
        {
            InitializeComponent();
            _History = new History(20);
            layerModelBindingSource.DataSource = _Layers = new BindingList<LayerModel>();
            toolboxItemModelBindingSource.DataSource = new List<ToolboxItemModel>
            {
                new ToolboxItemModel(R.tool_selection_16, R.Tool_Selection),
                new ToolboxItemModel(R.tool_pencil_16, R.Tool_Pencil),
                new ToolboxItemModel(R.tool_brush_16, R.Tool_Brush),
                new ToolboxItemModel(R.tool_eraser_16, R.Tool_Eraser),
                new ToolboxItemModel(R.tool_shape_16, R.Tool_Shape),
                new ToolboxItemModel(R.tool_line_16, R.Tool_Line),
            };

            _History.PropertyChanged += OnHistoryPropertyChanged;

            _Layers.ListChanged += OnLayersListChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedImage, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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
                var result = MessageBox.Show(this, R.AreYouSureUnsavedImage, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }

            // TODO Launch new dialog
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            if (_IsDirty)
            {
                var result = MessageBox.Show(this, R.AreYouSureUnsavedImage, R.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if ((result == DialogResult.Yes && !TrySave()) || result == DialogResult.Cancel) return;
            }
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _CurrentFile = openFileDialog.FileName;
                    _Layers.Clear();
                    _History.Reset();
                    var img = Image.FromFile(_CurrentFile);
                    canvasControl.VirtualSize = img.Size;
                    _Layers.Add(new LayerModel(R.Background, img, Point.Empty));
                    _IsDirty = false;
                    SetTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(R.ErrorFailedToLoadImage, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            canvasControl.SelectAll();
        }

        private void OnSelectNoneClick(object sender, EventArgs e)
        {
            canvasControl.SelectNone();
        }

        #endregion

        #region Image menu

        // TODO Event handlers for Image menu

        #endregion

        #region View menu

        private void OnZoomInClick(object sender, EventArgs e)
        {
            canvasControl.ZoomIn();
        }

        private void OnZoomOutClick(object sender, EventArgs e)
        {
            canvasControl.ZoomOut();
        }

        private void OnResetZoomClick(object sender, EventArgs e)
        {
            canvasControl.ActualSize();
        }

        private void OnZoomToFitClick(object sender, EventArgs e)
        {
            canvasControl.ZoomToFit();
        }

        #endregion

        #region Form controls

        private void OnToolboxDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (toolboxItemModelBindingSource[e.Index] is ToolboxItemModel toolboxItem)
            {
                e.Graphics.DrawListItem(toolboxItem.Icon, toolboxItem.Name, Font, ForeColor, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void OnGridLayersCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);
                e.Graphics.DrawImageCentered(R.eye_16, e.CellBounds);
                e.Handled = true;
            }
        }

        private void OnCanvasMouseLocationChanged(object sender, EventArgs e)
        {
            lblMouseCoord.Text = string.Format(R.LabelMouseLocation, canvasControl.MouseLocation.X, canvasControl.MouseLocation.Y);
        }

        private void OnLayerCurrentChanged(object sender, EventArgs e)
        {
            canvasControl.CurrentLayer = layerModelBindingSource.Current as LayerModel;
        }

        private void OnToolboxItemCurrentChanged(object sender, EventArgs e)
        {
            canvasControl.CurrentTool = toolboxItemModelBindingSource.Current as ToolboxItemModel;
        }

        private void OnSelectionRegionChanged(object sender, EventArgs e)
        {
            var point = Point.Truncate(canvasControl.SelectionRegion.Location);
            var size = Size.Round(canvasControl.SelectionRegion.Size);
            lblStatusSelection.Text = size.IsEmpty ? R.LabelSelectionNone : string.Format(R.LabelSelection, point.X, point.Y, size.Width, size.Height);
        }

        private void OnVirtualSizeChanged(object sender, EventArgs e)
        {
            lblStatusSize.Text = string.Format(R.LabelSize, canvasControl.VirtualSize.Width, canvasControl.VirtualSize.Height);
        }

        private void OnZoomed(object sender, Cyotek.Windows.Forms.ImageBoxZoomEventArgs e)
        {
            lblStatusZoom.Text = string.Format("{0:P}", canvasControl.ZoomFactor);
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

        private void OnLayersListChanged(object sender, ListChangedEventArgs e)
        {
            _IsDirty = true;
        }

        #endregion

        #endregion

        private void SetTitle()
        {
            Text = string.IsNullOrEmpty(_CurrentFile) ? R.AppTitle : string.Format("{0} - {1}{2}", R.AppTitle, Path.GetFileName(_CurrentFile), _IsDirty ? "*" : "");
        }

        private bool TrySave()
        {
            if (string.IsNullOrEmpty(_CurrentFile))
            {
                return DoSaveAs();
            }
            else
            {
                DoSave();
                return true;
            }
        }

        private bool DoSaveAs()
        {
            saveFileDialog.FileName = _CurrentFile;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                _CurrentFile = saveFileDialog.FileName;
                DoSave();
                SetTitle();
                return true;
            }
            return false;
        }

        private void DoSave()
        {
            try
            {
                var img = canvasControl.RenderDrawing();
                if (img != null)
                {
                    // TODO Format?
                    img.Save(_CurrentFile);
                    _IsDirty = false;
                    SetTitle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format(R.ErrorFailedToSaveImage, ex.Message), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
