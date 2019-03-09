namespace ImageViewer
{
    partial class ImageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
            this.imageView = new Cyotek.Windows.Forms.ImageBox();
            this.layout = new System.Windows.Forms.SplitContainer();
            this.infoLayout = new System.Windows.Forms.Panel();
            this.layoutMetadata = new System.Windows.Forms.TreeView();
            this.headerInfoMetadata = new System.Windows.Forms.ToolStrip();
            this.btnInfoMetadataToggle = new System.Windows.Forms.ToolStripButton();
            this.lblInfoMetadata = new System.Windows.Forms.ToolStripLabel();
            this.layoutInfoTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInfoTagAdd = new System.Windows.Forms.Button();
            this.headerInfoTags = new System.Windows.Forms.ToolStrip();
            this.btnInfoTagsToggle = new System.Windows.Forms.ToolStripButton();
            this.lblInfoTags = new System.Windows.Forms.ToolStripLabel();
            this.layoutInfoDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblInfoDetailsSize = new System.Windows.Forms.Label();
            this.lblInfoDetailsFileSize = new System.Windows.Forms.Label();
            this.lblInfoDetailsFormat = new System.Windows.Forms.Label();
            this.lblInfoDetailsSizeValue = new System.Windows.Forms.Label();
            this.lblInfoDetailsFileSizeValue = new System.Windows.Forms.Label();
            this.lblInfoDetailsFormatValue = new System.Windows.Forms.Label();
            this.lblInfoDetailsBitsPerPixel = new System.Windows.Forms.Label();
            this.lblInfoDetailsBitsPerPixelValue = new System.Windows.Forms.Label();
            this.lblInfoDetailsModifiedDate = new System.Windows.Forms.Label();
            this.lblInfoDetailsModifiedDateValue = new System.Windows.Forms.Label();
            this.lblInfoDetailsTitle = new System.Windows.Forms.Label();
            this.txtInfoDetailsTitle = new System.Windows.Forms.TextBox();
            this.btnInfoDetailsTitleSave = new System.Windows.Forms.Button();
            this.headerInfoDetails = new System.Windows.Forms.ToolStrip();
            this.btnInfoDetailsToggle = new System.Windows.Forms.ToolStripButton();
            this.lblInfoDetails = new System.Windows.Forms.ToolStripLabel();
            this.toolStripInformation = new System.Windows.Forms.ToolStrip();
            this.lblInformationTitle = new System.Windows.Forms.ToolStripLabel();
            this.btnInformationClose = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomToFit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemActualSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomToFit = new System.Windows.Forms.ToolStripButton();
            this.btnActualSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInformation = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
            this.layout.Panel1.SuspendLayout();
            this.layout.Panel2.SuspendLayout();
            this.layout.SuspendLayout();
            this.infoLayout.SuspendLayout();
            this.headerInfoMetadata.SuspendLayout();
            this.layoutInfoTags.SuspendLayout();
            this.headerInfoTags.SuspendLayout();
            this.layoutInfoDetails.SuspendLayout();
            this.headerInfoDetails.SuspendLayout();
            this.toolStripInformation.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageView
            // 
            this.imageView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imageView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageView.DropShadowSize = 6;
            this.imageView.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image;
            this.imageView.ImageBorderStyle = Cyotek.Windows.Forms.ImageBoxBorderStyle.FixedSingleGlowShadow;
            this.imageView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imageView.Location = new System.Drawing.Point(0, 0);
            this.imageView.Name = "imageView";
            this.imageView.Size = new System.Drawing.Size(800, 450);
            this.imageView.TabIndex = 0;
            this.imageView.Zoomed += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxZoomEventArgs>(this.OnImageViewZoomed);
            // 
            // layout
            // 
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            // 
            // layout.Panel1
            // 
            this.layout.Panel1.Controls.Add(this.imageView);
            // 
            // layout.Panel2
            // 
            this.layout.Panel2.Controls.Add(this.infoLayout);
            this.layout.Panel2.Controls.Add(this.toolStripInformation);
            this.layout.Panel2Collapsed = true;
            this.layout.Size = new System.Drawing.Size(800, 450);
            this.layout.SplitterDistance = 522;
            this.layout.TabIndex = 1;
            // 
            // infoLayout
            // 
            this.infoLayout.Controls.Add(this.layoutMetadata);
            this.infoLayout.Controls.Add(this.headerInfoMetadata);
            this.infoLayout.Controls.Add(this.layoutInfoTags);
            this.infoLayout.Controls.Add(this.headerInfoTags);
            this.infoLayout.Controls.Add(this.layoutInfoDetails);
            this.infoLayout.Controls.Add(this.headerInfoDetails);
            this.infoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLayout.Location = new System.Drawing.Point(0, 25);
            this.infoLayout.Name = "infoLayout";
            this.infoLayout.Size = new System.Drawing.Size(274, 425);
            this.infoLayout.TabIndex = 1;
            // 
            // layoutMetadata
            // 
            this.layoutMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMetadata.Location = new System.Drawing.Point(0, 231);
            this.layoutMetadata.Name = "layoutMetadata";
            this.layoutMetadata.Size = new System.Drawing.Size(274, 194);
            this.layoutMetadata.TabIndex = 4;
            // 
            // headerInfoMetadata
            // 
            this.headerInfoMetadata.BackColor = System.Drawing.SystemColors.ControlDark;
            this.headerInfoMetadata.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerInfoMetadata.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInfoMetadataToggle,
            this.lblInfoMetadata});
            this.headerInfoMetadata.Location = new System.Drawing.Point(0, 206);
            this.headerInfoMetadata.Name = "headerInfoMetadata";
            this.headerInfoMetadata.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.headerInfoMetadata.Size = new System.Drawing.Size(274, 25);
            this.headerInfoMetadata.TabIndex = 3;
            // 
            // btnInfoMetadataToggle
            // 
            this.btnInfoMetadataToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfoMetadataToggle.Image = global::ImageViewer.Properties.Resources.toggle_collapse_16;
            this.btnInfoMetadataToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfoMetadataToggle.Name = "btnInfoMetadataToggle";
            this.btnInfoMetadataToggle.Size = new System.Drawing.Size(23, 22);
            this.btnInfoMetadataToggle.Text = "Collapse";
            this.btnInfoMetadataToggle.Click += new System.EventHandler(this.OnInfoMetadataToggleClick);
            // 
            // lblInfoMetadata
            // 
            this.lblInfoMetadata.Name = "lblInfoMetadata";
            this.lblInfoMetadata.Size = new System.Drawing.Size(57, 22);
            this.lblInfoMetadata.Text = "Metadata";
            // 
            // layoutInfoTags
            // 
            this.layoutInfoTags.AutoSize = true;
            this.layoutInfoTags.Controls.Add(this.btnInfoTagAdd);
            this.layoutInfoTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutInfoTags.Location = new System.Drawing.Point(0, 176);
            this.layoutInfoTags.Name = "layoutInfoTags";
            this.layoutInfoTags.Size = new System.Drawing.Size(274, 30);
            this.layoutInfoTags.TabIndex = 2;
            // 
            // btnInfoTagAdd
            // 
            this.btnInfoTagAdd.Image = global::ImageViewer.Properties.Resources.add_16;
            this.btnInfoTagAdd.Location = new System.Drawing.Point(3, 3);
            this.btnInfoTagAdd.Name = "btnInfoTagAdd";
            this.btnInfoTagAdd.Size = new System.Drawing.Size(24, 24);
            this.btnInfoTagAdd.TabIndex = 0;
            this.btnInfoTagAdd.UseVisualStyleBackColor = true;
            this.btnInfoTagAdd.Click += new System.EventHandler(this.OnInfoTagAddClick);
            // 
            // headerInfoTags
            // 
            this.headerInfoTags.BackColor = System.Drawing.SystemColors.ControlDark;
            this.headerInfoTags.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerInfoTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInfoTagsToggle,
            this.lblInfoTags});
            this.headerInfoTags.Location = new System.Drawing.Point(0, 151);
            this.headerInfoTags.Name = "headerInfoTags";
            this.headerInfoTags.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.headerInfoTags.Size = new System.Drawing.Size(274, 25);
            this.headerInfoTags.TabIndex = 0;
            this.headerInfoTags.Text = "toolStrip1";
            // 
            // btnInfoTagsToggle
            // 
            this.btnInfoTagsToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfoTagsToggle.Image = global::ImageViewer.Properties.Resources.toggle_collapse_16;
            this.btnInfoTagsToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfoTagsToggle.Name = "btnInfoTagsToggle";
            this.btnInfoTagsToggle.Size = new System.Drawing.Size(23, 22);
            this.btnInfoTagsToggle.Text = "Collapse";
            this.btnInfoTagsToggle.Click += new System.EventHandler(this.OnInfoTagsCollapseClick);
            // 
            // lblInfoTags
            // 
            this.lblInfoTags.Name = "lblInfoTags";
            this.lblInfoTags.Size = new System.Drawing.Size(31, 22);
            this.lblInfoTags.Text = "Tags";
            // 
            // layoutInfoDetails
            // 
            this.layoutInfoDetails.AutoSize = true;
            this.layoutInfoDetails.ColumnCount = 3;
            this.layoutInfoDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutInfoDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutInfoDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsSize, 0, 1);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsFileSize, 0, 2);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsFormat, 0, 3);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsSizeValue, 1, 1);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsFileSizeValue, 1, 2);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsFormatValue, 1, 3);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsBitsPerPixel, 0, 4);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsBitsPerPixelValue, 1, 4);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsModifiedDate, 0, 5);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsModifiedDateValue, 1, 5);
            this.layoutInfoDetails.Controls.Add(this.lblInfoDetailsTitle, 0, 0);
            this.layoutInfoDetails.Controls.Add(this.txtInfoDetailsTitle, 1, 0);
            this.layoutInfoDetails.Controls.Add(this.btnInfoDetailsTitleSave, 2, 0);
            this.layoutInfoDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutInfoDetails.Location = new System.Drawing.Point(0, 25);
            this.layoutInfoDetails.Name = "layoutInfoDetails";
            this.layoutInfoDetails.RowCount = 6;
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutInfoDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutInfoDetails.Size = new System.Drawing.Size(274, 126);
            this.layoutInfoDetails.TabIndex = 1;
            // 
            // lblInfoDetailsSize
            // 
            this.lblInfoDetailsSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsSize.AutoSize = true;
            this.lblInfoDetailsSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsSize.Location = new System.Drawing.Point(55, 33);
            this.lblInfoDetailsSize.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsSize.Name = "lblInfoDetailsSize";
            this.lblInfoDetailsSize.Size = new System.Drawing.Size(31, 13);
            this.lblInfoDetailsSize.TabIndex = 0;
            this.lblInfoDetailsSize.Text = "Size";
            // 
            // lblInfoDetailsFileSize
            // 
            this.lblInfoDetailsFileSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsFileSize.AutoSize = true;
            this.lblInfoDetailsFileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsFileSize.Location = new System.Drawing.Point(31, 52);
            this.lblInfoDetailsFileSize.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsFileSize.Name = "lblInfoDetailsFileSize";
            this.lblInfoDetailsFileSize.Size = new System.Drawing.Size(55, 13);
            this.lblInfoDetailsFileSize.TabIndex = 1;
            this.lblInfoDetailsFileSize.Text = "File Size";
            // 
            // lblInfoDetailsFormat
            // 
            this.lblInfoDetailsFormat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsFormat.AutoSize = true;
            this.lblInfoDetailsFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsFormat.Location = new System.Drawing.Point(41, 71);
            this.lblInfoDetailsFormat.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsFormat.Name = "lblInfoDetailsFormat";
            this.lblInfoDetailsFormat.Size = new System.Drawing.Size(45, 13);
            this.lblInfoDetailsFormat.TabIndex = 2;
            this.lblInfoDetailsFormat.Text = "Format";
            // 
            // lblInfoDetailsSizeValue
            // 
            this.lblInfoDetailsSizeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInfoDetailsSizeValue.AutoSize = true;
            this.layoutInfoDetails.SetColumnSpan(this.lblInfoDetailsSizeValue, 2);
            this.lblInfoDetailsSizeValue.Location = new System.Drawing.Point(89, 33);
            this.lblInfoDetailsSizeValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblInfoDetailsSizeValue.Name = "lblInfoDetailsSizeValue";
            this.lblInfoDetailsSizeValue.Size = new System.Drawing.Size(30, 13);
            this.lblInfoDetailsSizeValue.TabIndex = 3;
            this.lblInfoDetailsSizeValue.Text = "0 x 0";
            // 
            // lblInfoDetailsFileSizeValue
            // 
            this.lblInfoDetailsFileSizeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInfoDetailsFileSizeValue.AutoSize = true;
            this.layoutInfoDetails.SetColumnSpan(this.lblInfoDetailsFileSizeValue, 2);
            this.lblInfoDetailsFileSizeValue.Location = new System.Drawing.Point(89, 52);
            this.lblInfoDetailsFileSizeValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblInfoDetailsFileSizeValue.Name = "lblInfoDetailsFileSizeValue";
            this.lblInfoDetailsFileSizeValue.Size = new System.Drawing.Size(29, 13);
            this.lblInfoDetailsFileSizeValue.TabIndex = 4;
            this.lblInfoDetailsFileSizeValue.Text = "0 kB";
            // 
            // lblInfoDetailsFormatValue
            // 
            this.lblInfoDetailsFormatValue.AutoSize = true;
            this.layoutInfoDetails.SetColumnSpan(this.lblInfoDetailsFormatValue, 2);
            this.lblInfoDetailsFormatValue.Location = new System.Drawing.Point(89, 71);
            this.lblInfoDetailsFormatValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblInfoDetailsFormatValue.Name = "lblInfoDetailsFormatValue";
            this.lblInfoDetailsFormatValue.Size = new System.Drawing.Size(27, 13);
            this.lblInfoDetailsFormatValue.TabIndex = 5;
            this.lblInfoDetailsFormatValue.Text = "N/A";
            // 
            // lblInfoDetailsBitsPerPixel
            // 
            this.lblInfoDetailsBitsPerPixel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsBitsPerPixel.AutoSize = true;
            this.lblInfoDetailsBitsPerPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsBitsPerPixel.Location = new System.Drawing.Point(26, 90);
            this.lblInfoDetailsBitsPerPixel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsBitsPerPixel.Name = "lblInfoDetailsBitsPerPixel";
            this.lblInfoDetailsBitsPerPixel.Size = new System.Drawing.Size(60, 13);
            this.lblInfoDetailsBitsPerPixel.TabIndex = 6;
            this.lblInfoDetailsBitsPerPixel.Text = "Bit Depth";
            // 
            // lblInfoDetailsBitsPerPixelValue
            // 
            this.lblInfoDetailsBitsPerPixelValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInfoDetailsBitsPerPixelValue.AutoSize = true;
            this.layoutInfoDetails.SetColumnSpan(this.lblInfoDetailsBitsPerPixelValue, 2);
            this.lblInfoDetailsBitsPerPixelValue.Location = new System.Drawing.Point(89, 90);
            this.lblInfoDetailsBitsPerPixelValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblInfoDetailsBitsPerPixelValue.Name = "lblInfoDetailsBitsPerPixelValue";
            this.lblInfoDetailsBitsPerPixelValue.Size = new System.Drawing.Size(34, 13);
            this.lblInfoDetailsBitsPerPixelValue.TabIndex = 7;
            this.lblInfoDetailsBitsPerPixelValue.Text = "0 bpp";
            // 
            // lblInfoDetailsModifiedDate
            // 
            this.lblInfoDetailsModifiedDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsModifiedDate.AutoSize = true;
            this.lblInfoDetailsModifiedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsModifiedDate.Location = new System.Drawing.Point(3, 109);
            this.lblInfoDetailsModifiedDate.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsModifiedDate.Name = "lblInfoDetailsModifiedDate";
            this.lblInfoDetailsModifiedDate.Size = new System.Drawing.Size(83, 13);
            this.lblInfoDetailsModifiedDate.TabIndex = 8;
            this.lblInfoDetailsModifiedDate.Text = "Last Modified";
            // 
            // lblInfoDetailsModifiedDateValue
            // 
            this.lblInfoDetailsModifiedDateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInfoDetailsModifiedDateValue.AutoSize = true;
            this.layoutInfoDetails.SetColumnSpan(this.lblInfoDetailsModifiedDateValue, 2);
            this.lblInfoDetailsModifiedDateValue.Location = new System.Drawing.Point(89, 109);
            this.lblInfoDetailsModifiedDateValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblInfoDetailsModifiedDateValue.Name = "lblInfoDetailsModifiedDateValue";
            this.lblInfoDetailsModifiedDateValue.Size = new System.Drawing.Size(27, 13);
            this.lblInfoDetailsModifiedDateValue.TabIndex = 9;
            this.lblInfoDetailsModifiedDateValue.Text = "N/A";
            // 
            // lblInfoDetailsTitle
            // 
            this.lblInfoDetailsTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInfoDetailsTitle.AutoSize = true;
            this.lblInfoDetailsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDetailsTitle.Location = new System.Drawing.Point(54, 8);
            this.lblInfoDetailsTitle.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblInfoDetailsTitle.Name = "lblInfoDetailsTitle";
            this.lblInfoDetailsTitle.Size = new System.Drawing.Size(32, 13);
            this.lblInfoDetailsTitle.TabIndex = 10;
            this.lblInfoDetailsTitle.Text = "Title";
            // 
            // txtInfoDetailsTitle
            // 
            this.txtInfoDetailsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfoDetailsTitle.Location = new System.Drawing.Point(89, 5);
            this.txtInfoDetailsTitle.Name = "txtInfoDetailsTitle";
            this.txtInfoDetailsTitle.Size = new System.Drawing.Size(152, 20);
            this.txtInfoDetailsTitle.TabIndex = 11;
            this.txtInfoDetailsTitle.TextChanged += new System.EventHandler(this.OnInfoDetailsTitleTextChanged);
            // 
            // btnInfoDetailsTitleSave
            // 
            this.btnInfoDetailsTitleSave.Image = global::ImageViewer.Properties.Resources.diskette_16;
            this.btnInfoDetailsTitleSave.Location = new System.Drawing.Point(247, 3);
            this.btnInfoDetailsTitleSave.Name = "btnInfoDetailsTitleSave";
            this.btnInfoDetailsTitleSave.Size = new System.Drawing.Size(24, 24);
            this.btnInfoDetailsTitleSave.TabIndex = 12;
            this.btnInfoDetailsTitleSave.UseVisualStyleBackColor = true;
            this.btnInfoDetailsTitleSave.Click += new System.EventHandler(this.OnInfoDetailsTitleSaveClick);
            // 
            // headerInfoDetails
            // 
            this.headerInfoDetails.BackColor = System.Drawing.SystemColors.ControlDark;
            this.headerInfoDetails.CanOverflow = false;
            this.headerInfoDetails.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerInfoDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInfoDetailsToggle,
            this.lblInfoDetails});
            this.headerInfoDetails.Location = new System.Drawing.Point(0, 0);
            this.headerInfoDetails.Name = "headerInfoDetails";
            this.headerInfoDetails.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.headerInfoDetails.Size = new System.Drawing.Size(274, 25);
            this.headerInfoDetails.TabIndex = 0;
            // 
            // btnInfoDetailsToggle
            // 
            this.btnInfoDetailsToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfoDetailsToggle.Image = global::ImageViewer.Properties.Resources.toggle_collapse_16;
            this.btnInfoDetailsToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfoDetailsToggle.Name = "btnInfoDetailsToggle";
            this.btnInfoDetailsToggle.Size = new System.Drawing.Size(23, 22);
            this.btnInfoDetailsToggle.Text = "Collapse";
            this.btnInfoDetailsToggle.Click += new System.EventHandler(this.OnInfoDetailsToggleClick);
            // 
            // lblInfoDetails
            // 
            this.lblInfoDetails.Name = "lblInfoDetails";
            this.lblInfoDetails.Size = new System.Drawing.Size(42, 22);
            this.lblInfoDetails.Text = "Details";
            // 
            // toolStripInformation
            // 
            this.toolStripInformation.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripInformation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInformationTitle,
            this.btnInformationClose});
            this.toolStripInformation.Location = new System.Drawing.Point(0, 0);
            this.toolStripInformation.Name = "toolStripInformation";
            this.toolStripInformation.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripInformation.Size = new System.Drawing.Size(274, 25);
            this.toolStripInformation.TabIndex = 0;
            this.toolStripInformation.Text = "toolStrip1";
            // 
            // lblInformationTitle
            // 
            this.lblInformationTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblInformationTitle.Name = "lblInformationTitle";
            this.lblInformationTitle.Size = new System.Drawing.Size(70, 22);
            this.lblInformationTitle.Text = "Information";
            // 
            // btnInformationClose
            // 
            this.btnInformationClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnInformationClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInformationClose.Image = global::ImageViewer.Properties.Resources.cross_16;
            this.btnInformationClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInformationClose.Name = "btnInformationClose";
            this.btnInformationClose.Size = new System.Drawing.Size(23, 22);
            this.btnInformationClose.Text = "Close";
            this.btnInformationClose.Click += new System.EventHandler(this.OnInformationCloseClick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEdit,
            this.menuView});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Visible = false;
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.addTagsToolStripMenuItem,
            this.metadataToolStripMenuItem});
            this.menuEdit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "&Edit";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // addTagsToolStripMenuItem
            // 
            this.addTagsToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.tag_add_16;
            this.addTagsToolStripMenuItem.Name = "addTagsToolStripMenuItem";
            this.addTagsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.T)));
            this.addTagsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.addTagsToolStripMenuItem.Text = "Add Tags...";
            this.addTagsToolStripMenuItem.Click += new System.EventHandler(this.OnInfoTagAddClick);
            // 
            // metadataToolStripMenuItem
            // 
            this.metadataToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.information_16;
            this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
            this.metadataToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.metadataToolStripMenuItem.Text = "Information";
            this.metadataToolStripMenuItem.Click += new System.EventHandler(this.OnInfoShowClick);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.menuItemZoomIn,
            this.menuItemZoomOut,
            this.menuItemZoomToFit,
            this.menuItemActualSize});
            this.menuView.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "&View";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // menuItemZoomIn
            // 
            this.menuItemZoomIn.Image = global::ImageViewer.Properties.Resources.zoom_in_16;
            this.menuItemZoomIn.Name = "menuItemZoomIn";
            this.menuItemZoomIn.Size = new System.Drawing.Size(136, 22);
            this.menuItemZoomIn.Text = "Zoom In";
            this.menuItemZoomIn.Click += new System.EventHandler(this.OnZoomInClick);
            // 
            // menuItemZoomOut
            // 
            this.menuItemZoomOut.Image = global::ImageViewer.Properties.Resources.zoom_out_16;
            this.menuItemZoomOut.Name = "menuItemZoomOut";
            this.menuItemZoomOut.Size = new System.Drawing.Size(136, 22);
            this.menuItemZoomOut.Text = "Zoom Out";
            this.menuItemZoomOut.Click += new System.EventHandler(this.OnZoomOutClick);
            // 
            // menuItemZoomToFit
            // 
            this.menuItemZoomToFit.Image = global::ImageViewer.Properties.Resources.zoom_fit_16;
            this.menuItemZoomToFit.Name = "menuItemZoomToFit";
            this.menuItemZoomToFit.Size = new System.Drawing.Size(136, 22);
            this.menuItemZoomToFit.Text = "Zoom to Fit";
            this.menuItemZoomToFit.Click += new System.EventHandler(this.OnZoomToFitClick);
            // 
            // menuItemActualSize
            // 
            this.menuItemActualSize.Image = global::ImageViewer.Properties.Resources.zoom_actual_16;
            this.menuItemActualSize.Name = "menuItemActualSize";
            this.menuItemActualSize.Size = new System.Drawing.Size(136, 22);
            this.menuItemActualSize.Text = "Actual Size";
            this.menuItemActualSize.Click += new System.EventHandler(this.OnActualSizeClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnZoomToFit,
            this.btnActualSize,
            this.toolStripSeparator4,
            this.btnInformation});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Visible = false;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::ImageViewer.Properties.Resources.zoom_in_16;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.OnZoomInClick);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::ImageViewer.Properties.Resources.zoom_out_16;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.OnZoomOutClick);
            // 
            // btnZoomToFit
            // 
            this.btnZoomToFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomToFit.Image = global::ImageViewer.Properties.Resources.zoom_fit_16;
            this.btnZoomToFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomToFit.Name = "btnZoomToFit";
            this.btnZoomToFit.Size = new System.Drawing.Size(23, 22);
            this.btnZoomToFit.Text = "Zoom to Fit";
            this.btnZoomToFit.Click += new System.EventHandler(this.OnZoomToFitClick);
            // 
            // btnActualSize
            // 
            this.btnActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActualSize.Image = global::ImageViewer.Properties.Resources.zoom_actual_16;
            this.btnActualSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualSize.Name = "btnActualSize";
            this.btnActualSize.Size = new System.Drawing.Size(23, 22);
            this.btnActualSize.Text = "Actual Size";
            this.btnActualSize.Click += new System.EventHandler(this.OnActualSizeClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnInformation
            // 
            this.btnInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInformation.Image = global::ImageViewer.Properties.Resources.information_16;
            this.btnInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(23, 22);
            this.btnInformation.Text = "Show information";
            this.btnInformation.Click += new System.EventHandler(this.OnInfoShowClick);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ImageForm";
            this.Text = "Image";
            this.layout.Panel1.ResumeLayout(false);
            this.layout.Panel2.ResumeLayout(false);
            this.layout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
            this.layout.ResumeLayout(false);
            this.infoLayout.ResumeLayout(false);
            this.infoLayout.PerformLayout();
            this.headerInfoMetadata.ResumeLayout(false);
            this.headerInfoMetadata.PerformLayout();
            this.layoutInfoTags.ResumeLayout(false);
            this.headerInfoTags.ResumeLayout(false);
            this.headerInfoTags.PerformLayout();
            this.layoutInfoDetails.ResumeLayout(false);
            this.layoutInfoDetails.PerformLayout();
            this.headerInfoDetails.ResumeLayout(false);
            this.headerInfoDetails.PerformLayout();
            this.toolStripInformation.ResumeLayout(false);
            this.toolStripInformation.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox imageView;
        private System.Windows.Forms.SplitContainer layout;
        private System.Windows.Forms.ToolStrip toolStripInformation;
        private System.Windows.Forms.ToolStripLabel lblInformationTitle;
        private System.Windows.Forms.ToolStripButton btnInformationClose;
        private System.Windows.Forms.Panel infoLayout;
        private System.Windows.Forms.ToolStrip headerInfoDetails;
        private System.Windows.Forms.ToolStripButton btnInfoDetailsToggle;
        private System.Windows.Forms.ToolStripLabel lblInfoDetails;
        private System.Windows.Forms.TableLayoutPanel layoutInfoDetails;
        private System.Windows.Forms.Label lblInfoDetailsSize;
        private System.Windows.Forms.Label lblInfoDetailsFileSize;
        private System.Windows.Forms.Label lblInfoDetailsFormat;
        private System.Windows.Forms.Label lblInfoDetailsSizeValue;
        private System.Windows.Forms.Label lblInfoDetailsFileSizeValue;
        private System.Windows.Forms.Label lblInfoDetailsFormatValue;
        private System.Windows.Forms.Label lblInfoDetailsBitsPerPixel;
        private System.Windows.Forms.Label lblInfoDetailsBitsPerPixelValue;
        private System.Windows.Forms.Label lblInfoDetailsModifiedDate;
        private System.Windows.Forms.Label lblInfoDetailsModifiedDateValue;
        private System.Windows.Forms.ToolStrip headerInfoTags;
        private System.Windows.Forms.ToolStripButton btnInfoTagsToggle;
        private System.Windows.Forms.ToolStripLabel lblInfoTags;
        private System.Windows.Forms.FlowLayoutPanel layoutInfoTags;
        private System.Windows.Forms.Button btnInfoTagAdd;
        private System.Windows.Forms.ToolStrip headerInfoMetadata;
        private System.Windows.Forms.ToolStripButton btnInfoMetadataToggle;
        private System.Windows.Forms.ToolStripLabel lblInfoMetadata;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomOut;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomToFit;
        private System.Windows.Forms.ToolStripMenuItem menuItemActualSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnZoomToFit;
        private System.Windows.Forms.ToolStripButton btnActualSize;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem addTagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnInformation;
        private System.Windows.Forms.TreeView layoutMetadata;
        private System.Windows.Forms.Label lblInfoDetailsTitle;
        private System.Windows.Forms.TextBox txtInfoDetailsTitle;
        private System.Windows.Forms.Button btnInfoDetailsTitleSave;
    }
}