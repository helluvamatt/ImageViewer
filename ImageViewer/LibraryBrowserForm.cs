using ImageViewer.Models;
using ImageViewer.Controls;
using ImageViewer.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer
{
    internal partial class LibraryBrowserForm : Form, IToolStripForm
    {
        private readonly ImageBrowser _ImageBrowser;
        private readonly BindingListEx<TagModel> _Tags;
        private readonly BindingListEx<ListItem> _Images;
        private readonly ImageModelSorter _Sorter;
        private readonly BrowseHistory _History;

        private FullscreenForm _FullscreenForm;
        private bool _SyncingNav;

        public LibraryBrowserForm(ImageBrowser imageBrowser)
        {
            InitializeComponent();
            _ImageBrowser = imageBrowser;
            _Tags = new BindingListEx<TagModel>();
            _Images = new BindingListEx<ListItem>();

            _Sorter = new ImageModelSorter();
            _History = new BrowseHistory(_ImageBrowser);

            _ImageBrowser.ImageAdded += OnImageAdded;
            _ImageBrowser.ImageRemoved += OnImageRemoved;
            _ImageBrowser.LibraryPathAdded += OnLibraryPathAdded;
            _ImageBrowser.LibraryPathRemoved += OnLibraryPathRemoved;
            _ImageBrowser.TagChanged += OnTagChanged;
            _ImageBrowser.DatabaseReset += OnDatabaseReset;

            _History.CurrentPageChanged += OnHistoryCurrentPageChanged;
            _History.BackEnabledChanged += (sender, e) => btnBrowseBack.Enabled = _History.BackEnabled;
            _History.ForwardEnabledChanged += (sender, e) => btnBrowseForward.Enabled = _History.ForwardEnabled;
            _History.UpEnabledChanged += (sender, e) => btnBrowseUp.Enabled = _History.UpEnabled;

            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;

            _Sorter.PropertyChanged += OnSorterPropertyChanged;

            btnBrowseBack.Enabled = _History.BackEnabled;
            btnBrowseForward.Enabled = _History.ForwardEnabled;
            btnBrowseUp.Enabled = _History.UpEnabled;

            btnFullscreen.Enabled = _History.CurrentPage != null;

            btnTagSelected.Enabled = btnDelete.Enabled = menuItemAddTag.Enabled = menuItemDelete.Enabled = HasSecondarySelection;
            btnInformation.Enabled = HasPrimarySelection;

            imageListView.DataSource = _Images;
            imageListView.Sorter = _Sorter;
            imageListView.ImageBrowser = _ImageBrowser;

            imageListView.ImageBackColor = Settings.Default.LibraryBrowserImageBackColor;
            imageListView.ImageBorderColor = Settings.Default.LibraryBrowserImageBorderColor;
            imageListView.DrawImageBorders = Settings.Default.LibraryBrowserDrawImageBorder;

            menuItemViewIcons.Checked = Settings.Default.LibraryBrowserViewMode == ViewMode.Icons;
            menuItemViewTiles.Checked = Settings.Default.LibraryBrowserViewMode == ViewMode.Tiles;
            menuItemViewDetails.Checked = Settings.Default.LibraryBrowserViewMode == ViewMode.Details;
            menuItemViewGallery.Checked = Settings.Default.LibraryBrowserViewMode == ViewMode.Gallery;

            _Sorter.SetSort(Sort.Name, SortOrder.Ascending);

            tagModelBindingSource.DataSource = _Tags;
        }

        public event EventHandler<ImageEventArgs> OpenImage;
        public event EventHandler<ImageEventArgs> OpenImageInformation;
        public event EventHandler ManageTags;

        public ToolStrip ToolStrip => toolStrip;

        #region Selection handling

        public bool HasPrimarySelection => imageListView.SelectedIndex > -1 && imageListView.SelectedIndex < _Images.Count;
        public bool HasSecondarySelection => imageListView.SelectedIndices != null && imageListView.SelectedIndices.Length > 0;

        private IEnumerable<ListItem> GetSecondarySelection() => imageListView.SelectedIndices.Select(i => _Images[i]);

        public ImageModel SelectedImage
        {
            get
            {
                var selected = HasPrimarySelection ? _Images[imageListView.SelectedIndex] : null;
                return selected is ImageListItem imageListItem ? imageListItem.ImageModel : null;
            }
        }

        public event EventHandler SelectedImageChanged;

        #endregion

        public void StartSlideshow()
        {
            if (_History.CurrentPage != null)
            {
                if (_FullscreenForm == null)
                {
                    _FullscreenForm = new FullscreenForm(_ImageBrowser);
                    _FullscreenForm.HistoryPage = _History.CurrentPage;
                    _FullscreenForm.FormClosed += OnFullscreenFormClosed;
                }
                _FullscreenForm.DesktopBounds = Screen.FromControl(this).Bounds;
                _FullscreenForm.Show();
            }
        }

        #region Form overrides

        protected override void OnLoad(EventArgs e)
        {
            Icon = Icon?.Clone() as Icon;
            base.OnLoad(e);
            
            foreach (var path in _ImageBrowser.LibraryPaths)
            {
                var newNode = treeViewFolders.Nodes.Add(path, path, 0);
                newNode.Nodes.Add(R.Loading);
            }

            LoadTags();

            if (treeViewFolders.Nodes.Count > 0) treeViewFolders.SelectedNode = treeViewFolders.Nodes[0];
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _History.Dispose();
            _ImageBrowser.ImageAdded -= OnImageAdded;
            _ImageBrowser.ImageRemoved -= OnImageRemoved;
            _ImageBrowser.LibraryPathAdded -= OnLibraryPathAdded;
            _ImageBrowser.LibraryPathRemoved -= OnLibraryPathRemoved;
            _ImageBrowser.TagChanged -= OnTagChanged;
            _ImageBrowser.DatabaseReset -= OnDatabaseReset;
            base.OnFormClosed(e);
        }

        #endregion

        #region Event handlers

        private void OnSettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nameof(Settings.LibraryBrowserDrawImageBorder) == e.PropertyName)
            {
                imageListView.DrawImageBorders = Settings.Default.LibraryBrowserDrawImageBorder;
            }
            else if (nameof(Settings.LibraryBrowserImageBackColor) == e.PropertyName)
            {
                imageListView.ImageBackColor = Settings.Default.LibraryBrowserImageBackColor;
            }
            else if (nameof(Settings.LibraryBrowserImageBorderColor) == e.PropertyName)
            {
                imageListView.ImageBorderColor = Settings.Default.LibraryBrowserImageBorderColor;
            }
        }

        private void OnSorterPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string[] props = e.PropertyName.Split(',');
            foreach (var prop in props)
            {
                if (nameof(ImageModelSorter.Order) == prop)
                {
                    btnSortAsc.Checked = _Sorter.Order == SortOrder.Ascending;
                    btnSortDesc.Checked = _Sorter.Order == SortOrder.Descending;
                }
                if (nameof(ImageModelSorter.OrderBy) == prop)
                {
                    menuItemSortByName.Checked = _Sorter.OrderBy == Sort.Name;
                    menuItemSortByFileSize.Checked = _Sorter.OrderBy == Sort.FileSize;
                    menuItemSortByModifiedDate.Checked = _Sorter.OrderBy == Sort.ModifiedDate;
                    menuItemSortByCreatedDate.Checked = _Sorter.OrderBy == Sort.CreatedDate;
                    menuItemSortByImageSize.Checked = _Sorter.OrderBy == Sort.ImageSize;
                }
            }
        }

        private void OnViewModeChanged(object sender, EventArgs e)
        {
            menuItemViewIcons.Checked = imageListView.ViewMode == ViewMode.Icons;
            menuItemViewTiles.Checked = imageListView.ViewMode == ViewMode.Tiles;
            menuItemViewDetails.Checked = imageListView.ViewMode == ViewMode.Details;
            menuItemViewGallery.Checked = imageListView.ViewMode == ViewMode.Gallery;
        }

        private void OnSelectNoneClick(object sender, EventArgs e)
        {
            imageListView.ClearSelection();
        }

        private void OnSelectAllClick(object sender, EventArgs e)
        {
            imageListView.SelectAll();
        }

        private void OnInvertSelectionClick(object sender, EventArgs e)
        {
            imageListView.InvertSelection();
        }

        private void OnImageRemoved(object sender, ImageRemovedEventArgs e)
        {
            if (_History.CurrentPage != null && _History.CurrentPage.IsIncluded(_ImageBrowser, e.Image))
            {
                _Images.MergeItems(_History.CurrentPage.GetListItems(_ImageBrowser));
            }
        }

        private void OnImageAdded(object sender, ImageEventArgs e)
        {
            if (_History.CurrentPage != null && _History.CurrentPage.IsIncluded(_ImageBrowser, e.Image))
            {
                _Images.MergeItems(_History.CurrentPage.GetListItems(_ImageBrowser));
            }
        }

        private void OnTagChanged(object sender, TagEventArgs e)
        {
            LoadTags();
        }

        private void OnDatabaseReset(object sender, EventArgs e)
        {
            _History.Clear();
            _Tags.Clear();
            _Images.Clear();
            treeViewFolders.CollapseAll();
        }

        private void OnHistoryCurrentPageChanged(object sender, EventArgs e)
        {
            if (_History.CurrentPage != null)
            {
                btnFullscreen.Enabled = true;
                _Images.SetItems(_History.CurrentPage.GetListItems(_ImageBrowser));
            }
            else
            {
                btnFullscreen.Enabled = false;
            }

            if (Settings.Default.LibraryBrowserSyncNav)
            {
                try
                {
                    _SyncingNav = true;
                    if (_History.CurrentPage is BrowseHistoryFolderPage folderPage)
                    {
                        tabControl.SelectedTab = tabPageFolders;

                        TreeNode current = treeViewFolders.Nodes.Cast<TreeNode>().FirstOrDefault(n => folderPage.FolderPath.StartsWith(n.Name));
                        while (current != null)
                        {
                            if (current.Name == folderPage.FolderPath)
                            {
                                treeViewFolders.SelectedNode = current;
                                break;
                            }
                            current.Expand();
                            current = current.Nodes.Cast<TreeNode>().FirstOrDefault(n => folderPage.FolderPath.StartsWith(n.Name));
                        }
                    }
                    else if (_History.CurrentPage is BrowseHistoryTagPage tagPage)
                    {
                        tabControl.SelectedTab = tabPageTags;
                        tagModelBindingSource.Position = _Tags.IndexOf(tagPage.Tag);
                    }
                }
                finally
                {
                    _SyncingNav = false;
                }
            }
        }

        private void OnBrowseUpClick(object sender, EventArgs e)
        {
            _History.GoUp();
        }

        private void OnBrowseReloadClick(object sender, EventArgs e)
        {
            _History.Reload();
        }

        private void OnBrowseBackClick(object sender, EventArgs e)
        {
            _History.GoBack();
        }

        private void OnBrowseForwardClick(object sender, EventArgs e)
        {
            _History.GoForward();
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            NavigateToItem();
        }

        private void OnInformationClick(object sender, EventArgs e)
        {
            if (HasPrimarySelection && _Images[imageListView.SelectedIndex] is ImageListItem imageListItem)
            {
                OpenImageInformation?.Invoke(this, new ImageEventArgs(imageListItem.ImageModel));
            }
        }

        private void OnFullscreenClick(object sender, EventArgs e)
        {
            StartSlideshow();
        }

        private void OnRemoveClick(object sender, EventArgs e)
        {
            if (HasSecondarySelection && MessageBox.Show(this, R.AreYouSureDeleteImage, R.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var selection = GetSecondarySelection().ToList();
                foreach (var imgItem in selection.OfType<ImageListItem>())
                {
                    _ImageBrowser.RemoveImage(imgItem.ImageModel);
                }
                foreach (var folderItem in selection.OfType<FolderListItem>())
                {
                    _ImageBrowser.RemoveImagesRecursive(folderItem.FolderPath);
                }
                _History.Reload();
            }
        }

        private void OnTabSelected(object sender, TabControlEventArgs e)
        {
            if (!_SyncingNav && e.TabPage == tabPageFolders && treeViewFolders.Nodes.Count > 0 && treeViewFolders.SelectedNode == null)
            {
                treeViewFolders.SelectedNode = treeViewFolders.Nodes[0];
            }
        }

        private void OnImageViewItemDoubleClicked(object sender, EventArgs e)
        {
            NavigateToItem();
        }

        private void OnImageListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            btnInformation.Enabled = HasPrimarySelection;
            SelectedImageChanged?.Invoke(this, new ImageEventArgs(SelectedImage));
        }

        private void OnImageListViewSelectedIndicesChanged(object sender, EventArgs e)
        {
            btnTagSelected.Enabled = btnDelete.Enabled = menuItemAddTag.Enabled = menuItemDelete.Enabled = HasSecondarySelection;
        }

        private void OnSortAscClick(object sender, EventArgs e)
        {
            _Sorter.Order = SortOrder.Ascending;
        }

        private void OnSortDescClick(object sender, EventArgs e)
        {
            _Sorter.Order = SortOrder.Descending;
        }

        private void OnSortByNameClick(object sender, EventArgs e)
        {
            _Sorter.OrderBy = Sort.Name;
        }

        private void OnSortByFileSizeClick(object sender, EventArgs e)
        {
            _Sorter.OrderBy = Sort.FileSize;
        }

        private void OnSortByModifiedDateClick(object sender, EventArgs e)
        {
            _Sorter.OrderBy = Sort.ModifiedDate;
        }

        private void OnSortByCreatedDateClick(object sender, EventArgs e)
        {
            _Sorter.OrderBy = Sort.CreatedDate;
        }

        private void OnSortByImageSizeClick(object sender, EventArgs e)
        {
            _Sorter.OrderBy = Sort.ImageSize;
        }

        private void OnViewIconsClick(object sender, EventArgs e)
        {
            imageListView.ViewMode = ViewMode.Icons;
        }

        private void OnViewTilesClick(object sender, EventArgs e)
        {
            imageListView.ViewMode = ViewMode.Tiles;
        }

        private void OnViewDetailsClick(object sender, EventArgs e)
        {
            imageListView.ViewMode = ViewMode.Details;
        }

        private void OnViewGalleryClick(object sender, EventArgs e)
        {
            imageListView.ViewMode = ViewMode.Gallery;
        }

        private void OnTagSelectedClick(object sender, EventArgs e)
        {
            if (imageListView.SelectedIndices != null && imageListView.SelectedIndices.Length > 0)
            {
                var dlg = new ImageTagForm(_ImageBrowser);
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedItem != null)
                {
                    foreach (var imageListItem in imageListView.SelectedIndices.Select(i => _Images[i]).OfType<ImageListItem>())
                    {
                        _ImageBrowser.AddImageTag(imageListItem.ImageModel, dlg.SelectedItem);
                    }
                }
            }
        }

        private void OnTagsListDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < tagModelBindingSource.Count && tagModelBindingSource[e.Index] is TagModel tag)
            {
                e.Graphics.DrawTagListItem(tag.Name, e.Font, Color.FromArgb(tag.Color), e.Bounds, listBoxTags.ItemHeight, true);
            }
            e.DrawFocusRectangle();
        }

        private void OnTabControlDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1 && e.Index < tabControl.TabPages.Count)
            {
                var img = tabControl.TabPages[e.Index].Tag is string r ? R.ResourceManager.GetObject(r) as Bitmap : null;
                e.Graphics.DrawListItem(img, tabControl.TabPages[e.Index].Text, e.Font, e.ForeColor, e.Bounds);
            }
        }

        private void OnManageTagsClick(object sender, EventArgs e)
        {
            ManageTags?.Invoke(this, EventArgs.Empty);
        }

        private void OnTagListPositionChanged(object sender, EventArgs e)
        {
            if (!_SyncingNav) _History.NavigateTo(new BrowseHistoryTagPage(_Tags[tagModelBindingSource.Position]));
        }

        private void OnLibraryPathAdded(object sender, PathEventArgs e)
        {
            var newNode = treeViewFolders.Nodes.Add(e.Path, e.Path, 0);
            newNode.Nodes.Add(R.Loading);
        }

        private void OnLibraryPathRemoved(object sender, PathEventArgs e)
        {
            treeViewFolders.Nodes.RemoveByKey(e.Path);
        }

        private void OnFolderBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ExpandFolderNode(e.Node);
        }

        private void OnFolderAfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add(R.Loading);
        }

        private void OnFolderRefreshClick(object sender, EventArgs e)
        {
            if (treeViewFolders.SelectedNode != null && !string.IsNullOrEmpty(treeViewFolders.SelectedNode.Name))
            {
                ExpandFolderNode(treeViewFolders.SelectedNode);
            }
        }

        private void OnFolderAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_SyncingNav) _History.NavigateTo(new BrowseHistoryFolderPage(e.Node.Name));
        }

        private void OnFolderDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null) return;
            
            var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            var unfocused = !e.Node.TreeView.Focused;

            if (selected && unfocused)
            {
                var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void OnFullscreenFormClosed(object sender, FormClosedEventArgs e)
        {
            _FullscreenForm = null;
        }

        #endregion

        private void ExpandFolderNode(TreeNode node)
        {
            node.ImageIndex = 1;
            var doExpand = new Action(() =>
            {
                var subFolders = _ImageBrowser.GetFolders(node.Name).ToList();
                Invoke(new Action(() =>
                {
                    node.Nodes.Clear();
                    if (subFolders.Count > 0)
                    {
                        foreach (var path in subFolders)
                        {
                            var newNode = node.Nodes.Add(path, Path.GetFileName(path), 0);
                            newNode.Nodes.Add(R.Loading);
                        }
                    }
                    node.ImageIndex = 0;
                }));
            });
            if (_SyncingNav) doExpand();
            else Task.Run(doExpand);
        }

        private void NavigateToItem()
        {
            if (HasPrimarySelection)
            {
                if (_Images[imageListView.SelectedIndex] is ImageListItem imageListItem)
                {
                    OnOpenImage(imageListItem.ImageModel);
                }
                else if (_Images[imageListView.SelectedIndex] is FolderListItem folderListItem)
                {
                    _History.NavigateTo(new BrowseHistoryFolderPage(folderListItem.FolderPath));
                }
            }
        }

        private void OnOpenImage(ImageModel imageModel)
        {
            OpenImage?.Invoke(this, new ImageEventArgs(imageModel));
        }

        private void LoadTags()
        {
            listBoxTags.UseWaitCursor = true;

            _SyncingNav = true;
            _Tags.Clear();
            _SyncingNav = false;

            Task.Run(() =>
            {
                var tags = _ImageBrowser.GetTags().ToList();
                Invoke(new Action(() =>
                {
                    try
                    {
                        _SyncingNav = true;
                        _Tags.SetItems(tags);
                    }
                    finally
                    {
                        listBoxTags.UseWaitCursor = false;
                        _SyncingNav = false;
                    }
                }));
            });
        }
    }
}
