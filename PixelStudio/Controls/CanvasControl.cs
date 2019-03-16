using PixelStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace PixelStudio.Controls
{
    internal class CanvasControl : ImageBox
    {
        private BindingList<LayerModel> _Layers;
        private Point _MouseLocation;

        #region MouseLocation property

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(typeof(Point), "0,0")]
        public Point MouseLocation
        {
            get => _MouseLocation;
            set
            {
                if (_MouseLocation != value)
                {
                    _MouseLocation = value;
                    OnMouseLocationChanged();
                }
            }
        }

        public event EventHandler MouseLocationChanged;

        private void OnMouseLocationChanged()
        {
            MouseLocationChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        [DefaultValue(null)]
        public ToolboxItemModel CurrentTool { get; set; }

        [DefaultValue(null)]
        public LayerModel CurrentLayer { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(null)]
        public BindingList<LayerModel> Layers
        {
            get => _Layers;
            set
            {
                if (_Layers != value)
                {
                    if (_Layers != null) _Layers.ListChanged -= OnLayersListChanged;
                    _Layers = value;
                    if (_Layers != null) _Layers.ListChanged += OnLayersListChanged;
                }
            }
        }

        #region Control overrides

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseLocation = PointToImage(e.Location);
            if (CurrentLayer != null && CurrentLayer.HandleMouseDown(CurrentTool, MouseLocation)) Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseLocation = PointToImage(e.Location);
            if (CurrentLayer != null && CurrentLayer.HandleMouseMove(MouseLocation)) Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseLocation = PointToImage(e.Location);
            if (CurrentLayer != null && CurrentLayer.HandleMouseUp(MouseLocation)) Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnVirtualDraw(PaintEventArgs e)
        {
            if (Layers != null)
            {
                using (var img = RenderDrawing())
                {
                    e.Graphics.DrawImage(img, GetImageViewPort(), GetSourceImageRegion(), GraphicsUnit.Pixel);
                }
            }
        }

        public Bitmap RenderDrawing()
        {
            var bm = new Bitmap(VirtualSize.Width, VirtualSize.Height);
            using (var g = Graphics.FromImage(bm))
            {
                if (Layers != null)
                {
                    foreach (var layer in Layers.Reverse())
                    {
                        layer.Draw(g, new Rectangle(Point.Empty, VirtualSize));
                    }
                }
            }
            return bm;
        }

        #endregion

        private void OnLayersListChanged(object sender, ListChangedEventArgs e)
        {
            Invalidate();
        }
    }
}
