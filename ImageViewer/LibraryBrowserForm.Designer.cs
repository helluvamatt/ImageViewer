namespace ImageViewer
{
    partial class LibraryBrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryBrowserForm));
            this.treeViewFolders = new System.Windows.Forms.TreeView();
            this.contextMenuStripFolders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemFoldersRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.folderImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageFolders = new System.Windows.Forms.TabPage();
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.listBoxTags = new ImageViewer.Controls.ListBoxEx();
            this.tagModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageListView = new ImageViewer.Controls.ImageListView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemInvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAddTag = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemViewIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewTiles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSlideshow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblBrowserPath = new System.Windows.Forms.ToolStripLabel();
            this.btnBrowseUp = new System.Windows.Forms.ToolStripButton();
            this.btnBrowseReload = new System.Windows.Forms.ToolStripButton();
            this.btnBrowseBack = new System.Windows.Forms.ToolStripButton();
            this.btnBrowseForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSortBy = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemSortByName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSortByFileSize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSortByModifiedDate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSortByCreatedDate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSortByImageSize = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSortAsc = new System.Windows.Forms.ToolStripButton();
            this.btnSortDesc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInformation = new System.Windows.Forms.ToolStripButton();
            this.btnTagSelected = new System.Windows.Forms.ToolStripButton();
            this.btnManageTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFullscreen = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripFolders.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageFolders.SuspendLayout();
            this.tabPageTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagModelBindingSource)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewFolders
            // 
            this.treeViewFolders.ContextMenuStrip = this.contextMenuStripFolders;
            this.treeViewFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFolders.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewFolders.HideSelection = false;
            this.treeViewFolders.ImageIndex = 0;
            this.treeViewFolders.ImageList = this.folderImageList;
            this.treeViewFolders.Location = new System.Drawing.Point(0, 0);
            this.treeViewFolders.Margin = new System.Windows.Forms.Padding(0);
            this.treeViewFolders.Name = "treeViewFolders";
            this.treeViewFolders.SelectedImageIndex = 0;
            this.treeViewFolders.Size = new System.Drawing.Size(192, 325);
            this.treeViewFolders.TabIndex = 0;
            this.treeViewFolders.Tag = "";
            this.treeViewFolders.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnFolderAfterCollapse);
            this.treeViewFolders.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnFolderBeforeExpand);
            this.treeViewFolders.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.OnFolderDrawNode);
            this.treeViewFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnFolderAfterSelect);
            // 
            // contextMenuStripFolders
            // 
            this.contextMenuStripFolders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFoldersRefresh});
            this.contextMenuStripFolders.Name = "contextMenuStripFolders";
            this.contextMenuStripFolders.Size = new System.Drawing.Size(114, 26);
            // 
            // menuItemFoldersRefresh
            // 
            this.menuItemFoldersRefresh.Image = global::ImageViewer.Properties.Resources.refresh_16;
            this.menuItemFoldersRefresh.Name = "menuItemFoldersRefresh";
            this.menuItemFoldersRefresh.Size = new System.Drawing.Size(113, 22);
            this.menuItemFoldersRefresh.Text = "Refresh";
            this.menuItemFoldersRefresh.Click += new System.EventHandler(this.OnFolderRefreshClick);
            // 
            // folderImageList
            // 
            this.folderImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("folderImageList.ImageStream")));
            this.folderImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.folderImageList.Images.SetKeyName(0, "folder_16.png");
            this.folderImageList.Images.SetKeyName(1, "folder_search_16.png");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.informationToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(147, 76);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.folder_picture_16;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.information_16;
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.informationToolStripMenuItem.Text = "Information...";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.OnInformationClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.delete_16;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.removeToolStripMenuItem.Text = "Remove...";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.OnRemoveClick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageFolders);
            this.tabControl.Controls.Add(this.tabPageTags);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.HotTrack = true;
            this.tabControl.ImageList = this.tabImageList;
            this.tabControl.ItemSize = new System.Drawing.Size(58, 28);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(200, 361);
            this.tabControl.TabIndex = 1;
            this.tabControl.Tag = "";
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.OnTabControlDrawItem);
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelected);
            // 
            // tabPageFolders
            // 
            this.tabPageFolders.Controls.Add(this.treeViewFolders);
            this.tabPageFolders.ImageIndex = 1;
            this.tabPageFolders.Location = new System.Drawing.Point(4, 32);
            this.tabPageFolders.Name = "tabPageFolders";
            this.tabPageFolders.Size = new System.Drawing.Size(192, 325);
            this.tabPageFolders.TabIndex = 1;
            this.tabPageFolders.Text = "Folders";
            this.tabPageFolders.UseVisualStyleBackColor = true;
            // 
            // tabPageTags
            // 
            this.tabPageTags.Controls.Add(this.listBoxTags);
            this.tabPageTags.ImageIndex = 0;
            this.tabPageTags.Location = new System.Drawing.Point(4, 32);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Size = new System.Drawing.Size(192, 300);
            this.tabPageTags.TabIndex = 0;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            // 
            // listBoxTags
            // 
            this.listBoxTags.DataSource = this.tagModelBindingSource;
            this.listBoxTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTags.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.IntegralHeight = false;
            this.listBoxTags.ItemHeight = 28;
            this.listBoxTags.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.listBoxTags.Location = new System.Drawing.Point(0, 0);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.Size = new System.Drawing.Size(192, 300);
            this.listBoxTags.TabIndex = 0;
            this.listBoxTags.Tag = "";
            this.listBoxTags.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnTagsListDrawItem);
            // 
            // tagModelBindingSource
            // 
            this.tagModelBindingSource.DataSource = typeof(ImageViewer.Data.Models.TagModel);
            this.tagModelBindingSource.PositionChanged += new System.EventHandler(this.OnTagListPositionChanged);
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImageList.Images.SetKeyName(0, "tags_16.png");
            this.tabImageList.Images.SetKeyName(1, "folders_16.png");
            // 
            // imageListView
            // 
            this.imageListView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imageListView.ContextMenuStrip = this.contextMenuStrip;
            this.imageListView.DataBindings.Add(new System.Windows.Forms.Binding("ViewMode", global::ImageViewer.Properties.Settings.Default, "LibraryBrowserViewMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.imageListView.DataBindings.Add(new System.Windows.Forms.Binding("DrawImageBorders", global::ImageViewer.Properties.Settings.Default, "LibraryBrowserDrawImageBorder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.imageListView.DetailsHeaderSize = 32;
            this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView.DrawImageBorders = global::ImageViewer.Properties.Settings.Default.LibraryBrowserDrawImageBorder;
            this.imageListView.ImageBackColor = System.Drawing.Color.Empty;
            this.imageListView.ImageBorderColor = System.Drawing.Color.Empty;
            this.imageListView.ItemPadding = new System.Windows.Forms.Padding(3);
            this.imageListView.ItemSize = 128;
            this.imageListView.ItemSpacingX = 4;
            this.imageListView.ItemSpacingY = 4;
            this.imageListView.Location = new System.Drawing.Point(200, 0);
            this.imageListView.Name = "imageListView";
            this.imageListView.Size = new System.Drawing.Size(584, 361);
            this.imageListView.TabIndex = 2;
            this.imageListView.TextPadding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.imageListView.ViewMode = global::ImageViewer.Properties.Settings.Default.LibraryBrowserViewMode;
            this.imageListView.ViewModeChanged += new System.EventHandler(this.OnViewModeChanged);
            this.imageListView.ItemDoubleClicked += new System.EventHandler(this.OnImageViewItemDoubleClicked);
            this.imageListView.Delete += new System.EventHandler(this.OnRemoveClick);
            this.imageListView.TagSelected += new System.EventHandler(this.OnTagSelectedClick);
            this.imageListView.SelectedIndexChanged += new System.EventHandler(this.OnImageListViewSelectedIndexChanged);
            this.imageListView.SelectedIndicesChanged += new System.EventHandler(this.OnImageListViewSelectedIndicesChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEdit,
            this.menuView});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Visible = false;
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator9,
            this.menuItemSelectNone,
            this.menuItemSelectAll,
            this.menuItemInvertSelection,
            this.toolStripSeparator7,
            this.menuItemAddTag,
            this.toolStripSeparator5,
            this.menuItemDelete});
            this.menuEdit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuEdit.MergeIndex = 1;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "&Edit";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(152, 6);
            // 
            // menuItemSelectNone
            // 
            this.menuItemSelectNone.Name = "menuItemSelectNone";
            this.menuItemSelectNone.Size = new System.Drawing.Size(155, 22);
            this.menuItemSelectNone.Text = "Select None";
            this.menuItemSelectNone.Click += new System.EventHandler(this.OnSelectNoneClick);
            // 
            // menuItemSelectAll
            // 
            this.menuItemSelectAll.Name = "menuItemSelectAll";
            this.menuItemSelectAll.Size = new System.Drawing.Size(155, 22);
            this.menuItemSelectAll.Text = "Select All";
            this.menuItemSelectAll.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // menuItemInvertSelection
            // 
            this.menuItemInvertSelection.Name = "menuItemInvertSelection";
            this.menuItemInvertSelection.Size = new System.Drawing.Size(155, 22);
            this.menuItemInvertSelection.Text = "Invert Selection";
            this.menuItemInvertSelection.Click += new System.EventHandler(this.OnInvertSelectionClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(152, 6);
            // 
            // menuItemAddTag
            // 
            this.menuItemAddTag.Image = global::ImageViewer.Properties.Resources.tag_add_16;
            this.menuItemAddTag.Name = "menuItemAddTag";
            this.menuItemAddTag.ShortcutKeyDisplayString = "T";
            this.menuItemAddTag.Size = new System.Drawing.Size(155, 22);
            this.menuItemAddTag.Text = "Add tag...";
            this.menuItemAddTag.Click += new System.EventHandler(this.OnTagSelectedClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Image = global::ImageViewer.Properties.Resources.delete_16;
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.ShortcutKeyDisplayString = "Del";
            this.menuItemDelete.Size = new System.Drawing.Size(155, 22);
            this.menuItemDelete.Text = "Delete";
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this.menuItemViewIcons,
            this.menuItemViewTiles,
            this.menuItemViewDetails,
            this.menuItemViewGallery,
            this.toolStripSeparator10,
            this.menuItemSlideshow});
            this.menuView.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "&View";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemViewIcons
            // 
            this.menuItemViewIcons.Image = global::ImageViewer.Properties.Resources.application_view_icons_16;
            this.menuItemViewIcons.Name = "menuItemViewIcons";
            this.menuItemViewIcons.Size = new System.Drawing.Size(152, 22);
            this.menuItemViewIcons.Text = "Icons";
            this.menuItemViewIcons.Click += new System.EventHandler(this.OnViewIconsClick);
            // 
            // menuItemViewTiles
            // 
            this.menuItemViewTiles.Image = global::ImageViewer.Properties.Resources.application_view_tile_16;
            this.menuItemViewTiles.Name = "menuItemViewTiles";
            this.menuItemViewTiles.Size = new System.Drawing.Size(152, 22);
            this.menuItemViewTiles.Text = "Tiles";
            this.menuItemViewTiles.Click += new System.EventHandler(this.OnViewTilesClick);
            // 
            // menuItemViewDetails
            // 
            this.menuItemViewDetails.Image = global::ImageViewer.Properties.Resources.application_view_detail_16;
            this.menuItemViewDetails.Name = "menuItemViewDetails";
            this.menuItemViewDetails.Size = new System.Drawing.Size(152, 22);
            this.menuItemViewDetails.Text = "Details";
            this.menuItemViewDetails.Click += new System.EventHandler(this.OnViewDetailsClick);
            // 
            // menuItemViewGallery
            // 
            this.menuItemViewGallery.Image = global::ImageViewer.Properties.Resources.application_view_gallery_16;
            this.menuItemViewGallery.Name = "menuItemViewGallery";
            this.menuItemViewGallery.Size = new System.Drawing.Size(152, 22);
            this.menuItemViewGallery.Text = "Gallery";
            this.menuItemViewGallery.Click += new System.EventHandler(this.OnViewGalleryClick);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemSlideshow
            // 
            this.menuItemSlideshow.Image = global::ImageViewer.Properties.Resources.fullscreen_16;
            this.menuItemSlideshow.Name = "menuItemSlideshow";
            this.menuItemSlideshow.ShortcutKeyDisplayString = "F11";
            this.menuItemSlideshow.Size = new System.Drawing.Size(152, 22);
            this.menuItemSlideshow.Text = "Slideshow";
            this.menuItemSlideshow.Click += new System.EventHandler(this.OnFullscreenClick);
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBrowserPath,
            this.btnBrowseUp,
            this.btnBrowseReload,
            this.btnBrowseBack,
            this.btnBrowseForward,
            this.toolStripSeparator4,
            this.btnSortBy,
            this.btnSortAsc,
            this.btnSortDesc,
            this.toolStripSeparator3,
            this.btnInformation,
            this.btnTagSelected,
            this.btnManageTags,
            this.toolStripSeparator2,
            this.btnDelete,
            this.toolStripSeparator6,
            this.btnFullscreen});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(784, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Visible = false;
            // 
            // lblBrowserPath
            // 
            this.lblBrowserPath.Name = "lblBrowserPath";
            this.lblBrowserPath.Size = new System.Drawing.Size(0, 22);
            // 
            // btnBrowseUp
            // 
            this.btnBrowseUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrowseUp.Image = global::ImageViewer.Properties.Resources.folder_up_16;
            this.btnBrowseUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseUp.Name = "btnBrowseUp";
            this.btnBrowseUp.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseUp.Text = "Go up";
            this.btnBrowseUp.Click += new System.EventHandler(this.OnBrowseUpClick);
            // 
            // btnBrowseReload
            // 
            this.btnBrowseReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrowseReload.Image = global::ImageViewer.Properties.Resources.refresh_16;
            this.btnBrowseReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseReload.Name = "btnBrowseReload";
            this.btnBrowseReload.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseReload.Text = "Reload";
            this.btnBrowseReload.Click += new System.EventHandler(this.OnBrowseReloadClick);
            // 
            // btnBrowseBack
            // 
            this.btnBrowseBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrowseBack.Image = global::ImageViewer.Properties.Resources.resultset_previous_16;
            this.btnBrowseBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseBack.Name = "btnBrowseBack";
            this.btnBrowseBack.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseBack.Text = "Back";
            this.btnBrowseBack.Click += new System.EventHandler(this.OnBrowseBackClick);
            // 
            // btnBrowseForward
            // 
            this.btnBrowseForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrowseForward.Image = global::ImageViewer.Properties.Resources.resultset_next_16;
            this.btnBrowseForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseForward.Name = "btnBrowseForward";
            this.btnBrowseForward.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseForward.Text = "Forward";
            this.btnBrowseForward.Click += new System.EventHandler(this.OnBrowseForwardClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSortBy
            // 
            this.btnSortBy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSortBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSortByName,
            this.menuItemSortByFileSize,
            this.menuItemSortByModifiedDate,
            this.menuItemSortByCreatedDate,
            this.menuItemSortByImageSize});
            this.btnSortBy.Image = global::ImageViewer.Properties.Resources.sort_16;
            this.btnSortBy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortBy.Name = "btnSortBy";
            this.btnSortBy.Size = new System.Drawing.Size(29, 22);
            this.btnSortBy.Text = "Sort By";
            // 
            // menuItemSortByName
            // 
            this.menuItemSortByName.Checked = true;
            this.menuItemSortByName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSortByName.Name = "menuItemSortByName";
            this.menuItemSortByName.Size = new System.Drawing.Size(149, 22);
            this.menuItemSortByName.Text = "Name";
            this.menuItemSortByName.Click += new System.EventHandler(this.OnSortByNameClick);
            // 
            // menuItemSortByFileSize
            // 
            this.menuItemSortByFileSize.Name = "menuItemSortByFileSize";
            this.menuItemSortByFileSize.Size = new System.Drawing.Size(149, 22);
            this.menuItemSortByFileSize.Text = "File Size";
            this.menuItemSortByFileSize.Click += new System.EventHandler(this.OnSortByFileSizeClick);
            // 
            // menuItemSortByModifiedDate
            // 
            this.menuItemSortByModifiedDate.Name = "menuItemSortByModifiedDate";
            this.menuItemSortByModifiedDate.Size = new System.Drawing.Size(149, 22);
            this.menuItemSortByModifiedDate.Text = "Modified Date";
            this.menuItemSortByModifiedDate.Click += new System.EventHandler(this.OnSortByModifiedDateClick);
            // 
            // menuItemSortByCreatedDate
            // 
            this.menuItemSortByCreatedDate.Name = "menuItemSortByCreatedDate";
            this.menuItemSortByCreatedDate.Size = new System.Drawing.Size(149, 22);
            this.menuItemSortByCreatedDate.Text = "Created Date";
            this.menuItemSortByCreatedDate.Click += new System.EventHandler(this.OnSortByCreatedDateClick);
            // 
            // menuItemSortByImageSize
            // 
            this.menuItemSortByImageSize.Name = "menuItemSortByImageSize";
            this.menuItemSortByImageSize.Size = new System.Drawing.Size(149, 22);
            this.menuItemSortByImageSize.Text = "Image Size";
            this.menuItemSortByImageSize.Click += new System.EventHandler(this.OnSortByImageSizeClick);
            // 
            // btnSortAsc
            // 
            this.btnSortAsc.Checked = true;
            this.btnSortAsc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSortAsc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSortAsc.Image = global::ImageViewer.Properties.Resources.sort_asc_az_16;
            this.btnSortAsc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortAsc.Name = "btnSortAsc";
            this.btnSortAsc.Size = new System.Drawing.Size(23, 22);
            this.btnSortAsc.Text = "Sort Ascending";
            this.btnSortAsc.Click += new System.EventHandler(this.OnSortAscClick);
            // 
            // btnSortDesc
            // 
            this.btnSortDesc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSortDesc.Image = global::ImageViewer.Properties.Resources.sort_desc_az_16;
            this.btnSortDesc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortDesc.Name = "btnSortDesc";
            this.btnSortDesc.Size = new System.Drawing.Size(23, 22);
            this.btnSortDesc.Text = "Sort Descending";
            this.btnSortDesc.Click += new System.EventHandler(this.OnSortDescClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnInformation
            // 
            this.btnInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInformation.Image = global::ImageViewer.Properties.Resources.information_16;
            this.btnInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(23, 22);
            this.btnInformation.Text = "Image information...";
            this.btnInformation.Click += new System.EventHandler(this.OnInformationClick);
            // 
            // btnTagSelected
            // 
            this.btnTagSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTagSelected.Image = global::ImageViewer.Properties.Resources.tag_add_16;
            this.btnTagSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTagSelected.Name = "btnTagSelected";
            this.btnTagSelected.Size = new System.Drawing.Size(23, 22);
            this.btnTagSelected.Text = "Tag selected images...";
            this.btnTagSelected.Click += new System.EventHandler(this.OnTagSelectedClick);
            // 
            // btnManageTags
            // 
            this.btnManageTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnManageTags.Image = global::ImageViewer.Properties.Resources.tags_16;
            this.btnManageTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManageTags.Name = "btnManageTags";
            this.btnManageTags.Size = new System.Drawing.Size(23, 22);
            this.btnManageTags.Text = "Manage Tags";
            this.btnManageTags.Click += new System.EventHandler(this.OnManageTagsClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::ImageViewer.Properties.Resources.delete_16;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.OnRemoveClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFullscreen
            // 
            this.btnFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullscreen.Image = global::ImageViewer.Properties.Resources.fullscreen_16;
            this.btnFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullscreen.Name = "btnFullscreen";
            this.btnFullscreen.Size = new System.Drawing.Size(23, 22);
            this.btnFullscreen.Text = "Start slideshow...";
            this.btnFullscreen.Click += new System.EventHandler(this.OnFullscreenClick);
            // 
            // LibraryBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.imageListView);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "LibraryBrowserForm";
            this.Text = "Library Browser";
            this.contextMenuStripFolders.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageFolders.ResumeLayout(false);
            this.tabPageTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tagModelBindingSource)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageTags;
        private System.Windows.Forms.TabPage tabPageFolders;
        private Controls.ListBoxEx listBoxTags;
        private System.Windows.Forms.BindingSource tagModelBindingSource;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.ImageList folderImageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolders;
        private System.Windows.Forms.ToolStripMenuItem menuItemFoldersRefresh;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnSortAsc;
        private System.Windows.Forms.ToolStripButton btnSortDesc;
        private System.Windows.Forms.ToolStripDropDownButton btnSortBy;
        private System.Windows.Forms.ToolStripMenuItem menuItemSortByName;
        private System.Windows.Forms.ToolStripMenuItem menuItemSortByFileSize;
        private System.Windows.Forms.ToolStripMenuItem menuItemSortByModifiedDate;
        private System.Windows.Forms.ToolStripMenuItem menuItemSortByCreatedDate;
        private Controls.ImageListView imageListView;
        private System.Windows.Forms.ToolStripLabel lblBrowserPath;
        private System.Windows.Forms.ToolStripButton btnBrowseUp;
        private System.Windows.Forms.ToolStripButton btnBrowseBack;
        private System.Windows.Forms.ToolStripButton btnBrowseForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnBrowseReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnTagSelected;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectNone;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemInvertSelection;
        private System.Windows.Forms.ToolStripButton btnManageTags;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnFullscreen;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuItemSlideshow;
        private System.Windows.Forms.ToolStripButton btnInformation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddTag;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewIcons;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewTiles;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewGallery;
        private System.Windows.Forms.ToolStripMenuItem menuItemSortByImageSize;
    }
}