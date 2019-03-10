namespace ImageViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLibraryBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.tagManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorWindowImages = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemWindowImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.errorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblZoom,
            this.lblFileSize,
            this.lblImageSize});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Spring = true;
            // 
            // lblZoom
            // 
            this.lblZoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblZoom.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblZoom.Name = "lblZoom";
            resources.ApplyResources(this.lblZoom, "lblZoom");
            // 
            // lblFileSize
            // 
            this.lblFileSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblFileSize.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblFileSize.Name = "lblFileSize";
            resources.ApplyResources(this.lblFileSize, "lblFileSize");
            // 
            // lblImageSize
            // 
            this.lblImageSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblImageSize.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblImageSize.Name = "lblImageSize";
            resources.ApplyResources(this.lblImageSize, "lblImageSize");
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuView,
            this.menuWindow,
            this.menuHelp});
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = global::ImageViewer.Properties.Resources.cross_16;
            this.menuItemExit.Name = "menuItemExit";
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            this.menuItemExit.Click += new System.EventHandler(this.OnExitClick);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditSettings});
            this.menuEdit.Name = "menuEdit";
            resources.ApplyResources(this.menuEdit, "menuEdit");
            // 
            // menuItemEditSettings
            // 
            this.menuItemEditSettings.Image = global::ImageViewer.Properties.Resources.setting_tools_16;
            this.menuItemEditSettings.Name = "menuItemEditSettings";
            resources.ApplyResources(this.menuItemEditSettings, "menuItemEditSettings");
            this.menuItemEditSettings.Click += new System.EventHandler(this.OnShowSettingsClick);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewLogs});
            this.menuView.Name = "menuView";
            resources.ApplyResources(this.menuView, "menuView");
            // 
            // menuItemViewLogs
            // 
            this.menuItemViewLogs.Name = "menuItemViewLogs";
            resources.ApplyResources(this.menuItemViewLogs, "menuItemViewLogs");
            this.menuItemViewLogs.Click += new System.EventHandler(this.OnViewLogsClick);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWindowCascade,
            this.menuItemWindowClose,
            this.toolStripSeparator2,
            this.menuItemLibraryBrowser,
            this.tagManagerToolStripMenuItem,
            this.menuItemSettings,
            this.toolStripSeparatorWindowImages,
            this.menuItemWindowImages,
            this.toolStripSeparator1,
            this.errorLogToolStripMenuItem});
            this.menuWindow.Name = "menuWindow";
            resources.ApplyResources(this.menuWindow, "menuWindow");
            // 
            // menuItemWindowCascade
            // 
            this.menuItemWindowCascade.Image = global::ImageViewer.Properties.Resources.application_cascade_16;
            this.menuItemWindowCascade.Name = "menuItemWindowCascade";
            resources.ApplyResources(this.menuItemWindowCascade, "menuItemWindowCascade");
            this.menuItemWindowCascade.Click += new System.EventHandler(this.OnWindowCascadeClick);
            // 
            // menuItemWindowClose
            // 
            this.menuItemWindowClose.Image = global::ImageViewer.Properties.Resources.application_delete_16;
            this.menuItemWindowClose.Name = "menuItemWindowClose";
            resources.ApplyResources(this.menuItemWindowClose, "menuItemWindowClose");
            this.menuItemWindowClose.Click += new System.EventHandler(this.OnWindowCloseClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItemLibraryBrowser
            // 
            this.menuItemLibraryBrowser.Image = global::ImageViewer.Properties.Resources.folder_picture_16;
            this.menuItemLibraryBrowser.Name = "menuItemLibraryBrowser";
            resources.ApplyResources(this.menuItemLibraryBrowser, "menuItemLibraryBrowser");
            this.menuItemLibraryBrowser.Click += new System.EventHandler(this.OnShowLibraryBrowserClick);
            // 
            // tagManagerToolStripMenuItem
            // 
            this.tagManagerToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.tags_16;
            this.tagManagerToolStripMenuItem.Name = "tagManagerToolStripMenuItem";
            resources.ApplyResources(this.tagManagerToolStripMenuItem, "tagManagerToolStripMenuItem");
            this.tagManagerToolStripMenuItem.Click += new System.EventHandler(this.OnShowTagManagerClick);
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Image = global::ImageViewer.Properties.Resources.setting_tools_16;
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.OnShowSettingsClick);
            // 
            // toolStripSeparatorWindowImages
            // 
            this.toolStripSeparatorWindowImages.Name = "toolStripSeparatorWindowImages";
            resources.ApplyResources(this.toolStripSeparatorWindowImages, "toolStripSeparatorWindowImages");
            // 
            // menuItemWindowImages
            // 
            this.menuItemWindowImages.Image = global::ImageViewer.Properties.Resources.picture_16;
            this.menuItemWindowImages.Name = "menuItemWindowImages";
            resources.ApplyResources(this.menuItemWindowImages, "menuItemWindowImages");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // errorLogToolStripMenuItem
            // 
            this.errorLogToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.exclamation_16;
            this.errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            resources.ApplyResources(this.errorLogToolStripMenuItem, "errorLogToolStripMenuItem");
            this.errorLogToolStripMenuItem.Click += new System.EventHandler(this.OnShowErrorLogClick);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpAbout});
            this.menuHelp.Name = "menuHelp";
            resources.ApplyResources(this.menuHelp, "menuHelp");
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Name = "menuItemHelpAbout";
            resources.ApplyResources(this.menuItemHelpAbout, "menuItemHelpAbout");
            this.menuItemHelpAbout.Click += new System.EventHandler(this.OnMenuItemAboutClick);
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.RestoreDirectory = true;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel lblZoom;
        private System.Windows.Forms.ToolStripStatusLabel lblFileSize;
        private System.Windows.Forms.ToolStripStatusLabel lblImageSize;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemLibraryBrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorWindowImages;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowImages;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem tagManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewLogs;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditSettings;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowCascade;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

