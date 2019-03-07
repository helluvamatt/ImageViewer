using ImageViewer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    internal class LogListControl : ScrollableControl
    {
        private readonly List<Rectangle> _Bounds;
        private readonly string _TimestampFormat;

        private Padding _ItemPadding;
        private Size _TimestampSize;
        private BindingList<ComponentErrorEventArgs> _DataSource;
        private int _SelectedIndex = -1;
        
        public LogListControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserPaint | ControlStyles.UserMouse, true);
            _Bounds = new List<Rectangle>();
            _TimestampFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
        }

        public BindingList<ComponentErrorEventArgs> DataSource
        {
            get => _DataSource;
            set
            {
                if (_DataSource != value)
                {
                    if (_DataSource != null) _DataSource.ListChanged -= OnDataSourceListChanged;
                    _DataSource = value;
                    if (_DataSource != null) _DataSource.ListChanged += OnDataSourceListChanged;
                    PerformLayout();
                    Invalidate();
                    if (_DataSource != null && _DataSource.Count > 0)
                    {
                        _SelectedIndex = 0;
                        OnSelectedIndexChanged();
                    }
                }
            }
        }

        public Padding ItemPadding
        {
            get => _ItemPadding;
            set
            {
                if (_ItemPadding != value)
                {
                    _ItemPadding = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        private void OnDataSourceListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.Reset)
            {
                PerformLayout();
                SelectedIndex = e.NewIndex;
            }
            else
            {
                PerformLayout();
                Invalidate();
            }
        }

        #region SelectedIndex property

        public int SelectedIndex
        {
            get => _SelectedIndex;
            set
            {
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    OnSelectedIndexChanged();
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged()
        {
            if (SelectedIndex > -1 && SelectedIndex < _Bounds.Count)
            {
                AutoScrollPosition = new Point(0, _Bounds[SelectedIndex].Y);
            }
            Invalidate();
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        var index = SelectedIndex - 1;
                        if (index > -1) SelectedIndex = index;
                        return true;
                    }
                case Keys.Down:
                    {
                        var index = SelectedIndex + 1;
                        if (index < _DataSource.Count) SelectedIndex = index;
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!Focused) Focus();

            var pt = e.Location;
            pt.Y -= AutoScrollPosition.Y;
            for (int i = 0; i < _Bounds.Count; i++)
            {
                if (_Bounds[i].Contains(pt))
                {
                    SelectedIndex = i;
                    break;
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            _Bounds.Clear();
            if (_DataSource != null)
            {
                using (var g = CreateGraphics())
                {
                    _TimestampSize = Size.Ceiling(g.MeasureString(_TimestampFormat, Font));

                    int y = 0;
                    for (int i = 0; i < _DataSource.Count; i++)
                    {
                        // Responsive: break lines if the width is below a certain fit size
                        var availableWidth = Width - ItemPadding.Horizontal;
                        int messageWidth = availableWidth < _TimestampSize.Width + 400 ? availableWidth : availableWidth - _TimestampSize.Width;
                        var textSize = Size.Ceiling(g.MeasureString(_DataSource[i].Message, Font, messageWidth));
                        var height = (availableWidth < _TimestampSize.Width + 400 ? textSize.Height + _TimestampSize.Height : Math.Max(textSize.Height, _TimestampSize.Height)) + ItemPadding.Vertical;
                        _Bounds.Add(new Rectangle(0, y, Width, height));
                        y += height;
                    }

                    AutoScrollMinSize = new Size(0, y);
                }
            }
            base.OnLayout(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_DataSource == null) return;

            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            for (int i = 0; i < _DataSource.Count; i++)
            {
                var bounds = _Bounds[i];

                // Skip rows that are out of the visible region because of scrolling
                if (bounds.Bottom < -AutoScrollPosition.Y) continue;
                if (bounds.Y > -AutoScrollPosition.Y + ClientSize.Height) continue;

                using (var bgBrush = new SolidBrush(i == SelectedIndex ? SystemColors.Highlight : BackColor))
                {
                    e.Graphics.FillRectangle(bgBrush, bounds);
                }

                var entry = _DataSource[i];
                using (var brush = new SolidBrush(i == SelectedIndex ? SystemColors.HighlightText : ForeColor))
                {
                    var timestampStr = entry.Timestamp.ToString(_TimestampFormat);
                    e.Graphics.DrawString(timestampStr, Font, brush, bounds.X + ItemPadding.Left, bounds.Y + ItemPadding.Top);

                    // Responsive: break lines if the width is below a certain fit size
                    int left = bounds.Width < _TimestampSize.Width + 400 ? ItemPadding.Left : _TimestampSize.Width;
                    int x = bounds.X + left;
                    int y = bounds.Y + (bounds.Width < _TimestampSize.Width + 400 ? _TimestampSize.Height : ItemPadding.Top);
                    int width = bounds.Width - (left + ItemPadding.Horizontal);
                    int height = bounds.Height - ItemPadding.Vertical;
                    var messageBounds = new Rectangle(x, y, width, height);
                    e.Graphics.DrawString(entry.Message, Font, brush, messageBounds, new StringFormat());
                }

                if (Focused && i == SelectedIndex) ControlPaint.DrawFocusRectangle(e.Graphics, bounds, SystemColors.HighlightText, SystemColors.Highlight);
            }

            e.Graphics.ResetTransform();
        }

    }
}
