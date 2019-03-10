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
    // TODO Implement other view modes: Tiles, Details (with clickable headers)
    // TODO Implement custom item sizing
    internal class ImageListView : ScrollableControl
    {
        private readonly ImageListViewRowCollection _RowCollection;
        private readonly ImageListViewItemCollection _ItemCollection;

        private readonly ConcurrentQueue<ImageListViewItem> _RenderQueue;
        private readonly SynchronizationContext _SyncContext;
        private readonly ManualResetEventSlim _RenderTrigger;

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

        public ImageListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.Selectable, true);
            _RowCollection = new ImageListViewRowCollection();
            _ItemCollection = new ImageListViewItemCollection();
            _RenderQueue = new ConcurrentQueue<ImageListViewItem>();
            _SyncContext = SynchronizationContext.Current;
            _RenderTrigger = new ManualResetEventSlim();
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
                    AutoScrollMargin = new Size(ItemSpacingX, ItemSpacingY);
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
                    AutoScrollMargin = new Size(ItemSpacingX, ItemSpacingY);
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
                    _SelectedIndex = value;
                    OnSelectedIndexChanged();
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        private void OnSelectedIndexChanged()
        {
            var selectedItem = SelectedIndex > -1 ? _ItemCollection[SelectedIndex] : null;
            var viewportRect = new Rectangle(-AutoScrollPosition.X, -AutoScrollPosition.Y, ClientRectangle.Width, ClientRectangle.Height);
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
                AutoScrollPosition = new Point(viewportRect.X, viewportRect.Y);
            }
            Invalidate();
            
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
                        item = _RowCollection.FindNextCellRight(item);
                        break;
                    case Direction.Left:
                        item = _RowCollection.FindNextCellLeft(item);
                        break;
                }
                SetSelectedItem(item);
            }
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
                pt.Y -= AutoScrollPosition.Y;
                _SelectionRegionStart = pt;
                _SelectionRegion = new Rectangle(_SelectionRegionStart.Value, Size.Empty);
                if (!ModifierKeys.HasFlag(Keys.Control))
                {
                    ClearSelection();
                }
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pt = e.Location;
            pt.Y -= AutoScrollPosition.Y;

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
                pt.Y -= AutoScrollPosition.Y;
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
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        #region Layout and Rendering

        protected override void OnLayout(LayoutEventArgs e)
        {
            using (var g = CreateGraphics()) // For measuring text
            {
                _RowCollection.Clear();

                // Size is zero or no items, don't render anything
                if (_DataSource == null || ItemSize.IsEmpty) return;

                var itemWidth = ItemSize.Width + ItemPadding.Horizontal;
                var textWidth = itemWidth - TextPadding.Horizontal;

                // Determine cell count
                int availableWidth = ClientSize.Width - ItemSpacingX * 2;
                int cellCount = availableWidth / (itemWidth + ItemSpacingX);
                if (cellCount < 1) cellCount = 1; // May not fit, but we have to do something

                // Determine item spacing from available width
                int itemSpacingX = cellCount > 1 ? (availableWidth - (itemWidth * cellCount)) / (cellCount - 1) : 0;

                int top = ItemSpacingY;
                var rows = _ItemCollection.Sorted.Batch(cellCount);

                // Add item rows
                foreach (var rowItems in rows)
                {
                    var maxItemHeight = (int)rowItems.Select(item => g.MeasureString(item.ListItem.Label, Font, textWidth).Height).Max() + ItemSize.Height + TextPadding.Vertical + ItemPadding.Vertical;
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
                
                AutoScrollMinSize = new Size(itemWidth, top);
            }

            base.OnLayout(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Translate for scroll offset
            e.Graphics.TranslateTransform(0, AutoScrollPosition.Y);

            var dashPen = new Pen(SystemColors.GrayText, 1) { DashStyle = DashStyle.Dot };

            foreach (var row in _RowCollection)
            {
                // Skip rows that are out of the visible region because of scrolling
                if (row.Region.Bottom < -AutoScrollPosition.Y) continue;
                if (row.Region.Y > -AutoScrollPosition.Y + ClientSize.Height) continue;

                foreach (var cell in row.Items)
                {
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

                    var imageHeight = ItemSize.Height + ItemPadding.Vertical;
                    var textRect = new Rectangle(cell.Region.X + TextPadding.Left, cell.Region.Y + imageHeight + TextPadding.Top, cell.Region.Width - TextPadding.Horizontal, cell.Region.Height - imageHeight - TextPadding.Vertical);
                    e.Graphics.DrawString(cell.ListItem.Label, Font, new SolidBrush(foreColor), textRect, new StringFormat { Alignment = StringAlignment.Center });
                    
                    if (SelectedIndex == index && Focused)
                    {
                        var selectionRegion = new Rectangle(cell.Region.X - 1, cell.Region.Y - 1, cell.Region.Width + 1, cell.Region.Height + 1);
                        e.Graphics.DrawRectangle(dashPen, selectionRegion);
                    }
                }
            }

            if (!_SelectionRegion.IsEmpty) e.Graphics.DrawRectangle(dashPen, _SelectionRegion);

            e.Graphics.ResetTransform();
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
            pt.Y -= AutoScrollPosition.Y;
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
                        using (var g = Graphics.FromImage(rendered))
                        {
                            g.Clear(Color.Transparent);
                            var images = ImageBrowser.GetImagesInFolder(folderListItem.FolderPath, 4).ToList();

                            var region = new Rectangle(Point.Empty, ItemSize);
                            g.DrawSvg(R.folder, region);
                            region.Inflate(-6, -6);

                            for (int i = 0; i < images.Count && i < 4; i++)
                            {
                                using (var result = ImageBrowser.LoadImage(images[i], true))
                                {
                                    if (string.IsNullOrEmpty(result.Error))
                                    {
                                        var width = (region.Width - 2) / 2;
                                        var height = (region.Height - 2) / 2;
                                        int x = ((i & 1) == 1 ? width + 2 : 0) + 6;
                                        int y = ((i >> 1) == 1 ? height + 2 : 0) + 6;
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

            public ImageListViewItem FindNextCellLeft(ImageListViewItem cell)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex))
                {
                    if (--colIndex < 0 && rowIndex > 0)
                    {
                        rowIndex--;
                        colIndex = this[rowIndex].Items.Count - 1;
                    }
                    return this[rowIndex].Items[colIndex];
                }
                return cell;
            }

            public ImageListViewItem FindNextCellRight(ImageListViewItem cell)
            {
                if (FindItemOwner(cell, out int rowIndex, out int colIndex))
                {
                    if (++colIndex >= this[rowIndex].Items.Count && rowIndex < Count - 1)
                    {
                        rowIndex++;
                        colIndex = 0;
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
                ListItem = listItem;
            }

            public ListItem ListItem { get; }

            public Image Icon { get; set; }

            public string LoadError { get; set; }

            public Rectangle Region { get; set; }

            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
                if (Icon != null)
                {
                    Icon.Dispose();
                    Icon = null;
                }
            }
        }

        private enum Direction { Up, Down, Left, Right }
    }

    internal abstract class ListItem : IEquatable<ListItem>
    {
        public abstract string Label { get; }

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

        public override string Label => ImageModel.Title;

        public ImageModel ImageModel { get; }

        public override bool Equals(ListItem other) => other is ImageListItem imageListItem && imageListItem.ImageModel == ImageModel;

        public override int GetHashCode() => ImageModel.GetHashCode();
    }

    internal class FolderListItem : ListItem
    {
        public FolderListItem(string path)
        {
            FolderPath = path;
        }

        public override string Label => Path.GetFileName(FolderPath);

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
