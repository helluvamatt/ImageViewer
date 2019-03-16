namespace PixelStudio
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemResize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCanvasSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemResetZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomToFit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMouseCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.layout = new System.Windows.Forms.SplitContainer();
            this.layoutSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeaderToolbox = new System.Windows.Forms.Label();
            this.lblHeaderLayers = new System.Windows.Forms.Label();
            this.listBoxToolbox = new PixelStudio.Controls.ListBoxEx();
            this.toolboxItemModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridLayers = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isVisibleDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.layerModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.canvasControl = new PixelStudio.Controls.CanvasControl();
            this.contextMenuLayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemNewLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
            this.layout.Panel1.SuspendLayout();
            this.layout.Panel2.SuspendLayout();
            this.layout.SuspendLayout();
            this.layoutSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolboxItemModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerModelBindingSource)).BeginInit();
            this.contextMenuLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuImage,
            this.menuView});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.toolStripSeparator1,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator2,
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Image = global::PixelStudio.Properties.Resources.page_white_16;
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNew.Size = new System.Drawing.Size(193, 22);
            this.menuItemNew.Text = "&New...";
            this.menuItemNew.Click += new System.EventHandler(this.OnNewClick);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = global::PixelStudio.Properties.Resources.folder_picture_16;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(193, 22);
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = global::PixelStudio.Properties.Resources.diskette_16;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(193, 22);
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Image = global::PixelStudio.Properties.Resources.file_save_as_16;
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuItemSaveAs.Size = new System.Drawing.Size(193, 22);
            this.menuItemSaveAs.Text = "Save &as...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.OnSaveAsClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = global::PixelStudio.Properties.Resources.door_in_16;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemExit.Size = new System.Drawing.Size(193, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.OnExitClick);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUndo,
            this.menuItemRedo,
            this.toolStripSeparator3,
            this.menuItemCut,
            this.menuItemCopy,
            this.menuItemPaste,
            this.toolStripSeparator6,
            this.menuItemSelectAll,
            this.menuItemSelectNone});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "Edit";
            // 
            // menuItemUndo
            // 
            this.menuItemUndo.Image = global::PixelStudio.Properties.Resources.undo_16;
            this.menuItemUndo.Name = "menuItemUndo";
            this.menuItemUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuItemUndo.Size = new System.Drawing.Size(144, 22);
            this.menuItemUndo.Text = "Undo";
            this.menuItemUndo.Click += new System.EventHandler(this.OnUndoClick);
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Image = global::PixelStudio.Properties.Resources.redo_16;
            this.menuItemRedo.Name = "menuItemRedo";
            this.menuItemRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuItemRedo.Size = new System.Drawing.Size(144, 22);
            this.menuItemRedo.Text = "Redo";
            this.menuItemRedo.Click += new System.EventHandler(this.OnRedoClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // menuItemCut
            // 
            this.menuItemCut.Image = global::PixelStudio.Properties.Resources.cut_16;
            this.menuItemCut.Name = "menuItemCut";
            this.menuItemCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuItemCut.Size = new System.Drawing.Size(144, 22);
            this.menuItemCut.Text = "Cut";
            this.menuItemCut.Click += new System.EventHandler(this.OnCutClick);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Image = global::PixelStudio.Properties.Resources.page_white_copy_16;
            this.menuItemCopy.Name = "menuItemCopy";
            this.menuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuItemCopy.Size = new System.Drawing.Size(144, 22);
            this.menuItemCopy.Text = "Copy";
            this.menuItemCopy.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // menuItemPaste
            // 
            this.menuItemPaste.Image = global::PixelStudio.Properties.Resources.page_white_paste_16;
            this.menuItemPaste.Name = "menuItemPaste";
            this.menuItemPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuItemPaste.Size = new System.Drawing.Size(144, 22);
            this.menuItemPaste.Text = "Paste";
            this.menuItemPaste.Click += new System.EventHandler(this.OnPasteClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
            // 
            // menuItemSelectAll
            // 
            this.menuItemSelectAll.Name = "menuItemSelectAll";
            this.menuItemSelectAll.Size = new System.Drawing.Size(144, 22);
            this.menuItemSelectAll.Text = "Select All";
            this.menuItemSelectAll.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // menuItemSelectNone
            // 
            this.menuItemSelectNone.Name = "menuItemSelectNone";
            this.menuItemSelectNone.Size = new System.Drawing.Size(144, 22);
            this.menuItemSelectNone.Text = "Select None";
            this.menuItemSelectNone.Click += new System.EventHandler(this.OnSelectNoneClick);
            // 
            // menuImage
            // 
            this.menuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemResize,
            this.menuItemCanvasSize,
            this.toolStripSeparator4,
            this.menuItemCrop});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(52, 20);
            this.menuImage.Text = "Image";
            // 
            // menuItemResize
            // 
            this.menuItemResize.Image = global::PixelStudio.Properties.Resources.resize_picture_16;
            this.menuItemResize.Name = "menuItemResize";
            this.menuItemResize.Size = new System.Drawing.Size(144, 22);
            this.menuItemResize.Text = "Resize...";
            // 
            // menuItemCanvasSize
            // 
            this.menuItemCanvasSize.Image = global::PixelStudio.Properties.Resources.canvas_size_16;
            this.menuItemCanvasSize.Name = "menuItemCanvasSize";
            this.menuItemCanvasSize.Size = new System.Drawing.Size(144, 22);
            this.menuItemCanvasSize.Text = "Canvas Size...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // menuItemCrop
            // 
            this.menuItemCrop.Image = global::PixelStudio.Properties.Resources.transform_crop_16;
            this.menuItemCrop.Name = "menuItemCrop";
            this.menuItemCrop.Size = new System.Drawing.Size(144, 22);
            this.menuItemCrop.Text = "Crop";
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemZoomIn,
            this.menuItemZoomOut,
            this.menuItemResetZoom,
            this.menuItemZoomToFit});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "View";
            // 
            // menuItemZoomIn
            // 
            this.menuItemZoomIn.Image = global::PixelStudio.Properties.Resources.zoom_in_16;
            this.menuItemZoomIn.Name = "menuItemZoomIn";
            this.menuItemZoomIn.ShortcutKeyDisplayString = "+";
            this.menuItemZoomIn.Size = new System.Drawing.Size(144, 22);
            this.menuItemZoomIn.Text = "Zoom In";
            this.menuItemZoomIn.Click += new System.EventHandler(this.OnZoomInClick);
            // 
            // menuItemZoomOut
            // 
            this.menuItemZoomOut.Image = global::PixelStudio.Properties.Resources.zoom_out_16;
            this.menuItemZoomOut.Name = "menuItemZoomOut";
            this.menuItemZoomOut.ShortcutKeyDisplayString = "-";
            this.menuItemZoomOut.Size = new System.Drawing.Size(144, 22);
            this.menuItemZoomOut.Text = "Zoom Out";
            this.menuItemZoomOut.Click += new System.EventHandler(this.OnZoomOutClick);
            // 
            // menuItemResetZoom
            // 
            this.menuItemResetZoom.Image = global::PixelStudio.Properties.Resources.zoom_actual_16;
            this.menuItemResetZoom.Name = "menuItemResetZoom";
            this.menuItemResetZoom.ShortcutKeyDisplayString = "0";
            this.menuItemResetZoom.Size = new System.Drawing.Size(144, 22);
            this.menuItemResetZoom.Text = "Actual Size";
            this.menuItemResetZoom.Click += new System.EventHandler(this.OnResetZoomClick);
            // 
            // menuItemZoomToFit
            // 
            this.menuItemZoomToFit.Image = global::PixelStudio.Properties.Resources.zoom_fit_16;
            this.menuItemZoomToFit.Name = "menuItemZoomToFit";
            this.menuItemZoomToFit.Size = new System.Drawing.Size(144, 22);
            this.menuItemZoomToFit.Text = "Zoom to Fit";
            this.menuItemZoomToFit.Click += new System.EventHandler(this.OnZoomToFitClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusName,
            this.lblStatusSize,
            this.lblStatusZoom,
            this.lblStatusSelection,
            this.statusStripSpacer,
            this.lblMouseCoord});
            this.statusStrip.Location = new System.Drawing.Point(0, 426);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 24);
            this.statusStrip.TabIndex = 1;
            // 
            // lblStatusName
            // 
            this.lblStatusName.Name = "lblStatusName";
            this.lblStatusName.Size = new System.Drawing.Size(88, 19);
            this.lblStatusName.Text = "No file selected";
            // 
            // lblStatusSize
            // 
            this.lblStatusSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblStatusSize.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblStatusSize.Name = "lblStatusSize";
            this.lblStatusSize.Size = new System.Drawing.Size(34, 19);
            this.lblStatusSize.Text = "0 x 0";
            // 
            // lblStatusZoom
            // 
            this.lblStatusZoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblStatusZoom.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblStatusZoom.Name = "lblStatusZoom";
            this.lblStatusZoom.Size = new System.Drawing.Size(39, 19);
            this.lblStatusZoom.Text = "100%";
            // 
            // lblStatusSelection
            // 
            this.lblStatusSelection.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblStatusSelection.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblStatusSelection.Name = "lblStatusSelection";
            this.lblStatusSelection.Size = new System.Drawing.Size(77, 19);
            this.lblStatusSelection.Text = "No selection";
            // 
            // statusStripSpacer
            // 
            this.statusStripSpacer.Name = "statusStripSpacer";
            this.statusStripSpacer.Size = new System.Drawing.Size(522, 19);
            this.statusStripSpacer.Spring = true;
            // 
            // lblMouseCoord
            // 
            this.lblMouseCoord.Name = "lblMouseCoord";
            this.lblMouseCoord.Size = new System.Drawing.Size(25, 19);
            this.lblMouseCoord.Text = "0, 0";
            // 
            // layout
            // 
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layout.Location = new System.Drawing.Point(0, 24);
            this.layout.Name = "layout";
            // 
            // layout.Panel1
            // 
            this.layout.Panel1.Controls.Add(this.layoutSidebar);
            // 
            // layout.Panel2
            // 
            this.layout.Panel2.Controls.Add(this.canvasControl);
            this.layout.Size = new System.Drawing.Size(800, 402);
            this.layout.SplitterDistance = 220;
            this.layout.TabIndex = 2;
            // 
            // layoutSidebar
            // 
            this.layoutSidebar.ColumnCount = 1;
            this.layoutSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutSidebar.Controls.Add(this.lblHeaderToolbox, 0, 0);
            this.layoutSidebar.Controls.Add(this.lblHeaderLayers, 0, 2);
            this.layoutSidebar.Controls.Add(this.listBoxToolbox, 0, 1);
            this.layoutSidebar.Controls.Add(this.gridLayers, 0, 3);
            this.layoutSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSidebar.Location = new System.Drawing.Point(0, 0);
            this.layoutSidebar.Name = "layoutSidebar";
            this.layoutSidebar.RowCount = 4;
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSidebar.Size = new System.Drawing.Size(220, 402);
            this.layoutSidebar.TabIndex = 0;
            // 
            // lblHeaderToolbox
            // 
            this.lblHeaderToolbox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblHeaderToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderToolbox.Location = new System.Drawing.Point(3, 0);
            this.lblHeaderToolbox.Name = "lblHeaderToolbox";
            this.lblHeaderToolbox.Size = new System.Drawing.Size(214, 26);
            this.lblHeaderToolbox.TabIndex = 0;
            this.lblHeaderToolbox.Text = "Toolbox";
            this.lblHeaderToolbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeaderLayers
            // 
            this.lblHeaderLayers.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblHeaderLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderLayers.Location = new System.Drawing.Point(3, 201);
            this.lblHeaderLayers.Name = "lblHeaderLayers";
            this.lblHeaderLayers.Size = new System.Drawing.Size(214, 26);
            this.lblHeaderLayers.TabIndex = 1;
            this.lblHeaderLayers.Text = "Layers";
            this.lblHeaderLayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxToolbox
            // 
            this.listBoxToolbox.DataSource = this.toolboxItemModelBindingSource;
            this.listBoxToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxToolbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxToolbox.FormattingEnabled = true;
            this.listBoxToolbox.IntegralHeight = false;
            this.listBoxToolbox.ItemHeight = 26;
            this.listBoxToolbox.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.listBoxToolbox.Location = new System.Drawing.Point(3, 29);
            this.listBoxToolbox.Name = "listBoxToolbox";
            this.listBoxToolbox.Size = new System.Drawing.Size(214, 169);
            this.listBoxToolbox.TabIndex = 2;
            this.listBoxToolbox.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnToolboxDrawItem);
            // 
            // toolboxItemModelBindingSource
            // 
            this.toolboxItemModelBindingSource.DataSource = typeof(PixelStudio.Models.ToolboxItemModel);
            this.toolboxItemModelBindingSource.CurrentChanged += new System.EventHandler(this.OnToolboxItemCurrentChanged);
            // 
            // gridLayers
            // 
            this.gridLayers.AllowUserToResizeColumns = false;
            this.gridLayers.AllowUserToResizeRows = false;
            this.gridLayers.AutoGenerateColumns = false;
            this.gridLayers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.isVisibleDataGridViewCheckBoxColumn});
            this.gridLayers.DataSource = this.layerModelBindingSource;
            this.gridLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLayers.Location = new System.Drawing.Point(3, 230);
            this.gridLayers.MultiSelect = false;
            this.gridLayers.Name = "gridLayers";
            this.gridLayers.RowHeadersVisible = false;
            this.gridLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLayers.Size = new System.Drawing.Size(214, 169);
            this.gridLayers.TabIndex = 3;
            this.gridLayers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.OnGridLayersCellPainting);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // isVisibleDataGridViewCheckBoxColumn
            // 
            this.isVisibleDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.isVisibleDataGridViewCheckBoxColumn.DataPropertyName = "IsVisible";
            this.isVisibleDataGridViewCheckBoxColumn.HeaderText = "";
            this.isVisibleDataGridViewCheckBoxColumn.Name = "isVisibleDataGridViewCheckBoxColumn";
            this.isVisibleDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isVisibleDataGridViewCheckBoxColumn.Width = 28;
            // 
            // layerModelBindingSource
            // 
            this.layerModelBindingSource.DataSource = typeof(PixelStudio.Models.LayerModel);
            this.layerModelBindingSource.CurrentChanged += new System.EventHandler(this.OnLayerCurrentChanged);
            // 
            // canvasControl
            // 
            this.canvasControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.canvasControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasControl.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image;
            this.canvasControl.Location = new System.Drawing.Point(0, 0);
            this.canvasControl.Name = "canvasControl";
            this.canvasControl.ShowPixelGrid = true;
            this.canvasControl.Size = new System.Drawing.Size(576, 402);
            this.canvasControl.TabIndex = 0;
            this.canvasControl.VirtualMode = true;
            this.canvasControl.MouseLocationChanged += new System.EventHandler(this.OnCanvasMouseLocationChanged);
            this.canvasControl.SelectionRegionChanged += new System.EventHandler(this.OnSelectionRegionChanged);
            this.canvasControl.VirtualSizeChanged += new System.EventHandler(this.OnVirtualSizeChanged);
            this.canvasControl.Zoomed += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxZoomEventArgs>(this.OnZoomed);
            // 
            // contextMenuLayers
            // 
            this.contextMenuLayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewLayer,
            this.toolStripSeparator5,
            this.menuItemDeleteLayer});
            this.contextMenuLayers.Name = "contextMenuLayers";
            this.contextMenuLayers.Size = new System.Drawing.Size(139, 54);
            // 
            // menuItemNewLayer
            // 
            this.menuItemNewLayer.Name = "menuItemNewLayer";
            this.menuItemNewLayer.Size = new System.Drawing.Size(138, 22);
            this.menuItemNewLayer.Text = "New Layer";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(135, 6);
            // 
            // menuItemDeleteLayer
            // 
            this.menuItemDeleteLayer.Name = "menuItemDeleteLayer";
            this.menuItemDeleteLayer.Size = new System.Drawing.Size(138, 22);
            this.menuItemDeleteLayer.Text = "Delete Layer";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open image file...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Save image file...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Pixel Studio";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.layout.Panel1.ResumeLayout(false);
            this.layout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
            this.layout.ResumeLayout(false);
            this.layoutSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toolboxItemModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerModelBindingSource)).EndInit();
            this.contextMenuLayers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuImage;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemUndo;
        private System.Windows.Forms.ToolStripMenuItem menuItemRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemCut;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem menuItemResize;
        private System.Windows.Forms.ToolStripMenuItem menuItemCanvasSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuItemCrop;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemResetZoom;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomToFit;
        private System.Windows.Forms.SplitContainer layout;
        private System.Windows.Forms.TableLayoutPanel layoutSidebar;
        private System.Windows.Forms.Label lblHeaderToolbox;
        private System.Windows.Forms.Label lblHeaderLayers;
        private Controls.ListBoxEx listBoxToolbox;
        private System.Windows.Forms.BindingSource toolboxItemModelBindingSource;
        private System.Windows.Forms.BindingSource layerModelBindingSource;
        private Controls.CanvasControl canvasControl;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusName;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSize;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSelection;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSpacer;
        private System.Windows.Forms.ToolStripStatusLabel lblMouseCoord;
        private System.Windows.Forms.ContextMenuStrip contextMenuLayers;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteLayer;
        private System.Windows.Forms.DataGridView gridLayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isVisibleDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectNone;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}