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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCloseProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemImportImages = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExportVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemProjectProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.layout = new System.Windows.Forms.SplitContainer();
            this.layoutSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lblProperties = new System.Windows.Forms.Label();
            this.lblImages = new System.Windows.Forms.Label();
            this.listBoxImages = new PixelStudio.Controls.ListBoxEx();
            this.imageReferenceModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutContent = new System.Windows.Forms.TableLayoutPanel();
            this.layoutTimeline = new System.Windows.Forms.Panel();
            this.timelineControl = new PixelStudio.Controls.TimelineControl();
            this.timelineToolstrip = new System.Windows.Forms.ToolStrip();
            this.lblTimeline = new System.Windows.Forms.ToolStripLabel();
            this.btnTimelineZoomToFit = new System.Windows.Forms.ToolStripButton();
            this.btnTimelineZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnTimelineZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnTransportNext = new System.Windows.Forms.Button();
            this.btnTransportPlayPause = new System.Windows.Forms.Button();
            this.previewControl = new PixelStudio.Controls.PreviewControl();
            this.lblTransportDisplay = new System.Windows.Forms.Label();
            this.btnTransportPrevious = new System.Windows.Forms.Button();
            this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
            this.layout.Panel1.SuspendLayout();
            this.layout.Panel2.SuspendLayout();
            this.layout.SuspendLayout();
            this.layoutSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageReferenceModelBindingSource)).BeginInit();
            this.layoutContent.SuspendLayout();
            this.layoutTimeline.SuspendLayout();
            this.timelineToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuProject});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.toolStripSeparator1,
            this.menuItemCloseProject,
            this.toolStripSeparator7,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator4,
            this.menuItemImportImages,
            this.toolStripSeparator2,
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Image = global::PixelStudio.Properties.Resources.page_white_16;
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNew.Size = new System.Drawing.Size(235, 22);
            this.menuItemNew.Text = "&New Project...";
            this.menuItemNew.Click += new System.EventHandler(this.OnNewClick);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = global::PixelStudio.Properties.Resources.folder_16;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(235, 22);
            this.menuItemOpen.Text = "&Open Project...";
            this.menuItemOpen.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
            // 
            // menuItemCloseProject
            // 
            this.menuItemCloseProject.Name = "menuItemCloseProject";
            this.menuItemCloseProject.Size = new System.Drawing.Size(235, 22);
            this.menuItemCloseProject.Text = "Close Project";
            this.menuItemCloseProject.Click += new System.EventHandler(this.OnCloseClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(232, 6);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = global::PixelStudio.Properties.Resources.diskette_16;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(235, 22);
            this.menuItemSave.Text = "&Save Project";
            this.menuItemSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Image = global::PixelStudio.Properties.Resources.file_save_as_16;
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuItemSaveAs.Size = new System.Drawing.Size(235, 22);
            this.menuItemSaveAs.Text = "Save Project &As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.OnSaveAsClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(232, 6);
            // 
            // menuItemImportImages
            // 
            this.menuItemImportImages.Image = global::PixelStudio.Properties.Resources.folder_picture_16;
            this.menuItemImportImages.Name = "menuItemImportImages";
            this.menuItemImportImages.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.menuItemImportImages.Size = new System.Drawing.Size(235, 22);
            this.menuItemImportImages.Text = "&Import images...";
            this.menuItemImportImages.Click += new System.EventHandler(this.OnImportImagesClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(232, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = global::PixelStudio.Properties.Resources.door_in_16;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemExit.Size = new System.Drawing.Size(235, 22);
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
            this.menuEdit.Text = "&Edit";
            // 
            // menuItemUndo
            // 
            this.menuItemUndo.Image = global::PixelStudio.Properties.Resources.undo_16;
            this.menuItemUndo.Name = "menuItemUndo";
            this.menuItemUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuItemUndo.Size = new System.Drawing.Size(144, 22);
            this.menuItemUndo.Text = "&Undo";
            this.menuItemUndo.Click += new System.EventHandler(this.OnUndoClick);
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Image = global::PixelStudio.Properties.Resources.redo_16;
            this.menuItemRedo.Name = "menuItemRedo";
            this.menuItemRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuItemRedo.Size = new System.Drawing.Size(144, 22);
            this.menuItemRedo.Text = "&Redo";
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
            this.menuItemCut.Text = "Cu&t";
            this.menuItemCut.Click += new System.EventHandler(this.OnCutClick);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Image = global::PixelStudio.Properties.Resources.page_white_copy_16;
            this.menuItemCopy.Name = "menuItemCopy";
            this.menuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuItemCopy.Size = new System.Drawing.Size(144, 22);
            this.menuItemCopy.Text = "&Copy";
            this.menuItemCopy.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // menuItemPaste
            // 
            this.menuItemPaste.Image = global::PixelStudio.Properties.Resources.page_white_paste_16;
            this.menuItemPaste.Name = "menuItemPaste";
            this.menuItemPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuItemPaste.Size = new System.Drawing.Size(144, 22);
            this.menuItemPaste.Text = "&Paste";
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
            this.menuItemSelectAll.Text = "Select &All";
            this.menuItemSelectAll.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // menuItemSelectNone
            // 
            this.menuItemSelectNone.Name = "menuItemSelectNone";
            this.menuItemSelectNone.Size = new System.Drawing.Size(144, 22);
            this.menuItemSelectNone.Text = "Select &None";
            this.menuItemSelectNone.Click += new System.EventHandler(this.OnSelectNoneClick);
            // 
            // menuProject
            // 
            this.menuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExportVideo,
            this.toolStripSeparator5,
            this.menuItemProjectProperties});
            this.menuProject.Name = "menuProject";
            this.menuProject.Size = new System.Drawing.Size(56, 20);
            this.menuProject.Text = "&Project";
            // 
            // menuItemExportVideo
            // 
            this.menuItemExportVideo.Image = global::PixelStudio.Properties.Resources.film_16;
            this.menuItemExportVideo.Name = "menuItemExportVideo";
            this.menuItemExportVideo.Size = new System.Drawing.Size(149, 22);
            this.menuItemExportVideo.Text = "Export Video...";
            this.menuItemExportVideo.Click += new System.EventHandler(this.OnExportVideoClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(146, 6);
            // 
            // menuItemProjectProperties
            // 
            this.menuItemProjectProperties.Image = global::PixelStudio.Properties.Resources.property_16;
            this.menuItemProjectProperties.Name = "menuItemProjectProperties";
            this.menuItemProjectProperties.Size = new System.Drawing.Size(149, 22);
            this.menuItemProjectProperties.Text = "&Properties...";
            this.menuItemProjectProperties.Click += new System.EventHandler(this.OnProjectPropertiesClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 639);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Pixel Studio project...|*.psproj|All files...|*.*";
            this.openFileDialog.Title = "Open Project...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Pixel Studio project...|*.psproj|All files...|*.*";
            this.saveFileDialog.Title = "Save Project...";
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
            this.layout.Panel2.Controls.Add(this.layoutContent);
            this.layout.Size = new System.Drawing.Size(1184, 615);
            this.layout.SplitterDistance = 314;
            this.layout.TabIndex = 2;
            // 
            // layoutSidebar
            // 
            this.layoutSidebar.ColumnCount = 1;
            this.layoutSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutSidebar.Controls.Add(this.propertyGrid, 0, 3);
            this.layoutSidebar.Controls.Add(this.lblProperties, 0, 2);
            this.layoutSidebar.Controls.Add(this.lblImages, 0, 0);
            this.layoutSidebar.Controls.Add(this.listBoxImages, 0, 1);
            this.layoutSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSidebar.Location = new System.Drawing.Point(0, 0);
            this.layoutSidebar.Name = "layoutSidebar";
            this.layoutSidebar.RowCount = 4;
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSidebar.Size = new System.Drawing.Size(314, 615);
            this.layoutSidebar.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 335);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(308, 277);
            this.propertyGrid.TabIndex = 0;
            // 
            // lblProperties
            // 
            this.lblProperties.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProperties.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.Location = new System.Drawing.Point(3, 307);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(308, 25);
            this.lblProperties.TabIndex = 1;
            this.lblProperties.Text = "Properties";
            this.lblProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImages
            // 
            this.lblImages.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImages.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImages.Location = new System.Drawing.Point(3, 0);
            this.lblImages.Name = "lblImages";
            this.lblImages.Size = new System.Drawing.Size(308, 25);
            this.lblImages.TabIndex = 2;
            this.lblImages.Text = "Images";
            this.lblImages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxImages
            // 
            this.listBoxImages.DataSource = this.imageReferenceModelBindingSource;
            this.listBoxImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxImages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxImages.FormattingEnabled = true;
            this.listBoxImages.IntegralHeight = false;
            this.listBoxImages.ItemHeight = 24;
            this.listBoxImages.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.listBoxImages.Location = new System.Drawing.Point(3, 28);
            this.listBoxImages.Name = "listBoxImages";
            this.listBoxImages.Size = new System.Drawing.Size(308, 276);
            this.listBoxImages.TabIndex = 3;
            this.listBoxImages.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnImageListDrawItem);
            this.listBoxImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnImagesMouseDown);
            this.listBoxImages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnImagesMouseMove);
            this.listBoxImages.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnImagesMouseUp);
            // 
            // imageReferenceModelBindingSource
            // 
            this.imageReferenceModelBindingSource.DataSource = typeof(PixelStudio.Common.ImageReferenceModel);
            // 
            // layoutContent
            // 
            this.layoutContent.ColumnCount = 5;
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutContent.Controls.Add(this.layoutTimeline, 0, 2);
            this.layoutContent.Controls.Add(this.btnTransportNext, 3, 1);
            this.layoutContent.Controls.Add(this.btnTransportPlayPause, 2, 1);
            this.layoutContent.Controls.Add(this.previewControl, 0, 0);
            this.layoutContent.Controls.Add(this.lblTransportDisplay, 0, 1);
            this.layoutContent.Controls.Add(this.btnTransportPrevious, 1, 1);
            this.layoutContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContent.Location = new System.Drawing.Point(0, 0);
            this.layoutContent.Name = "layoutContent";
            this.layoutContent.RowCount = 3;
            this.layoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutContent.Size = new System.Drawing.Size(866, 615);
            this.layoutContent.TabIndex = 0;
            // 
            // layoutTimeline
            // 
            this.layoutContent.SetColumnSpan(this.layoutTimeline, 5);
            this.layoutTimeline.Controls.Add(this.timelineControl);
            this.layoutTimeline.Controls.Add(this.timelineToolstrip);
            this.layoutTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTimeline.Location = new System.Drawing.Point(3, 468);
            this.layoutTimeline.Name = "layoutTimeline";
            this.layoutTimeline.Size = new System.Drawing.Size(860, 144);
            this.layoutTimeline.TabIndex = 0;
            // 
            // timelineControl
            // 
            this.timelineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timelineControl.Location = new System.Drawing.Point(0, 25);
            this.timelineControl.Name = "timelineControl";
            this.timelineControl.SelectedIndex = 0;
            this.timelineControl.Size = new System.Drawing.Size(860, 119);
            this.timelineControl.TabIndex = 1;
            this.timelineControl.Timeline = null;
            this.timelineControl.ZoomChanged += new System.EventHandler(this.OnTimelineZoomChanged);
            this.timelineControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnTimelineDragDrop);
            this.timelineControl.DragOver += new System.Windows.Forms.DragEventHandler(this.OnTimelineDragOver);
            // 
            // timelineToolstrip
            // 
            this.timelineToolstrip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.timelineToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.timelineToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTimeline,
            this.btnTimelineZoomToFit,
            this.btnTimelineZoomOut,
            this.btnTimelineZoomIn});
            this.timelineToolstrip.Location = new System.Drawing.Point(0, 0);
            this.timelineToolstrip.Name = "timelineToolstrip";
            this.timelineToolstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.timelineToolstrip.Size = new System.Drawing.Size(860, 25);
            this.timelineToolstrip.TabIndex = 0;
            this.timelineToolstrip.Text = "toolStrip1";
            // 
            // lblTimeline
            // 
            this.lblTimeline.Name = "lblTimeline";
            this.lblTimeline.Size = new System.Drawing.Size(53, 22);
            this.lblTimeline.Text = "Timeline";
            // 
            // btnTimelineZoomToFit
            // 
            this.btnTimelineZoomToFit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnTimelineZoomToFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTimelineZoomToFit.Image = ((System.Drawing.Image)(resources.GetObject("btnTimelineZoomToFit.Image")));
            this.btnTimelineZoomToFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimelineZoomToFit.Name = "btnTimelineZoomToFit";
            this.btnTimelineZoomToFit.Size = new System.Drawing.Size(23, 22);
            this.btnTimelineZoomToFit.Text = "Zoom to fit";
            this.btnTimelineZoomToFit.Click += new System.EventHandler(this.OnTimelineZoomToFitClick);
            // 
            // btnTimelineZoomOut
            // 
            this.btnTimelineZoomOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnTimelineZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTimelineZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnTimelineZoomOut.Image")));
            this.btnTimelineZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimelineZoomOut.Name = "btnTimelineZoomOut";
            this.btnTimelineZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnTimelineZoomOut.Text = "Zoom out";
            this.btnTimelineZoomOut.Click += new System.EventHandler(this.OnTimelineZoomOutClick);
            // 
            // btnTimelineZoomIn
            // 
            this.btnTimelineZoomIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnTimelineZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTimelineZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnTimelineZoomIn.Image")));
            this.btnTimelineZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimelineZoomIn.Name = "btnTimelineZoomIn";
            this.btnTimelineZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnTimelineZoomIn.Text = "Zoom in";
            this.btnTimelineZoomIn.Click += new System.EventHandler(this.OnTimelineZoomInClick);
            // 
            // btnTransportNext
            // 
            this.btnTransportNext.FlatAppearance.BorderSize = 0;
            this.btnTransportNext.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTransportNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransportNext.Image = global::PixelStudio.Properties.Resources.control_next_32;
            this.btnTransportNext.Location = new System.Drawing.Point(454, 423);
            this.btnTransportNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnTransportNext.Name = "btnTransportNext";
            this.btnTransportNext.Size = new System.Drawing.Size(42, 42);
            this.btnTransportNext.TabIndex = 1;
            this.btnTransportNext.UseVisualStyleBackColor = true;
            this.btnTransportNext.Click += new System.EventHandler(this.OnTransportNextClick);
            // 
            // btnTransportPlayPause
            // 
            this.btnTransportPlayPause.FlatAppearance.BorderSize = 0;
            this.btnTransportPlayPause.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTransportPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransportPlayPause.Image = global::PixelStudio.Properties.Resources.control_play_32;
            this.btnTransportPlayPause.Location = new System.Drawing.Point(412, 423);
            this.btnTransportPlayPause.Margin = new System.Windows.Forms.Padding(0);
            this.btnTransportPlayPause.Name = "btnTransportPlayPause";
            this.btnTransportPlayPause.Size = new System.Drawing.Size(42, 42);
            this.btnTransportPlayPause.TabIndex = 0;
            this.btnTransportPlayPause.UseVisualStyleBackColor = true;
            this.btnTransportPlayPause.Click += new System.EventHandler(this.OnTransportPlayPauseClick);
            // 
            // previewControl
            // 
            this.previewControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.layoutContent.SetColumnSpan(this.previewControl, 5);
            this.previewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl.Location = new System.Drawing.Point(3, 3);
            this.previewControl.Name = "previewControl";
            this.previewControl.Size = new System.Drawing.Size(860, 417);
            this.previewControl.TabIndex = 1;
            // 
            // lblTransportDisplay
            // 
            this.lblTransportDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransportDisplay.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransportDisplay.Location = new System.Drawing.Point(3, 423);
            this.lblTransportDisplay.Name = "lblTransportDisplay";
            this.lblTransportDisplay.Size = new System.Drawing.Size(364, 42);
            this.lblTransportDisplay.TabIndex = 3;
            this.lblTransportDisplay.Text = "0:00:00.000";
            this.lblTransportDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTransportPrevious
            // 
            this.btnTransportPrevious.FlatAppearance.BorderSize = 0;
            this.btnTransportPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTransportPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransportPrevious.Image = global::PixelStudio.Properties.Resources.control_prev_32;
            this.btnTransportPrevious.Location = new System.Drawing.Point(370, 423);
            this.btnTransportPrevious.Margin = new System.Windows.Forms.Padding(0);
            this.btnTransportPrevious.Name = "btnTransportPrevious";
            this.btnTransportPrevious.Size = new System.Drawing.Size(42, 42);
            this.btnTransportPrevious.TabIndex = 2;
            this.btnTransportPrevious.UseVisualStyleBackColor = true;
            this.btnTransportPrevious.Click += new System.EventHandler(this.OnTransportPreviousClick);
            // 
            // openFileDialogImport
            // 
            this.openFileDialogImport.Filter = resources.GetString("openFileDialogImport.Filter");
            this.openFileDialogImport.Multiselect = true;
            this.openFileDialogImport.Title = "Import images...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Pixel Studio";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.layout.Panel1.ResumeLayout(false);
            this.layout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
            this.layout.ResumeLayout(false);
            this.layoutSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageReferenceModelBindingSource)).EndInit();
            this.layoutContent.ResumeLayout(false);
            this.layoutTimeline.ResumeLayout(false);
            this.layoutTimeline.PerformLayout();
            this.timelineToolstrip.ResumeLayout(false);
            this.timelineToolstrip.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemUndo;
        private System.Windows.Forms.ToolStripMenuItem menuItemRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemCut;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectNone;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.SplitContainer layout;
        private System.Windows.Forms.TableLayoutPanel layoutContent;
        private System.Windows.Forms.Panel layoutTimeline;
        private System.Windows.Forms.ToolStrip timelineToolstrip;
        private System.Windows.Forms.ToolStripLabel lblTimeline;
        private System.Windows.Forms.ToolStripButton btnTimelineZoomToFit;
        private System.Windows.Forms.ToolStripButton btnTimelineZoomOut;
        private System.Windows.Forms.ToolStripButton btnTimelineZoomIn;
        private Controls.TimelineControl timelineControl;
        private System.Windows.Forms.TableLayoutPanel layoutSidebar;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Label lblProperties;
        private System.Windows.Forms.Label lblImages;
        private Controls.ListBoxEx listBoxImages;
        private Controls.PreviewControl previewControl;
        private System.Windows.Forms.Button btnTransportPrevious;
        private System.Windows.Forms.Button btnTransportPlayPause;
        private System.Windows.Forms.Button btnTransportNext;
        private System.Windows.Forms.Label lblTransportDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuItemImportImages;
        private System.Windows.Forms.OpenFileDialog openFileDialogImport;
        private System.Windows.Forms.BindingSource imageReferenceModelBindingSource;
        private System.Windows.Forms.ToolStripMenuItem menuProject;
        private System.Windows.Forms.ToolStripMenuItem menuItemProjectProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportVideo;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}