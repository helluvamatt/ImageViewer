using ImageViewer.Data.Models;
using ImageViewer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer.Controls
{
    // TODO Implement custom item sizing
    internal class ImageListView : Control
    {
        private readonly ImageListViewRowCollection _RowCollection;
        private readonly ImageListViewItemCollection _ItemCollection;

        private readonly SynchronizationContext _SyncContext;
        private readonly ConcurrentQueue<ImageListViewItem> _RenderQueue;
        private readonly ManualResetEventSlim _RenderTrigger;

        private readonly VScrollBar _VScrollBar;
        private readonly HScrollBar _HScrollBar;

        private int[] _SelectedIndices;
        private int _SelectedIndex = -1;
        private ContextMenuStrip _ContextMenu;
        private BindingListEx<ListItem> _DataSource;
        private Padding _ItemPadding;
        private int _ItemSpacingX;
        private int _ItemSpacingY;
        private Padding _TextPadding;
        private Size _ItemSize;
        private Point? _SelectionRegionStart;
        private Rectangle _SelectionRegion;
        private bool _DrawImageBorders;
        private Color _ImageBackColor;
        private Color _ImageBorderColor;
        private ViewMode _ViewMode;

        private Size _GallerySize;
        private Size _ScrollCanvasSize;

        public ImageListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.Selectable, true);
            _RowCollection = new ImageListViewRowCollection();
            _ItemCollection = new ImageListViewItemCollection();
            _RenderQueue = new ConcurrentQueue<ImageListViewItem>();
            _SyncContext = SynchronizationContext.Current;
            _RenderTrigger = new ManualResetEventSlim();
            _VScrollBar = new VScrollBar()
            {
                Dock = DockStyle.Right,
            };
            _VScrollBar.Scroll += OnScroll;
            _HScrollBar = new HScrollBar()
            {
                Dock = DockStyle.Bottom,
            };
            _HScrollBar.Scroll += OnScroll;

            Controls.Add(_HScrollBar);
            Controls.Add(_VScrollBar);
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingListEx<ListItem> DataSource
        {
            get => _DataSource;
            set
            {
                if (_DataSource != value)
                {
                    if (_DataSource != null)
                    {
                        _DataSource.ListChanged -= OnDataSourceListChanged;
                        _DataSource.ListItemRemoving -= OnDateSourceListItemRemoving;
                        _DataSource.ListClearing -= OnDataSourceListClearing;
                    }
                    _ItemCollection.Clear();
                    _DataSource = value;
                    
                    if (_DataSource != null)
                    {
                        _DataSource.ListChanged += OnDataSourceListChanged;
                        _DataSource.ListItemRemoving += OnDateSourceListItemRemoving;
                        _DataSource.ListClearing += OnDataSourceListClearing;
                        _ItemCollection.AddRange(_DataSource.Select(m => new ImageListViewItem(m)));
                        foreach (var item in _ItemCollection) QueueRender(item);
                    }
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImageModelSorter Sorter
        {
            get => _ItemCollection.Sorter;
            set
            {
                if (_ItemCollection.Sorter != value)
                {
                    if (_ItemCollection.Sorter != null) _ItemCollection.Sorter.PropertyChanged -= OnSorterPropertyChanged;
                    _ItemCollection.Sorter = value;
                    if (_ItemCollection.Sorter != null) _ItemCollection.Sorter.PropertyChanged += OnSorterPropertyChanged;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImageBrowser ImageBrowser { get; set; }

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
        
        public int ItemSpacingX
        {
            get => _ItemSpacingX;
            set
            {
                if (_ItemSpacingX != value)
                {
                    _ItemSpacingX = value;
                    Invalidate();
                }
            }
        }
        
        public int ItemSpacingY
        {
            get => _ItemSpacingY;
            set
            {
                if (_ItemSpacingY != value)
                {
                    _ItemSpacingY = value;
                    Invalidate();
                }
            }
        }
        
        public Padding TextPadding
        {
            get => _TextPadding;
            set
            {
                if (_TextPadding != value)
                {
                    _TextPadding = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        public Size ItemSize
        {
            get => _ItemSize;
            set
            {
                if (_ItemSize != value)
                {
                    _ItemSize = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        public bool DrawImageBorders
        {
            get => _DrawImageBorders;
            set
            {
                if (_DrawImageBorders != value)
                {
                    _DrawImageBorders = value;
                    RefreshImages();
                }
            }
        }

        public Color ImageBackColor
        {
            get => _ImageBackColor;
            set
            {
                if (_ImageBackColor != value)
                {
                    _ImageBackColor = value;
                    if (DrawImageBorders) RefreshImages();
                }
            }
        }

        public Color ImageBorderColor
        {
            get => _ImageBorderColor;
            set
            {
                if (_ImageBorderColor != value)
                {
                    _ImageBorderColor = value;
                    if (DrawImageBorders) RefreshImages();
                }
            }
        }

        #region ViewMode property

        public ViewMode ViewMode
        {
            get => _ViewMode;
            set
            {
                if (_ViewMode != value)
                {
                    _ViewMode = value;
                    OnViewModeChanged();
                }
            }
        }

        public event EventHandler ViewModeChanged;

        private void OnViewModeChanged()
        {
            PerformLayout();
            Invalidate();
            ViewModeChanged?.Invoke(this, EventArgs.Empty);

            if (ViewMode == ViewMode.Gallery) QueueGalleryRender();
        }

        public void SetViewMode(int viewMode)
        {
            if (Enum.IsDefined(typeof(ViewMode), viewMode)) ViewMode = (ViewMode)viewMode;
        }

        #endregion

        public event EventHandler ItemDoubleClicked;
        public event EventHandler Delete;
        public event EventHandler TagSelected;

        #region Selection handling

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => _SelectedIndex;
            set
            {
                if (_SelectedIndex != value)
                {
                    if (_SelectedIndex > -1 && _SelectedIndex < _ItemCollection.Count && _ItemCollection[_SelectedIndex].GalleryImage != null) _ItemCollection[_SelectedIndex].FreeGalleryImage();
                    _SelectedIndex = value;
                    OnSelectedIndexChanged();
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        private void OnSelectedIndexChanged()
        {
            var selectedItem = GetSelectedItem();
            var viewportRect = new Rectangle(-ScrollPosition.X, -ScrollPosition.Y, ClientRectangle.Width, ClientRectangle.Height);
            if (selectedItem != null && !viewportRect.Contains(selectedItem.Region))
            {
                if (selectedItem.Region.Bottom > viewportRect.Bottom)
                {
                    // Need to scroll down to get the image completely in view
                    viewportRect.Y = selectedItem.Region.Bottom - viewportRect.Height;
                }
                if (selectedItem.Region.Top < viewportRect.Top)
                {
                    // Need to scroll up to get the image completely in view
                    viewportRect.Y = selectedItem.Region.Top;
                }
                if (selectedItem.Region.Right > viewportRect.Right)
                {
                    // Need to scroll left to get the image completely in view
                    viewportRect.X = selectedItem.Region.Right - viewportRect.Width;
                }
                if (selectedItem.Region.Left < viewportRect.Left)
                {
                    // Need to scroll right to get the image completely in view
                    viewportRect.X = selectedItem.Region.Left;
                }
                ScrollPosition = new Point(-viewportRect.X, -viewportRect.Y);
            }
            Invalidate();

            if (selectedItem != null && ViewMode == ViewMode.Gallery) QueueGalleryRender(selectedItem);

            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] SelectedIndices
        {
            get => _SelectedIndices;
            set
            {
                if (_SelectedIndices != value)
                {
                    _SelectedIndices = value;
                    OnSelectedIndicesChanged();
                }
            }
        }

        public event EventHandler SelectedIndicesChanged;

        public void ClearSelection()
        {
            SelectedIndices = new int[0];
            SelectedIndex = -1;
        }

        public void SelectAll()
        {
            SelectedIndices = Enumerable.Range(0, _ItemCollection.Count).ToArray();
            SelectedIndex = 0;
        }

        public void InvertSelection()
        {
            SelectedIndices = Enumerable.Range(0, _ItemCollection.Count).Where(i => !SelectedIndices.Contains(i)).ToArray();
            SelectedIndex = SelectedIndices.Any() ? SelectedIndices.Min() : -1;
        }

        private void OnSelectedIndicesChanged()
        {
            SelectedIndicesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void AddToSelection(int index)
        {
            var currentSelection = new HashSet<int>(SelectedIndices ?? new int[0]);
            currentSelection.Add(index);
            SelectedIndices = currentSelection.ToArray();
        }

        private void ComputeSelectionFromRegion(Rectangle region, bool preserve)
        {
            var currentSelection = new HashSet<int>(preserve && SelectedIndices != null ? SelectedIndices : new int[0]);
            if (SelectedIndex > -1 && preserve) currentSelection.Add(SelectedIndex);
            for (int i = 0; i < _ItemCollection.Count; i++)
            {
                if (_ItemCollection[i].Region.IntersectsWith(region)) currentSelection.Add(i);
            }
            SelectedIndices = currentSelection.ToArray();
        }

        private void SetSelectedItem(ImageListViewItem item)
        {
            var index = _ItemCollection.IndexOf(item);
            SelectedIndices = index > -1 ? new int[] { index } : new int[0];
            SelectedIndex = index;
        }

        private ImageListViewItem GetSelectedItem() => SelectedIndex > -1 ? _ItemCollection[SelectedIndex] : null;

        private void NavigateSelection(Direction direction)
        {
            if (_ItemCollection.Count > 0)
            {
                int index = SelectedIndex;
                if (index < 0) index = 0;
                var item = _ItemCollection[index];
                switch (direction)
                {
                    case Direction.Up:
                        item = _RowCollection.FindNextCellUp(item);
                        break;
                    case Direction.Down:
                        item = _RowCollection.FindNextCellDown(item);
                        break;
                    case Direction.Right:
                        item = _RowCollection.FindNextCellRight(item, ViewMode == ViewMode.Gallery);
                        break;
                    case Direction.Left:
                        item = _RowCollection.FindNextCellLeft(item, ViewMode == ViewMode.Gallery);
                        break;
                }
                SetSelectedItem(item);
            }
        }

        #endregion

        #region Scroll handling

        public Size ViewportSize
        {
            get
            {
                var sz = ClientSize;
                if (_HScrollBar.Visible) sz.Height -= _HScrollBar.Height;
                if (_VScrollBar.Visible) sz.Width -= _VScrollBar.Width;
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
            if (_HScrollBar != null)
            {
                var oldState = _HScrollBar.Visible;
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
            if (_VScrollBar != null)
            {
                var oldState = _VScrollBar.Visible;
                if (ScrollCanvasSize.Height > ViewportSize.Height)
                {
                    _VScrollBar.LargeChange = ViewportSize.Height / 10;
                    _VScrollBar.SmallChange = ViewportSize.Height / 20;
                    _VScrollBar.Maximum = ScrollCanvasSize.Height - ViewportSize.Height + _VScrollBar.LargeChange - 1;
                    _VScrollBar.Visible = true;
                }
                else
                {
                    _VScrollBar.Visible = false;
                }
            }
        }

        private Point ScrollPosition
        {
            get => new Point(_HScrollBar != null && _HScrollBar.Visible ? -_HScrollBar.Value : 0, _VScrollBar != null && _VScrollBar.Visible ? -_VScrollBar.Value : 0);
            set
            {
                if (_HScrollBar != null && _HScrollBar.Value != value.X) _HScrollBar.Value = -value.X;
                if (_VScrollBar != null && _VScrollBar.Value != value.Y) _VScrollBar.Value = -value.Y;
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

        #region Form overrides

        #region Unused from Control

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => string.Empty;
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ContextMenu ContextMenu { get => null; set { } }

        #endregion

        public override ContextMenuStrip ContextMenuStrip { get => _ContextMenu; set => _ContextMenu = value; }
        
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!Focused) Focus();

            if (e.Button == MouseButtons.Left)
            {
                var pt = e.Location;
                pt.Y -= ScrollPosition.Y;
                pt.X -= ScrollPosition.X;
                _SelectionRegionStart = pt;
                _SelectionRegion = new Rectangle(_SelectionRegionStart.Value, Size.Empty);
                if (!ModifierKeys.HasFlag(Keys.Control))
                {
                    if (FindCellAt(e.Location, out int index))
                    {
                        SelectedIndices = new int[] { index };
                        SelectedIndex = index;
                    }
                }
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pt = e.Location;
            pt.Y -= ScrollPosition.Y;
            pt.X -= ScrollPosition.X;

            if (_SelectionRegionStart.HasValue)
            {
                int left = Math.Min(_SelectionRegionStart.Value.X, pt.X);
                int top = Math.Min(_SelectionRegionStart.Value.Y, pt.Y);
                int right = Math.Max(_SelectionRegionStart.Value.X, pt.X);
                int bottom = Math.Max(_SelectionRegionStart.Value.Y, pt.Y);
                _SelectionRegion = Rectangle.FromLTRB(left, top, right, bottom);
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_SelectionRegionStart.HasValue)
            {
                var pt = e.Location;
                pt.Y -= ScrollPosition.Y;
                pt.X -= ScrollPosition.X;
                int left = Math.Min(_SelectionRegionStart.Value.X, pt.X);
                int top = Math.Min(_SelectionRegionStart.Value.Y, pt.Y);
                int right = Math.Max(_SelectionRegionStart.Value.X, pt.X);
                int bottom = Math.Max(_SelectionRegionStart.Value.Y, pt.Y);
                ComputeSelectionFromRegion(Rectangle.FromLTRB(left, top, right, bottom), ModifierKeys.HasFlag(Keys.Control));
                _SelectionRegion = Rectangle.Empty;
                _SelectionRegionStart = null;
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (FindCellAt(e.Location, out int index))
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    AddToSelection(index);
                }
                SelectedIndex = index;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip?.Show(this, e.Location);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && FindCellAt(e.Location, out int index))
            {
                SelectedIndex = index;
                ItemDoubleClicked?.Invoke(this, e);
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_VScrollBar != null && _VScrollBar.Visible)
            {
                int deltaY = e.Delta * SystemInformation.MouseWheelScrollLines / 120 * 16; // 120 = WHEEL_DELTA constant, 16 = Text line height in pixels
                if (deltaY != 0)
                {
                    var scrollPosition = ScrollPosition;
                    scrollPosition.Y += deltaY;
                    if (scrollPosition.Y > 0) scrollPosition.Y = 0;
                    if (scrollPosition.Y < ViewportSize.Height - ScrollCanvasSize.Height) scrollPosition.Y = ViewportSize.Height - ScrollCanvasSize.Height;
                    ScrollPosition = scrollPosition;
                    Invalidate();
                }
            }
            base.OnMouseWheel(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    NavigateSelection(Direction.Up);
                    return true;
                case Keys.Down:
                    NavigateSelection(Direction.Down);
                    return true;
                case Keys.Right:
                    NavigateSelection(Direction.Right);
                    return true;
                case Keys.Left:
                    NavigateSelection(Direction.Left);
                    return true;
                case Keys.Delete:
                    Delete?.Invoke(this, EventArgs.Empty);
                    return true;
                case Keys.T:
                    TagSelected?.Invoke(this, EventArgs.Empty);
                    return true;
                case Keys.Enter:
                    ItemDoubleClicked?.Invoke(this, EventArgs.Empty);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Layout and Rendering

        protected override void OnLayout(LayoutEventArgs e)
        {
            _RowCollection.Clear();

            // Size is zero or no items, don't render anything
            if (_DataSource != null && _DataSource.Count > 0 && !ItemSize.IsEmpty)
            {
                using (var g = CreateGraphics()) // For measuring text
                {
                    var boldFont = new Font(Font, FontStyle.Bold);

                    if (ViewMode == ViewMode.Details)
                    {
                        // TODO Layout for details grid view
                    }
                    else if (ViewMode == ViewMode.Gallery)
                    {
                        var itemWidth = ItemSize.Width + ItemPadding.Horizontal;
                        var textWidth = itemWidth - TextPadding.Horizontal;

                        var rowItems = _ItemCollection.Sorted.ToList();
                        var maxItemHeight = (int)rowItems.Select(item => g.MeasureString(item.ListItem.PrimaryLabel, Font, textWidth).Height).Max() + ItemSize.Height + TextPadding.Vertical + ItemPadding.Vertical;
                        var totalWidth = rowItems.Count * itemWidth + (rowItems.Count + 1) * ItemSpacingX;

                        ScrollCanvasSize = new Size(totalWidth, maxItemHeight);

                        var cells = new ImageListViewItemCollection();
                        var left = ItemSpacingX;
                        var top = ViewportSize.Height - (maxItemHeight + ItemSpacingY);
                        foreach (var item in rowItems)
                        {
                            item.Region = new Rectangle(left, top, itemWidth, maxItemHeight);
                            cells.Add(item);
                            left += itemWidth + ItemSpacingX;
                        }
                        _RowCollection.Add(new ImageListViewRow(cells, new Rectangle(0, top, left, maxItemHeight)));

                        var newGallerySize = new Size(ViewportSize.Width - ItemPadding.Horizontal, top - ItemPadding.Vertical);
                        if (_GallerySize != newGallerySize)
                        {
                            _GallerySize = newGallerySize;
                            QueueGalleryRender();
                        }
                    }
                    else // ViewMode.Icons or ViewMode.Tiles
                    {
                        // First pass: Measure items to see if we need a vertical scrollbar
                        var itemWidth = (ViewMode == ViewMode.Tiles ? ItemSize.Width * 2 + ItemSize.Width / 2 + ItemSpacingX + TextPadding.Horizontal : ItemSize.Width) + ItemPadding.Horizontal;
                        var textWidth = (ViewMode == ViewMode.Tiles ? itemWidth - ItemSize.Width - ItemSpacingX - ItemPadding.Horizontal : itemWidth) - TextPadding.Horizontal;

                        // Determine cell count
                        int availableWidth = ViewportSize.Width - ItemSpacingX * 2;
                        int cellCount = availableWidth / (itemWidth + ItemSpacingX);
                        if (cellCount < 1) cellCount = 1; // May not fit, but we have to do something

                        // Determine item spacing from available width
                        int itemSpacingX = cellCount > 1 ? (availableWidth - (itemWidth * cellCount)) / (cellCount - 1) : 0;

                        int top = ItemSpacingY;
                        var rows = _ItemCollection.Sorted.Batch(cellCount);

                        // Measure rows
                        foreach (var rowItems in rows)
                        {
                            int maxItemHeight;
                            if (ViewMode == ViewMode.Tiles)
                            {
                                var maxTextHeight = rowItems.Select(item =>
                                {
                                    return Size.Ceiling(g.MeasureString(item.ListItem.PrimaryLabel, boldFont, textWidth)).Height
                                        + Size.Ceiling(g.MeasureString(item.ListItem.SecondaryLabel, Font, textWidth)).Height
                                        + Size.Ceiling(g.MeasureString(item.ListItem.TertiaryLabel, Font, textWidth)).Height
                                        + ItemSpacingY * 2;
                                }).Max();
                                maxItemHeight = Math.Max(ItemSize.Height + ItemPadding.Vertical, maxTextHeight + TextPadding.Vertical);
                            }
                            else
                            {
                                maxItemHeight = rowItems.Select(item => Size.Ceiling(g.MeasureString(item.ListItem.PrimaryLabel, Font, textWidth)).Height).Max();
                                maxItemHeight += ItemSize.Height + TextPadding.Vertical + ItemPadding.Vertical;
                            }
                            var cells = new ImageListViewItemCollection();
                            var left = ItemSpacingX;
                            foreach (var item in rowItems)
                            {
                                item.Region = new Rectangle(left, top, itemWidth, maxItemHeight);
                                cells.Add(item);
                                left += itemWidth + itemSpacingX;
                            }

                            _RowCollection.Add(new ImageListViewRow(cells, new Rectangle(0, top, left, maxItemHeight)));

                            top += maxItemHeight;
                            top += ItemSpacingY;
                        }

                        bool hasVScrollBar = _VScrollBar.Visible;
                        ScrollCanvasSize = new Size(itemWidth + ItemSpacingX * 2, top);

                        // Second pass: Available space may have changed
                        if (_VScrollBar.Visible != hasVScrollBar)
                        {
                            _RowCollection.Clear();

                            availableWidth = ViewportSize.Width - ItemSpacingX * 2;
                            cellCount = availableWidth / (itemWidth + ItemSpacingX);
                            if (cellCount < 1) cellCount = 1; // May not fit, but we have to do something

                            // Determine item spacing from available width
                            itemSpacingX = cellCount > 1 ? (availableWidth - (itemWidth * cellCount)) / (cellCount - 1) : 0;

                            top = ItemSpacingY;
                            rows = _ItemCollection.Sorted.Batch(cellCount);

                            // Measure rows
                            foreach (var rowItems in rows)
                            {
                                int maxItemHeight;
                                if (ViewMode == ViewMode.Tiles)
                                {
                                    var maxTextHeight = rowItems.Select(item =>
                                    {
                                        return Size.Ceiling(g.MeasureString(item.ListItem.PrimaryLabel, boldFont, textWidth)).Height
                                            + Size.Ceiling(g.MeasureString(item.ListItem.SecondaryLabel, Font, textWidth)).Height
                                            + Size.Ceiling(g.MeasureString(item.ListItem.TertiaryLabel, Font, textWidth)).Height
                                            + ItemSpacingY * 2;
                                    }).Max();
                                    maxItemHeight = Math.Max(ItemSize.Height + ItemPadding.Vertical, maxTextHeight + TextPadding.Vertical);
                                }
                                else
                                {
                                    maxItemHeight = rowItems.Select(item => Size.Ceiling(g.MeasureString(item.ListItem.PrimaryLabel, Font, textWidth)).Height).Max();
                                    maxItemHeight += ItemSize.Height + TextPadding.Vertical + ItemPadding.Vertical;
                                }
                                var cells = new ImageListViewItemCollection();
                                var left = ItemSpacingX;
                                foreach (var item in rowItems)
                                {
                                    item.Region = new Rectangle(left, top, itemWidth, maxItemHeight);
                                    cells.Add(item);
                                    left += itemWidth + itemSpacingX;
                                }

                                _RowCollection.Add(new ImageListViewRow(cells, new Rectangle(0, top, left, maxItemHeight)));

                                top += maxItemHeight;
                                top += ItemSpacingY;
                            }
                        }
                    }
                }
            }

            base.OnLayout(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var boldFont = new Font(Font, FontStyle.Bold);

            if (ViewMode == ViewMode.Details)
            {
                // TODO Paint for Details grid view
            }
            else
            {
                // Draw gallery if needed
                if (ViewMode == ViewMode.Gallery)
                {
                    var galleryRegion = new Rectangle(ItemPadding.Left, ItemPadding.Top, _GallerySize.Width, _GallerySize.Height);
                    var selectedItem = GetSelectedItem();
                    if (selectedItem == null)
                    {
                        e.Graphics.DrawStringVerticallyCentered(R.NoItemSelected, Font, ForeColor, galleryRegion, new StringFormat { Alignment = StringAlignment.Center });
                    }
                    else if (!string.IsNullOrEmpty(selectedItem.GalleryLoadError))
                    {
                        e.Graphics.DrawImageAndStringVerticallyCentered(selectedItem.GalleryLoadError, R.error_32, Font, ForeColor, galleryRegion);
                    }
                    else if (selectedItem.GalleryImage != null)
                    {
                        e.Graphics.DrawImageCentered(selectedItem.GalleryImage, galleryRegion);
                    }
                    else
                    {
                        e.Graphics.DrawImageAndStringVerticallyCentered(R.Loading, R.hourglass_32, Font, ForeColor, galleryRegion);
                    }
                }

                // Translate for scroll offset
                e.Graphics.TranslateTransform(ScrollPosition.X, ScrollPosition.Y);

                foreach (var row in _RowCollection)
                {
                    // Skip rows that are out of the visible region because of scrolling
                    if (row.Region.Bottom < -ScrollPosition.Y) continue;
                    if (row.Region.Y > -ScrollPosition.Y + ViewportSize.Height) continue;

                    foreach (var cell in row.Items)
                    {
                        // Skip cells that are out of the visible region because of scrolling
                        if (cell.Region.Right < -ScrollPosition.X) continue;
                        if (cell.Region.X > -ScrollPosition.X + ViewportSize.Width) continue;

                        var foreColor = ForeColor;
                        int index = _ItemCollection.IndexOf(cell);

                        if (SelectedIndex == index || (SelectedIndices != null && SelectedIndices.Contains(index)) || cell.Region.IntersectsWith(_SelectionRegion))
                        {
                            e.Graphics.FillRectangle(SystemBrushes.Highlight, cell.Region);
                            foreColor = SystemColors.HighlightText;
                        }

                        var imageRect = new Rectangle(new Point(cell.Region.X + ItemPadding.Left, cell.Region.Y + ItemPadding.Top), ItemSize);
                        if (!string.IsNullOrEmpty(cell.LoadError))
                        {
                            e.Graphics.DrawImageCentered(R.error_32, imageRect);
                        }
                        else if (cell.Icon != null && cell.Icon.Width > 0 && cell.Icon.Height > 0)
                        {
                            e.Graphics.DrawImageCentered(cell.Icon, imageRect);
                        }
                        else
                        {
                            e.Graphics.DrawImageCentered(R.hourglass_32, imageRect);
                        }

                        if (ViewMode == ViewMode.Tiles)
                        {
                            var textOffsetX = ItemSize.Width + ItemPadding.Horizontal + ItemSpacingX + TextPadding.Left;
                            var textX = cell.Region.X + textOffsetX;
                            var textWidth = cell.Region.Width - textOffsetX - TextPadding.Right;

                            var primarySize = Size.Ceiling(e.Graphics.MeasureString(cell.ListItem.PrimaryLabel, Font, textWidth));
                            var secondarySize = Size.Ceiling(e.Graphics.MeasureString(cell.ListItem.SecondaryLabel, Font, textWidth));
                            var tertiarySize = Size.Ceiling(e.Graphics.MeasureString(cell.ListItem.TertiaryLabel, Font, textWidth));

                            var textY = cell.Region.Y + (cell.Region.Height - (TextPadding.Vertical + primarySize.Height + secondarySize.Height + tertiarySize.Height + ItemSpacingY * 2)) / 2;

                            var primaryRect = new Rectangle(textX, textY, textWidth, primarySize.Height);
                            textY += primarySize.Height + ItemSpacingY;
                            var secondaryRect = new Rectangle(textX, textY, textWidth, secondarySize.Height);
                            textY += secondarySize.Height + ItemSpacingY;
                            var tertiaryRect = new Rectangle(textX, textY, textWidth, tertiarySize.Height);

                            e.Graphics.DrawString(cell.ListItem.PrimaryLabel, boldFont, new SolidBrush(foreColor), primaryRect);
                            e.Graphics.DrawString(cell.ListItem.SecondaryLabel, Font, new SolidBrush(foreColor), secondaryRect);
                            e.Graphics.DrawString(cell.ListItem.TertiaryLabel, Font, new SolidBrush(foreColor), tertiaryRect);
                        }
                        else
                        {
                            var imageHeight = ItemSize.Height + ItemPadding.Vertical;
                            var textRect = new Rectangle(cell.Region.X + TextPadding.Left, cell.Region.Y + imageHeight + TextPadding.Top, cell.Region.Width - TextPadding.Horizontal, cell.Region.Height - imageHeight - TextPadding.Vertical);
                            e.Graphics.DrawString(cell.ListItem.PrimaryLabel, Font, new SolidBrush(foreColor), textRect, new StringFormat { Alignment = StringAlignment.Center });
                        }

                        if (SelectedIndex == index && Focused)
                        {
                            var selectionRegion = new Rectangle(cell.Region.X - 1, cell.Region.Y - 1, cell.Region.Width + 1, cell.Region.Height + 1);
                            ControlPaint.DrawFocusRectangle(e.Graphics, selectionRegion, SystemColors.HighlightText, SystemColors.Highlight);
                        }
                    }
                }

                if (!_SelectionRegion.IsEmpty) ControlPaint.DrawFocusRectangle(e.Graphics, _SelectionRegion, ForeColor, BackColor);

                e.Graphics.ResetTransform();
            }
        }

        #endregion

        #endregion

        #region Event handlers

        private void OnSorterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PerformLayout();
            Invalidate();
        }

        private void OnDataSourceListClearing(object sender, CancelEventArgs e)
        {
            foreach (var item in _ItemCollection.Where(item => _DataSource.Contains(item.ListItem)))
            {
                item.Dispose();
            }
        }

        private void OnDateSourceListItemRemoving(object sender, ListItemRemovingEventArgs<ListItem> e)
        {
            _ItemCollection.FirstOrDefault(item => item.ListItem == e.OldItem)?.Dispose();
        }

        private void OnDataSourceListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    var newItem = new ImageListViewItem(_DataSource[e.NewIndex]);
                    _ItemCollection.Insert(e.NewIndex, newItem);
                    QueueRender(newItem);
                    break;
                case ListChangedType.ItemDeleted:
                    _ItemCollection.RemoveAt(e.NewIndex);
                    break;
                case ListChangedType.ItemChanged:
                    var changedItem = _ItemCollection[e.NewIndex];
                    changedItem.Dispose();
                    QueueRender(changedItem);
                    break;
                case ListChangedType.ItemMoved:
                    var moveItem = _ItemCollection[e.OldIndex];
                    _ItemCollection.RemoveAt(e.OldIndex);
                    _ItemCollection.Insert(e.NewIndex, moveItem);
                    break;
                default:
                    _ItemCollection.Clear();
                    _ItemCollection.AddRange(_DataSource.Select(m => new ImageListViewItem(m)));
                    foreach (var item in _ItemCollection) QueueRender(item);
                    break;
            }
            PerformLayout();
            Invalidate();
        }

        #endregion

        private bool FindCellAt(Point pt, out int index)
        {
            index = -1;
            pt.Y -= ScrollPosition.Y;
            pt.X -= ScrollPosition.X;
            foreach (var row in _RowCollection)
            {
                if (row.Region.Contains(pt))
                {
                    foreach (var cell in row.Items)
                    {
                        if (cell.Region.Contains(pt))
                        {
                            index = _ItemCollection.IndexOf(cell);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void RefreshImages()
        {
            foreach (var item in _ItemCollection)
            {
                if (item.ListItem is ImageListItem imageListItem)
                {
                    item.Dispose();
                    QueueRender(item);
                }
            }
        }

        #region Image rendering

        private void QueueRender(ImageListViewItem item)
        {
            _RenderQueue.Enqueue(item);
            if (!_RenderTrigger.IsSet)
            {
                _RenderTrigger.Set();
                Task.Run(new Action(DoRender));
            }
        }

        private void DoRender()
        {
            try
            {
                if (ImageBrowser == null) return; // Have to requeue later
                while (_RenderQueue.TryDequeue(out ImageListViewItem item))
                {
                    if (item.IsDisposed) continue;
                    if (item.ListItem is ImageListItem imageListItem)
                    {
                        using (var result = ImageBrowser.LoadImage(imageListItem.ImageModel, true))
                        {
                            if (string.IsNullOrEmpty(result.Error) && result.Image.Width > 0 && result.Image.Height > 0)
                            {
                                float ratio = Math.Min((float)ItemSize.Width / result.Image.Width, (float)ItemSize.Height / result.Image.Height);
                                if (ratio > 1) ratio = 1;
                                int width = (int)(result.Image.Width * ratio);
                                int height = (int)(result.Image.Height * ratio);
                                var rendered = new Bitmap(width, height);
                                using (var g = Graphics.FromImage(rendered))
                                {
                                    if (ratio < 1) g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    var bounds = new Rectangle(0, 0, width, height);
                                    if (DrawImageBorders)
                                    {
                                        g.FillRectangle(new SolidBrush(ImageBackColor), bounds);
                                        g.DrawRectangle(new Pen(ImageBorderColor), new Rectangle(bounds.Location, new Size(bounds.Width - 1, bounds.Height - 1)));
                                        g.DrawImage(result.Image, new Rectangle(ItemPadding.Left, ItemPadding.Top, width - ItemPadding.Horizontal, height - ItemPadding.Vertical));
                                    }
                                    else
                                    {
                                        g.DrawImage(result.Image, bounds);
                                    }
                                }
                                item.Icon = rendered;
                            }
                            else
                            {
                                item.LoadError = result.Error;
                            }
                        }
                    }
                    else if (item.ListItem is FolderListItem folderListItem)
                    {
                        var rendered = new Bitmap(ItemSize.Width, ItemSize.Height);
                        var paddingX = ItemSize.Width / 16;
                        var paddingY = ItemSize.Height / 16;
                        var spacingX = paddingX / 2;
                        var spacingY = paddingY / 2;
                        using (var g = Graphics.FromImage(rendered))
                        {
                            g.Clear(Color.Transparent);
                            var images = ImageBrowser.GetImagesInFolder(folderListItem.FolderPath, 4).ToList();

                            var region = new Rectangle(Point.Empty, ItemSize);
                            g.DrawSvg(R.folder, region);
                            region.Inflate(-paddingX, -paddingY);

                            for (int i = 0; i < images.Count && i < 4; i++)
                            {
                                using (var result = ImageBrowser.LoadImage(images[i], true))
                                {
                                    if (string.IsNullOrEmpty(result.Error))
                                    {
                                        var width = (region.Width - spacingX) / 2;
                                        var height = (region.Height - spacingY) / 2;
                                        int x = ((i & 1) == 1 ? width + spacingX : 0) + paddingX;
                                        int y = ((i >> 1) == 1 ? height + spacingY : 0) + paddingY;
                                        g.DrawImageZoomed(result.Image, new RectangleF(x, y, width, height));
                                    }
                                }
                            }
                        }
                        item.Icon = rendered;
                    }
                    _SyncContext.Post((state) => Invalidate(), null);
                }
            }
            finally
            {
                _RenderTrigger.Reset();
            }
        }

        private void QueueGalleryRender(ImageListViewItem item = null)
        {
            if (item == null) item = GetSelectedItem(); // Note that this sets item, this is deliberate
            if (item != null)
            {
                item.ResetGallery();
                Task.Run(new Action(() => DoGalleryRender(item)));
            }
        }

        private void DoGalleryRender(ImageListViewItem item)
        {
            if (item.IsGalleryDisposed) return;
            if (item.ListItem is ImageListItem imageListItem)
            {
                using (var result = ImageBrowser.LoadImage(imageListItem.ImageModel, false))
                {
                    if (string.IsNullOrEmpty(result.Error) && result.Image.Width > 0 && result.Image.Height > 0)
                    {
                        float ratio = Math.Min((float)_GallerySize.Width / result.Image.Width, (float)_GallerySize.Height / result.Image.Height);
                        if (ratio > 1) ratio = 1;
                        int width = (int)(result.Image.Width * ratio);
                        int height = (int)(result.Image.Height * ratio);
                        var rendered = new Bitmap(width, height);
                        using (var g = Graphics.FromImage(rendered))
                        {
                            if (ratio < 1) g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(result.Image, new Rectangle(0, 0, width, height));
                        }
                        if (!item.IsGalleryDisposed) item.GalleryImage = rendered;
                        else rendered.Dispose();
                    }
                    else if (!item.IsGalleryDisposed)
                    {
                        item.GalleryLoadError = result.Error;
                    }
                }
            }
            else if (item.ListItem is FolderListItem folderListItem)
            {
                var size = Math.Min(_GallerySize.Width, _GallerySize.Height);
                var rendered = new Bitmap(size, size);
                var padding = size / 16;
                var spacing = padding / 2;
                using (var g = Graphics.FromImage(rendered))
                {
                    g.Clear(Color.Transparent);
                    var images = ImageBrowser.GetImagesInFolder(folderListItem.FolderPath, 4).ToList();

                    var region = new Rectangle(Point.Empty, rendered.Size);
                    g.DrawSvg(R.folder, region);
                    region.Inflate(-padding, -padding);

                    for (int i = 0; i < images.Count && i < 4; i++)
                    {
                        using (var result = ImageBrowser.LoadImage(images[i], true))
                        {
                            if (string.IsNullOrEmpty(result.Error))
                            {
                                var width = (region.Width - spacing) / 2;
                                var height = (region.Height - spacing) / 2;
                                int x = ((i & 1) == 1 ? width + spacing : 0) + padding;
                                int y = ((i >> 1) == 1 ? height + spacing : 0) + padding;
                                g.DrawImageZoomed(result.Image, new RectangleF(x, y, width, height));
                            }
                        }
                    }
                }
                if (!item.IsGalleryDisposed) item.GalleryImage = rendered;
                else rendered.Dispose();
            }
            _SyncContext.Post((state) => Invalidate(), null);
        }

        #endregion

        private class ImageListViewRowCollection : List<ImageListViewRow>
        {
            public int CellCount => this.Sum(row => row.Items.Count);

            public ImageListViewItem FindNextCellUp(ImageListViewItem cell)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex) && rowIndex > 0)
                {
                    rowIndex--;
                    return this[rowIndex].Items[colIndex];
                }
                return cell;
            }

            public ImageListViewItem FindNextCellDown(ImageListViewItem cell)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex) && rowIndex < Count - 1)
                {
                    rowIndex++;
                    if (colIndex >= this[rowIndex].Items.Count) colIndex = this[rowIndex].Items.Count - 1;
                    return this[rowIndex].Items[colIndex];
                }
                return cell;
            }

            public ImageListViewItem FindNextCellLeft(ImageListViewItem cell, bool loopSameRow)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex))
                {
                    colIndex--;
                    if (colIndex < 0)
                    {
                        if (loopSameRow)
                        {
                            colIndex = this[rowIndex].Items.Count - 1;
                        }
                        else if (rowIndex > 0)
                        {
                            rowIndex--;
                            colIndex = this[rowIndex].Items.Count - 1;
                        }
                    }
                    return this[rowIndex].Items[colIndex];
                }
                return cell;
            }

            public ImageListViewItem FindNextCellRight(ImageListViewItem cell, bool loopSameRow)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex))
                {
                    colIndex++;
                    if (colIndex >= this[rowIndex].Items.Count)
                    {
                        if (loopSameRow)
                        {
                            colIndex = 0;
                        }
                        else if (rowIndex < Count - 1)
                        {
                            rowIndex++;
                            colIndex = 0;
                        }
                    }
                    return this[rowIndex].Items[colIndex];
                }
                return cell;
            }

            private bool FindItemOwner(ImageListViewItem item, out int rowIndex, out int colIndex)
            {
                for (int i = 0; i < Count; i++)
                {
                    int cellIndex = this[i].Items.IndexOf(item);
                    if (cellIndex > -1)
                    {
                        rowIndex = i;
                        colIndex = cellIndex;
                        return true;
                    }
                }
                rowIndex = -1;
                colIndex = -1;
                return false;
            }
        }

        private class ImageListViewItemCollection : List<ImageListViewItem>, IComparer<ImageListViewItem>
        {
            public ImageModelSorter Sorter { get; set; }
            
            public IEnumerable<ImageListViewItem> Sorted => this.OrderBy(item => item, this);

            public int Compare(ImageListViewItem x, ImageListViewItem y)
            {
                if (x.ListItem is ImageListItem imgItemX && y.ListItem is ImageListItem imgItemY)
                    return Sorter.Compare(imgItemX.ImageModel, imgItemY.ImageModel);
                else if (x.ListItem is FolderListItem folderItemX && y.ListItem is FolderListItem folderItemY)
                    return Sorter.Compare(new DirectoryInfo(folderItemX.FolderPath), new DirectoryInfo(folderItemY.FolderPath));
                else if (x.ListItem is FolderListItem _folderItemX && y.ListItem is ImageListItem _imgItemY)
                    return -1; // Folders always precede images
                else if (x.ListItem is ImageListItem _imgItemX && y.ListItem is FolderListItem _folderItemY)
                    return 1; // Folders always precede images
                return 0;
            }
        }

        private class ImageListViewRow
        {
            public ImageListViewRow(ImageListViewItemCollection items, Rectangle region)
            {
                Items = items;
                Region = region;
            }

            public ImageListViewItemCollection Items { get; }

            public Rectangle Region { get; }
        }

        private class ImageListViewItem : IDisposable
        {
            public ImageListViewItem(ListItem listItem)
            {
                ListItem = listItem ?? throw new ArgumentNullException(nameof(listItem));
            }

            public ListItem ListItem { get; }
            public Rectangle Region { get; set; }

            public Image Icon { get; set; }
            public string LoadError { get; set; }
            public bool IsDisposed { get; private set; }

            public Image GalleryImage { get; set; }
            public string GalleryLoadError { get; set; }
            public bool IsGalleryDisposed { get; private set; }
            
            public void ResetGallery()
            {
                GalleryLoadError = null;
                IsGalleryDisposed = false;
            }

            public void FreeGalleryImage()
            {
                IsGalleryDisposed = true;
                if (GalleryImage != null)
                {
                    GalleryImage.Dispose();
                    GalleryImage = null;
                }
            }

            public void Dispose()
            {
                IsDisposed = true;
                if (Icon != null)
                {
                    Icon.Dispose();
                    Icon = null;
                }
                FreeGalleryImage();
            }
        }

        private enum Direction { Up, Down, Left, Right }
    }

    internal enum ViewMode { Icons, Tiles, Details, Gallery }

    internal abstract class ListItem : IEquatable<ListItem>
    {
        public abstract string PrimaryLabel { get; }

        public abstract string SecondaryLabel { get; }

        public abstract string TertiaryLabel { get; }

        public abstract bool Equals(ListItem other);

        public override abstract int GetHashCode();

        public override bool Equals(object obj) => obj is ListItem listItem && Equals(listItem);
    }

    internal class ImageListItem : ListItem
    {
        public ImageListItem(ImageModel imageModel)
        {
            ImageModel = imageModel;
        }

        public override string PrimaryLabel => ImageModel.Title;

        public override string SecondaryLabel => FileUtils.GetFileSizeString(ImageModel.FileSize);

        public override string TertiaryLabel => ImageModel.FileModifiedDate.ToString();

        public ImageModel ImageModel { get; }

        public override bool Equals(ListItem other) => other is ImageListItem imageListItem && imageListItem.ImageModel == ImageModel;

        public override int GetHashCode() => ImageModel.GetHashCode();
    }

    internal class FolderListItem : ListItem
    {
        public FolderListItem(string path, int imageCount, int folderCount, DateTime lastModified)
        {
            FolderPath = path;

            if (imageCount > 0 && folderCount > 0) SecondaryLabel = string.Format(R.ImageAndFolderCount, imageCount, folderCount);
            else if (imageCount > 0) SecondaryLabel = string.Format(R.ImageCount, imageCount);
            else if (folderCount > 0) SecondaryLabel = string.Format(R.FolderCount, folderCount);
            else SecondaryLabel = R.Empty;

            _LastModified = lastModified;
        }
        
        private readonly DateTime _LastModified;

        public override string PrimaryLabel => Path.GetFileName(FolderPath);

        public override string SecondaryLabel { get; }

        public override string TertiaryLabel => _LastModified.ToString();

        public string FolderPath { get; }

        public override bool Equals(ListItem other) => other is FolderListItem folderListItem && folderListItem.FolderPath == FolderPath;

        public override int GetHashCode() => FolderPath.GetHashCode();
    }

    internal class ItemDoubleClickedEventArgs : MouseEventArgs
    {
        public ItemDoubleClickedEventArgs(MouseEventArgs e, ImageModel m) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            ImageModel = m;
        }

        public ImageModel ImageModel { get; }
    }
}
