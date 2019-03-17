using PixelStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelStudio.Controls
{
    internal class TimelineControl : Control
    {
        private readonly HScrollBar _HScrollBar;
        private readonly List<TimelineItem> _ItemCollection;

        private TimelineModel _Timeline;
        private int _SelectedIndex;

        private Size _ScrollCanvasSize;

        public TimelineControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            _HScrollBar = new HScrollBar();
            _HScrollBar.Dock = DockStyle.Bottom;
            _HScrollBar.Scroll += OnScroll;
            Controls.Add(_HScrollBar);

            _ItemCollection = new List<TimelineItem>();
        }

        #region Timeline handling

        public TimelineModel Timeline
        {
            get => _Timeline;
            set
            {
                if (_Timeline != value)
                {
                    if (_Timeline != null)
                    {
                        _Timeline.ListChanged -= OnTimelineListChanged;
                        _Timeline.ListItemRemoving -= OnTimelineListItemRemoving;
                    }
                    _Timeline = value;
                    if (_Timeline != null)
                    {
                        _Timeline.ListChanged += OnTimelineListChanged;
                        _Timeline.ListItemRemoving += OnTimelineListItemRemoving;
                    }
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        private void OnTimelineListChanged(object sender, ListChangedEventArgs e)
        {
            // TODO Handle timeline list changes by syncing the event with _ItemCollection
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    break;
                case ListChangedType.ItemDeleted:
                    break;
                case ListChangedType.ItemMoved:
                    break;
                case ListChangedType.ItemChanged:
                    break;
                default:

                    break;
            }
            PerformLayout();
            Invalidate();
        }

        private void OnTimelineListItemRemoving(object sender, ListItemRemovingEventArgs<TimelineItemModel> e)
        {
            // TODO Item is about to be removed, dispose it's image if needed
        }

        #endregion

        #region Selection handling

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

        private void OnSelectedIndexChanged()
        {
            Invalidate();
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Zoom handling

        // Number of milliseconds of timeline per pixel
        // ZoomFactor = ms / px
        // ms / px * time = pixels
        private int _ZoomFactor;
        private int ZoomFactor
        {
            get => _ZoomFactor;
            set
            {
                if (_ZoomFactor != value)
                {
                    _ZoomFactor = value;
                    PerformLayout();
                    Invalidate();
                    ZoomChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        public bool ZoomInEnabled => Timeline != null && ZoomFactor > 1;
        public bool ZoomOutEnabled => Timeline != null && ZoomFactor < MaxZoomFactor;

        public int MaxZoomFactor => Math.Max(1, Timeline != null && Timeline.TotalTime.TotalMilliseconds > 0 ? (int)((ClientSize.Width - 100) / Timeline.TotalTime.TotalMilliseconds) : 0);

        public void ZoomIn()
        {
            if (ZoomInEnabled)
            {
                var f = ZoomFactor / 1000;
                f--;
                ZoomFactor = f * 1000;
            }
        }

        public void ZoomOut()
        {
            if (ZoomOutEnabled)
            {
                var f = ZoomFactor / 1000;
                f++;
                ZoomFactor = f * 1000;
            }
        }

        public void ZoomToFit()
        {
            ZoomFactor = MaxZoomFactor;
        }

        public event EventHandler ZoomChanged;

        #endregion

        #region Scroll handling

        private Size ViewportSize
        {
            get
            {
                var sz = ClientSize;
                if (_HScrollBar.Visible) sz.Height -= _HScrollBar.Height;
                return sz;
            }
        }

        private Size ScrollCanvasSize
        {
            get => _ScrollCanvasSize;
            set
            {
                if (_ScrollCanvasSize != value)
                {
                    _ScrollCanvasSize = value;
                    OnScrollCanvasSizeChanged();
                }
            }
        }

        private void OnScrollCanvasSizeChanged()
        {
            System.Diagnostics.Debug.WriteLine("OnScrollCanvasSizeChanged() called...");
            if (_HScrollBar != null && ViewportSize.Width > -1)
            {
                if (ScrollCanvasSize.Width > ViewportSize.Width)
                {
                    _HScrollBar.LargeChange = ViewportSize.Width / 10;
                    _HScrollBar.SmallChange = ViewportSize.Width / 20;
                    _HScrollBar.Maximum = ScrollCanvasSize.Width - ViewportSize.Width + _HScrollBar.LargeChange - 1;
                    _HScrollBar.Visible = true;
                }
                else
                {
                    _HScrollBar.Visible = false;
                }
            }
        }

        private Point ScrollPosition
        {
            get => new Point(_HScrollBar != null && _HScrollBar.Visible ? -_HScrollBar.Value : 0, 0);
            set
            {
                if (_HScrollBar != null && _HScrollBar.Value != value.X) _HScrollBar.Value = -value.X;
                if (ScrollPosition != value)
                {
                    Invalidate();
                }
            }
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Control overrides

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // TODO Scroll left or right

            base.OnMouseWheel(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (Timeline != null && _ItemCollection.Any())
            {
                // Compute total width (with 100 pixels for a buffer for adding to the end)
                var canvasWidth = (int)(Timeline.TotalTime.TotalMilliseconds / ZoomFactor) + 100;
                ScrollCanvasSize = new Size(canvasWidth, 1);

                // Layout timeline items
                int offset = 0;
                int height = ViewportSize.Height;
                foreach (var item in _ItemCollection)
                {
                    int x = offset / ZoomFactor;
                    int width = (int)(item.Model.Duration.TotalMilliseconds / ZoomFactor);
                    item.Bounds = new Rectangle(x, 0, width, height);
                    offset += (int)item.Model.DurationMs;
                }
            }

            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Timeline != null && _ItemCollection.Any())
            {

            }
        }

        #endregion

        #region Event handlers

        #endregion

        #region Layout classes

        private class TimelineItem
        {
            public TimelineItem(TimelineItemModel model)
            {
                Model = model ?? throw new ArgumentNullException(nameof(model));
            }

            public TimelineItemModel Model { get; }

            public Rectangle Bounds { get; set; }

            public Image Image { get; set; }
        }

        #endregion
    }
}
