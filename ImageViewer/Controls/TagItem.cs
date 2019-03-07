using ImageViewer.Data.Models;
using ImageViewer.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer.Controls
{
    internal class TagItem : Control
    {
        private static readonly Size _IconSize;

        private bool _ShowRemoveButton;
        private long _TagId;
        private Color _BackColor;

        static TagItem()
        {
            _IconSize = R.delete_16.Size;
        }

        public TagItem()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public event EventHandler Removed;

        #region ShowRemoveButton property

        public bool ShowRemoveButton
        {
            get => _ShowRemoveButton;
            set
            {
                if (_ShowRemoveButton != value)
                {
                    _ShowRemoveButton = value;
                    OnShowRemoveButtonChanged();
                }
            }
        }

        public event EventHandler ShowRemoveButtonChanged;

        private void OnShowRemoveButtonChanged()
        {
            ShowRemoveButtonChanged?.Invoke(this, EventArgs.Empty);
            Size = GetPreferredSize(Size.Empty);
        }

        #endregion

        public TagModel TagModel
        {
            get => new TagModel
            {
                ID = _TagId,
                Name = Text,
                Color = _BackColor.ToArgb(),
            };
            set
            {
                if (value != null)
                {
                    SuspendLayout();
                    _TagId = value.ID;
                    Text = value.Name;
                    _BackColor = Color.FromArgb(value.Color);
                    ForeColor = DrawingUtils.GetIdealTextColor(_BackColor);
                    Size = GetPreferredSize(Size.Empty);
                    ResumeLayout(true);
                }
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            Size = GetPreferredSize(Size.Empty);
            base.OnPaddingChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ShowRemoveButton)
            {
                var iconRect = new RectangleF(Width - (Padding.Right + _IconSize.Width), 0, _IconSize.Width, Height);
                iconRect = DrawingUtils.GetCenteredImageRegion(R.delete_16, iconRect);
                Cursor = iconRect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (ShowRemoveButton)
            {
                var iconRect = new RectangleF(Width - (Padding.Right + _IconSize.Width), 0, _IconSize.Width, Height);
                iconRect = DrawingUtils.GetCenteredImageRegion(R.delete_16, iconRect);
                if (iconRect.Contains(e.Location))
                {
                    Removed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            using (var g = CreateGraphics())
            {
                SizeF textSize;
                if (!MaximumSize.IsEmpty) textSize = g.MeasureString(Text, Font, MaximumSize);
                else textSize = g.MeasureString(Text, Font);
                Size sz = Size.Ceiling(textSize);
                return new Size(sz.Width + Padding.Horizontal + (ShowRemoveButton ? (/* icon spacing: */ 2 + _IconSize.Width) : 0), Math.Max(sz.Height + Padding.Vertical, ShowRemoveButton ? _IconSize.Height : 0));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bounds = new RectangleF(0, 0, Width, Height);
            e.Graphics.DrawTag(Text, Font, _BackColor, ForeColor, bounds, Padding);
            if (ShowRemoveButton)
            {
                var iconRect = new RectangleF(bounds.Right - (Padding.Right + _IconSize.Width), bounds.Top, _IconSize.Width, bounds.Height);
                e.Graphics.DrawImageCentered(R.delete_16, iconRect);
            }
        }
    }
}
