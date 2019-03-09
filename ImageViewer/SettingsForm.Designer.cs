namespace ImageViewer
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.panelSwitcher = new ImageViewer.Controls.PanelSwitcher();
            this.tabPageLibraries = new System.Windows.Forms.TabPage();
            this.grpLibraryMaintenance = new System.Windows.Forms.GroupBox();
            this.btnLibraryMaintenanceScan = new System.Windows.Forms.Button();
            this.lblLibraryMaintenanceScan = new System.Windows.Forms.Label();
            this.btnLibraryMaintenanceResetDeleted = new System.Windows.Forms.Button();
            this.lblLibraryMaintenanceResetDeleted = new System.Windows.Forms.Label();
            this.btnLibraryOpenLogs = new System.Windows.Forms.Button();
            this.chkLibraryAutoScan = new System.Windows.Forms.CheckBox();
            this.libraryPathsGroupBox = new System.Windows.Forms.GroupBox();
            this.btnLibraryPathsRemove = new System.Windows.Forms.Button();
            this.btnLibraryPathsAdd = new System.Windows.Forms.Button();
            this.libraryPathsList = new ImageViewer.Controls.ListBoxEx();
            this.libraryPathsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageBrowsing = new System.Windows.Forms.TabPage();
            this.chkBrowsingShowFolders = new System.Windows.Forms.CheckBox();
            this.chkBrowsingAutoNavigate = new System.Windows.Forms.CheckBox();
            this.tabPageViewing = new System.Windows.Forms.TabPage();
            this.grpViewingFullscreen = new System.Windows.Forms.GroupBox();
            this.layoutViewingFullscreen = new System.Windows.Forms.TableLayoutPanel();
            this.btnViewingFullscreenBackColor = new ImageViewer.Controls.ColorButton();
            this.numViewingFullscreenTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblViewingFullscreenTimeoutUnit = new System.Windows.Forms.Label();
            this.lblViewingFullscreenBackColor = new System.Windows.Forms.Label();
            this.lblViewingFullscreenTimeout = new System.Windows.Forms.Label();
            this.chkViewingFulllscreenAutoPlay = new System.Windows.Forms.CheckBox();
            this.lblViewingFullscreenAutoPlay = new System.Windows.Forms.Label();
            this.grpViewingZoomLevels = new System.Windows.Forms.GroupBox();
            this.lblViewingZoomLevelsHelp = new System.Windows.Forms.Label();
            this.txtViewingZoomLevels = new System.Windows.Forms.TextBox();
            this.tabListBox = new ImageViewer.Controls.ListBoxEx();
            this.tabPageEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.chkLibraryFullScan = new System.Windows.Forms.CheckBox();
            this.btnLibraryMaintenanceResetDatabase = new System.Windows.Forms.Button();
            this.lblLibraryMaintenanceResetDatabase = new System.Windows.Forms.Label();
            this.panelSwitcher.SuspendLayout();
            this.tabPageLibraries.SuspendLayout();
            this.grpLibraryMaintenance.SuspendLayout();
            this.libraryPathsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.libraryPathsBindingSource)).BeginInit();
            this.tabPageBrowsing.SuspendLayout();
            this.tabPageViewing.SuspendLayout();
            this.grpViewingFullscreen.SuspendLayout();
            this.layoutViewingFullscreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numViewingFullscreenTimeout)).BeginInit();
            this.grpViewingZoomLevels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEntryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSwitcher
            // 
            resources.ApplyResources(this.panelSwitcher, "panelSwitcher");
            this.panelSwitcher.Controls.Add(this.tabPageLibraries);
            this.panelSwitcher.Controls.Add(this.tabPageBrowsing);
            this.panelSwitcher.Controls.Add(this.tabPageViewing);
            this.panelSwitcher.Name = "panelSwitcher";
            this.panelSwitcher.SelectedIndex = 0;
            // 
            // tabPageLibraries
            // 
            this.tabPageLibraries.Controls.Add(this.chkLibraryFullScan);
            this.tabPageLibraries.Controls.Add(this.grpLibraryMaintenance);
            this.tabPageLibraries.Controls.Add(this.btnLibraryOpenLogs);
            this.tabPageLibraries.Controls.Add(this.chkLibraryAutoScan);
            this.tabPageLibraries.Controls.Add(this.libraryPathsGroupBox);
            resources.ApplyResources(this.tabPageLibraries, "tabPageLibraries");
            this.tabPageLibraries.Name = "tabPageLibraries";
            this.tabPageLibraries.UseVisualStyleBackColor = true;
            // 
            // grpLibraryMaintenance
            // 
            resources.ApplyResources(this.grpLibraryMaintenance, "grpLibraryMaintenance");
            this.grpLibraryMaintenance.Controls.Add(this.lblLibraryMaintenanceResetDatabase);
            this.grpLibraryMaintenance.Controls.Add(this.btnLibraryMaintenanceResetDatabase);
            this.grpLibraryMaintenance.Controls.Add(this.btnLibraryMaintenanceScan);
            this.grpLibraryMaintenance.Controls.Add(this.lblLibraryMaintenanceScan);
            this.grpLibraryMaintenance.Controls.Add(this.btnLibraryMaintenanceResetDeleted);
            this.grpLibraryMaintenance.Controls.Add(this.lblLibraryMaintenanceResetDeleted);
            this.grpLibraryMaintenance.Name = "grpLibraryMaintenance";
            this.grpLibraryMaintenance.TabStop = false;
            // 
            // btnLibraryMaintenanceScan
            // 
            resources.ApplyResources(this.btnLibraryMaintenanceScan, "btnLibraryMaintenanceScan");
            this.btnLibraryMaintenanceScan.Name = "btnLibraryMaintenanceScan";
            this.btnLibraryMaintenanceScan.UseVisualStyleBackColor = true;
            this.btnLibraryMaintenanceScan.Click += new System.EventHandler(this.OnLibraryMaintenanceScanClick);
            // 
            // lblLibraryMaintenanceScan
            // 
            resources.ApplyResources(this.lblLibraryMaintenanceScan, "lblLibraryMaintenanceScan");
            this.lblLibraryMaintenanceScan.Name = "lblLibraryMaintenanceScan";
            // 
            // btnLibraryMaintenanceResetDeleted
            // 
            resources.ApplyResources(this.btnLibraryMaintenanceResetDeleted, "btnLibraryMaintenanceResetDeleted");
            this.btnLibraryMaintenanceResetDeleted.Name = "btnLibraryMaintenanceResetDeleted";
            this.btnLibraryMaintenanceResetDeleted.UseVisualStyleBackColor = true;
            this.btnLibraryMaintenanceResetDeleted.Click += new System.EventHandler(this.OnLibraryMaintenanceResetDeletedClick);
            // 
            // lblLibraryMaintenanceResetDeleted
            // 
            resources.ApplyResources(this.lblLibraryMaintenanceResetDeleted, "lblLibraryMaintenanceResetDeleted");
            this.lblLibraryMaintenanceResetDeleted.Name = "lblLibraryMaintenanceResetDeleted";
            // 
            // btnLibraryOpenLogs
            // 
            resources.ApplyResources(this.btnLibraryOpenLogs, "btnLibraryOpenLogs");
            this.btnLibraryOpenLogs.Name = "btnLibraryOpenLogs";
            this.btnLibraryOpenLogs.UseVisualStyleBackColor = true;
            this.btnLibraryOpenLogs.Click += new System.EventHandler(this.OnLibraryOpenLogsClick);
            // 
            // chkLibraryAutoScan
            // 
            resources.ApplyResources(this.chkLibraryAutoScan, "chkLibraryAutoScan");
            this.chkLibraryAutoScan.Checked = global::ImageViewer.Properties.Settings.Default.LibraryAutoScan;
            this.chkLibraryAutoScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLibraryAutoScan.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ImageViewer.Properties.Settings.Default, "LibraryAutoScan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLibraryAutoScan.Name = "chkLibraryAutoScan";
            this.chkLibraryAutoScan.UseVisualStyleBackColor = true;
            // 
            // libraryPathsGroupBox
            // 
            resources.ApplyResources(this.libraryPathsGroupBox, "libraryPathsGroupBox");
            this.libraryPathsGroupBox.Controls.Add(this.btnLibraryPathsRemove);
            this.libraryPathsGroupBox.Controls.Add(this.btnLibraryPathsAdd);
            this.libraryPathsGroupBox.Controls.Add(this.libraryPathsList);
            this.libraryPathsGroupBox.Name = "libraryPathsGroupBox";
            this.libraryPathsGroupBox.TabStop = false;
            // 
            // btnLibraryPathsRemove
            // 
            this.btnLibraryPathsRemove.Image = global::ImageViewer.Properties.Resources.delete_16;
            resources.ApplyResources(this.btnLibraryPathsRemove, "btnLibraryPathsRemove");
            this.btnLibraryPathsRemove.Name = "btnLibraryPathsRemove";
            this.btnLibraryPathsRemove.UseVisualStyleBackColor = true;
            this.btnLibraryPathsRemove.Click += new System.EventHandler(this.OnLibraryPathsRemoveClick);
            // 
            // btnLibraryPathsAdd
            // 
            this.btnLibraryPathsAdd.Image = global::ImageViewer.Properties.Resources.add_16;
            resources.ApplyResources(this.btnLibraryPathsAdd, "btnLibraryPathsAdd");
            this.btnLibraryPathsAdd.Name = "btnLibraryPathsAdd";
            this.btnLibraryPathsAdd.UseVisualStyleBackColor = true;
            this.btnLibraryPathsAdd.Click += new System.EventHandler(this.OnLibraryPathsAddClick);
            // 
            // libraryPathsList
            // 
            resources.ApplyResources(this.libraryPathsList, "libraryPathsList");
            this.libraryPathsList.DataSource = this.libraryPathsBindingSource;
            this.libraryPathsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.libraryPathsList.FormattingEnabled = true;
            this.libraryPathsList.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.libraryPathsList.Name = "libraryPathsList";
            this.libraryPathsList.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnLibraryPathsListDrawItem);
            // 
            // libraryPathsBindingSource
            // 
            this.libraryPathsBindingSource.DataSource = typeof(string);
            // 
            // tabPageBrowsing
            // 
            this.tabPageBrowsing.Controls.Add(this.chkBrowsingShowFolders);
            this.tabPageBrowsing.Controls.Add(this.chkBrowsingAutoNavigate);
            resources.ApplyResources(this.tabPageBrowsing, "tabPageBrowsing");
            this.tabPageBrowsing.Name = "tabPageBrowsing";
            this.tabPageBrowsing.UseVisualStyleBackColor = true;
            // 
            // chkBrowsingShowFolders
            // 
            resources.ApplyResources(this.chkBrowsingShowFolders, "chkBrowsingShowFolders");
            this.chkBrowsingShowFolders.Checked = global::ImageViewer.Properties.Settings.Default.LibraryBrowserShowFolders;
            this.chkBrowsingShowFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBrowsingShowFolders.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ImageViewer.Properties.Settings.Default, "LibraryBrowserShowFolders", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBrowsingShowFolders.Name = "chkBrowsingShowFolders";
            this.chkBrowsingShowFolders.UseVisualStyleBackColor = true;
            // 
            // chkBrowsingAutoNavigate
            // 
            resources.ApplyResources(this.chkBrowsingAutoNavigate, "chkBrowsingAutoNavigate");
            this.chkBrowsingAutoNavigate.Checked = global::ImageViewer.Properties.Settings.Default.LibraryBrowserSyncNav;
            this.chkBrowsingAutoNavigate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBrowsingAutoNavigate.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ImageViewer.Properties.Settings.Default, "LibraryBrowserSyncNav", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBrowsingAutoNavigate.Name = "chkBrowsingAutoNavigate";
            this.chkBrowsingAutoNavigate.UseVisualStyleBackColor = true;
            // 
            // tabPageViewing
            // 
            this.tabPageViewing.Controls.Add(this.grpViewingFullscreen);
            this.tabPageViewing.Controls.Add(this.grpViewingZoomLevels);
            resources.ApplyResources(this.tabPageViewing, "tabPageViewing");
            this.tabPageViewing.Name = "tabPageViewing";
            this.tabPageViewing.UseVisualStyleBackColor = true;
            // 
            // grpViewingFullscreen
            // 
            resources.ApplyResources(this.grpViewingFullscreen, "grpViewingFullscreen");
            this.grpViewingFullscreen.Controls.Add(this.layoutViewingFullscreen);
            this.grpViewingFullscreen.Name = "grpViewingFullscreen";
            this.grpViewingFullscreen.TabStop = false;
            // 
            // layoutViewingFullscreen
            // 
            resources.ApplyResources(this.layoutViewingFullscreen, "layoutViewingFullscreen");
            this.layoutViewingFullscreen.Controls.Add(this.btnViewingFullscreenBackColor, 1, 2);
            this.layoutViewingFullscreen.Controls.Add(this.numViewingFullscreenTimeout, 1, 1);
            this.layoutViewingFullscreen.Controls.Add(this.lblViewingFullscreenTimeoutUnit, 2, 1);
            this.layoutViewingFullscreen.Controls.Add(this.lblViewingFullscreenBackColor, 0, 2);
            this.layoutViewingFullscreen.Controls.Add(this.lblViewingFullscreenTimeout, 0, 1);
            this.layoutViewingFullscreen.Controls.Add(this.chkViewingFulllscreenAutoPlay, 1, 0);
            this.layoutViewingFullscreen.Controls.Add(this.lblViewingFullscreenAutoPlay, 0, 0);
            this.layoutViewingFullscreen.Name = "layoutViewingFullscreen";
            // 
            // btnViewingFullscreenBackColor
            // 
            this.btnViewingFullscreenBackColor.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::ImageViewer.Properties.Settings.Default, "FullscreenBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnViewingFullscreenBackColor.ForeColor = global::ImageViewer.Properties.Settings.Default.FullscreenBackColor;
            resources.ApplyResources(this.btnViewingFullscreenBackColor, "btnViewingFullscreenBackColor");
            this.btnViewingFullscreenBackColor.Name = "btnViewingFullscreenBackColor";
            this.btnViewingFullscreenBackColor.UseVisualStyleBackColor = true;
            this.btnViewingFullscreenBackColor.Click += new System.EventHandler(this.OnViewingFullscreenBackColorClick);
            // 
            // numViewingFullscreenTimeout
            // 
            resources.ApplyResources(this.numViewingFullscreenTimeout, "numViewingFullscreenTimeout");
            this.numViewingFullscreenTimeout.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::ImageViewer.Properties.Settings.Default, "FullscreenAutoPlay", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numViewingFullscreenTimeout.Enabled = global::ImageViewer.Properties.Settings.Default.FullscreenAutoPlay;
            this.numViewingFullscreenTimeout.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numViewingFullscreenTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numViewingFullscreenTimeout.Name = "numViewingFullscreenTimeout";
            this.numViewingFullscreenTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numViewingFullscreenTimeout.ValueChanged += new System.EventHandler(this.OnViewingFullscreenTimeoutValueChanged);
            // 
            // lblViewingFullscreenTimeoutUnit
            // 
            resources.ApplyResources(this.lblViewingFullscreenTimeoutUnit, "lblViewingFullscreenTimeoutUnit");
            this.lblViewingFullscreenTimeoutUnit.Name = "lblViewingFullscreenTimeoutUnit";
            // 
            // lblViewingFullscreenBackColor
            // 
            resources.ApplyResources(this.lblViewingFullscreenBackColor, "lblViewingFullscreenBackColor");
            this.lblViewingFullscreenBackColor.Name = "lblViewingFullscreenBackColor";
            // 
            // lblViewingFullscreenTimeout
            // 
            resources.ApplyResources(this.lblViewingFullscreenTimeout, "lblViewingFullscreenTimeout");
            this.lblViewingFullscreenTimeout.Name = "lblViewingFullscreenTimeout";
            // 
            // chkViewingFulllscreenAutoPlay
            // 
            resources.ApplyResources(this.chkViewingFulllscreenAutoPlay, "chkViewingFulllscreenAutoPlay");
            this.chkViewingFulllscreenAutoPlay.Checked = global::ImageViewer.Properties.Settings.Default.FullscreenAutoPlay;
            this.chkViewingFulllscreenAutoPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewingFulllscreenAutoPlay.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ImageViewer.Properties.Settings.Default, "FullscreenAutoPlay", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkViewingFulllscreenAutoPlay.Name = "chkViewingFulllscreenAutoPlay";
            this.chkViewingFulllscreenAutoPlay.UseVisualStyleBackColor = true;
            // 
            // lblViewingFullscreenAutoPlay
            // 
            resources.ApplyResources(this.lblViewingFullscreenAutoPlay, "lblViewingFullscreenAutoPlay");
            this.lblViewingFullscreenAutoPlay.Name = "lblViewingFullscreenAutoPlay";
            // 
            // grpViewingZoomLevels
            // 
            resources.ApplyResources(this.grpViewingZoomLevels, "grpViewingZoomLevels");
            this.grpViewingZoomLevels.Controls.Add(this.lblViewingZoomLevelsHelp);
            this.grpViewingZoomLevels.Controls.Add(this.txtViewingZoomLevels);
            this.grpViewingZoomLevels.Name = "grpViewingZoomLevels";
            this.grpViewingZoomLevels.TabStop = false;
            // 
            // lblViewingZoomLevelsHelp
            // 
            resources.ApplyResources(this.lblViewingZoomLevelsHelp, "lblViewingZoomLevelsHelp");
            this.lblViewingZoomLevelsHelp.Name = "lblViewingZoomLevelsHelp";
            // 
            // txtViewingZoomLevels
            // 
            resources.ApplyResources(this.txtViewingZoomLevels, "txtViewingZoomLevels");
            this.txtViewingZoomLevels.Name = "txtViewingZoomLevels";
            this.txtViewingZoomLevels.TextChanged += new System.EventHandler(this.OnViewingZoomLevelsTextChanged);
            // 
            // tabListBox
            // 
            resources.ApplyResources(this.tabListBox, "tabListBox");
            this.tabListBox.DataSource = this.tabPageEntryBindingSource;
            this.tabListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tabListBox.FormattingEnabled = true;
            this.tabListBox.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tabListBox.Name = "tabListBox";
            this.tabListBox.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnDrawItemEx);
            // 
            // tabPageEntryBindingSource
            // 
            this.tabPageEntryBindingSource.DataSource = typeof(ImageViewer.Controls.TabPageEntry);
            this.tabPageEntryBindingSource.PositionChanged += new System.EventHandler(this.OnTabPageChange);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // chkLibraryFullScan
            // 
            resources.ApplyResources(this.chkLibraryFullScan, "chkLibraryFullScan");
            this.chkLibraryFullScan.Checked = global::ImageViewer.Properties.Settings.Default.LibraryFullScan;
            this.chkLibraryFullScan.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ImageViewer.Properties.Settings.Default, "LibraryFullScan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLibraryFullScan.Name = "chkLibraryFullScan";
            this.chkLibraryFullScan.UseVisualStyleBackColor = true;
            // 
            // btnLibraryMaintenanceResetDatabase
            // 
            resources.ApplyResources(this.btnLibraryMaintenanceResetDatabase, "btnLibraryMaintenanceResetDatabase");
            this.btnLibraryMaintenanceResetDatabase.Name = "btnLibraryMaintenanceResetDatabase";
            this.btnLibraryMaintenanceResetDatabase.UseVisualStyleBackColor = true;
            this.btnLibraryMaintenanceResetDatabase.Click += new System.EventHandler(this.OnLibraryMaintenanceResetDatabaseClick);
            // 
            // lblLibraryMaintenanceResetDatabase
            // 
            resources.ApplyResources(this.lblLibraryMaintenanceResetDatabase, "lblLibraryMaintenanceResetDatabase");
            this.lblLibraryMaintenanceResetDatabase.ForeColor = System.Drawing.Color.Red;
            this.lblLibraryMaintenanceResetDatabase.Name = "lblLibraryMaintenanceResetDatabase";
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabListBox);
            this.Controls.Add(this.panelSwitcher);
            this.Name = "SettingsForm";
            this.panelSwitcher.ResumeLayout(false);
            this.tabPageLibraries.ResumeLayout(false);
            this.tabPageLibraries.PerformLayout();
            this.grpLibraryMaintenance.ResumeLayout(false);
            this.libraryPathsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.libraryPathsBindingSource)).EndInit();
            this.tabPageBrowsing.ResumeLayout(false);
            this.tabPageBrowsing.PerformLayout();
            this.tabPageViewing.ResumeLayout(false);
            this.grpViewingFullscreen.ResumeLayout(false);
            this.layoutViewingFullscreen.ResumeLayout(false);
            this.layoutViewingFullscreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numViewingFullscreenTimeout)).EndInit();
            this.grpViewingZoomLevels.ResumeLayout(false);
            this.grpViewingZoomLevels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEntryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PanelSwitcher panelSwitcher;
        private System.Windows.Forms.TabPage tabPageLibraries;
        private System.Windows.Forms.TabPage tabPageBrowsing;
        private Controls.ListBoxEx tabListBox;
        private System.Windows.Forms.BindingSource tabPageEntryBindingSource;
        private System.Windows.Forms.TabPage tabPageViewing;
        private System.Windows.Forms.GroupBox libraryPathsGroupBox;
        private System.Windows.Forms.Button btnLibraryPathsRemove;
        private System.Windows.Forms.Button btnLibraryPathsAdd;
        private Controls.ListBoxEx libraryPathsList;
        private System.Windows.Forms.BindingSource libraryPathsBindingSource;
        private System.Windows.Forms.CheckBox chkLibraryAutoScan;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnLibraryOpenLogs;
        private System.Windows.Forms.CheckBox chkBrowsingShowFolders;
        private System.Windows.Forms.CheckBox chkBrowsingAutoNavigate;
        private System.Windows.Forms.GroupBox grpViewingZoomLevels;
        private System.Windows.Forms.Label lblViewingZoomLevelsHelp;
        private System.Windows.Forms.TextBox txtViewingZoomLevels;
        private System.Windows.Forms.GroupBox grpLibraryMaintenance;
        private System.Windows.Forms.Button btnLibraryMaintenanceScan;
        private System.Windows.Forms.Label lblLibraryMaintenanceScan;
        private System.Windows.Forms.Button btnLibraryMaintenanceResetDeleted;
        private System.Windows.Forms.Label lblLibraryMaintenanceResetDeleted;
        private System.Windows.Forms.GroupBox grpViewingFullscreen;
        private System.Windows.Forms.CheckBox chkViewingFulllscreenAutoPlay;
        private System.Windows.Forms.Label lblViewingFullscreenTimeout;
        private System.Windows.Forms.NumericUpDown numViewingFullscreenTimeout;
        private System.Windows.Forms.Label lblViewingFullscreenTimeoutUnit;
        private Controls.ColorButton btnViewingFullscreenBackColor;
        private System.Windows.Forms.Label lblViewingFullscreenBackColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TableLayoutPanel layoutViewingFullscreen;
        private System.Windows.Forms.Label lblViewingFullscreenAutoPlay;
        private System.Windows.Forms.CheckBox chkLibraryFullScan;
        private System.Windows.Forms.Label lblLibraryMaintenanceResetDatabase;
        private System.Windows.Forms.Button btnLibraryMaintenanceResetDatabase;
    }
}