namespace ImageViewer
{
    partial class ErrorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorsForm));
            this.layout = new System.Windows.Forms.SplitContainer();
            this.listContainer = new System.Windows.Forms.Panel();
            this.logList = new ImageViewer.Controls.LogListControl();
            this.logDetailsView = new ImageViewer.Controls.LogDetailsView();
            this.headerInfo = new System.Windows.Forms.ToolStrip();
            this.lblHeaderInfo = new System.Windows.Forms.ToolStripLabel();
            this.btnInfoClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
            this.layout.Panel1.SuspendLayout();
            this.layout.Panel2.SuspendLayout();
            this.layout.SuspendLayout();
            this.listContainer.SuspendLayout();
            this.headerInfo.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            resources.ApplyResources(this.layout, "layout");
            this.layout.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.layout.Name = "layout";
            // 
            // layout.Panel1
            // 
            this.layout.Panel1.Controls.Add(this.listContainer);
            // 
            // layout.Panel2
            // 
            resources.ApplyResources(this.layout.Panel2, "layout.Panel2");
            this.layout.Panel2.Controls.Add(this.logDetailsView);
            this.layout.Panel2.Controls.Add(this.headerInfo);
            this.layout.Panel2Collapsed = true;
            // 
            // listContainer
            // 
            this.listContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listContainer.Controls.Add(this.logList);
            resources.ApplyResources(this.listContainer, "listContainer");
            this.listContainer.Name = "listContainer";
            // 
            // logList
            // 
            resources.ApplyResources(this.logList, "logList");
            this.logList.DataSource = null;
            this.logList.ForeColor = System.Drawing.Color.Firebrick;
            this.logList.ItemPadding = new System.Windows.Forms.Padding(3);
            this.logList.Name = "logList";
            this.logList.SelectedIndex = 0;
            this.logList.SelectedIndexChanged += new System.EventHandler(this.OnLogListSelectedIndexChanged);
            // 
            // logDetailsView
            // 
            resources.ApplyResources(this.logDetailsView, "logDetailsView");
            this.logDetailsView.CellSpacing = 0;
            this.logDetailsView.Name = "logDetailsView";
            this.logDetailsView.SelectedItem = null;
            // 
            // headerInfo
            // 
            this.headerInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.headerInfo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblHeaderInfo,
            this.btnInfoClose});
            resources.ApplyResources(this.headerInfo, "headerInfo");
            this.headerInfo.Name = "headerInfo";
            this.headerInfo.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // lblHeaderInfo
            // 
            this.lblHeaderInfo.Name = "lblHeaderInfo";
            resources.ApplyResources(this.lblHeaderInfo, "lblHeaderInfo");
            // 
            // btnInfoClose
            // 
            this.btnInfoClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnInfoClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfoClose.Image = global::ImageViewer.Properties.Resources.cross_16;
            resources.ApplyResources(this.btnInfoClose, "btnInfoClose");
            this.btnInfoClose.Name = "btnInfoClose";
            this.btnInfoClose.Click += new System.EventHandler(this.OnInfoCloseClick);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInfo,
            this.toolStripSeparator1,
            this.btnClear});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // btnInfo
            // 
            this.btnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfo.Image = global::ImageViewer.Properties.Resources.information_16;
            resources.ApplyResources(this.btnInfo, "btnInfo");
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Click += new System.EventHandler(this.OnInfoClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = global::ImageViewer.Properties.Resources.cross_16;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.Click += new System.EventHandler(this.OnClearClick);
            // 
            // ErrorsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layout);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ErrorsForm";
            this.layout.Panel1.ResumeLayout(false);
            this.layout.Panel2.ResumeLayout(false);
            this.layout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
            this.layout.ResumeLayout(false);
            this.listContainer.ResumeLayout(false);
            this.headerInfo.ResumeLayout(false);
            this.headerInfo.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnInfo;
        private System.Windows.Forms.SplitContainer layout;
        private System.Windows.Forms.ToolStrip headerInfo;
        private System.Windows.Forms.ToolStripLabel lblHeaderInfo;
        private System.Windows.Forms.ToolStripButton btnInfoClose;
        private Controls.LogListControl logList;
        private Controls.LogDetailsView logDetailsView;
        private System.Windows.Forms.Panel listContainer;
    }
}