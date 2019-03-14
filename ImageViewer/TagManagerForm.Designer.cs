namespace ImageViewer
{
    partial class TagManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagManagerForm));
            ImageViewer.Data.Models.TagModel tagModel1 = new ImageViewer.Data.Models.TagModel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnTagAdd = new System.Windows.Forms.ToolStripButton();
            this.btnTagDelete = new System.Windows.Forms.ToolStripButton();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTagName = new System.Windows.Forms.Label();
            this.lblTagColor = new System.Windows.Forms.Label();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.tagViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnTagColor = new ImageViewer.Controls.ColorButton();
            this.tagPreview = new ImageViewer.Controls.TagItem();
            this.tagListBox = new ImageViewer.Controls.ListBoxEx();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.mainToolStrip.SuspendLayout();
            this.layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTagAdd,
            this.btnTagDelete});
            resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // btnTagAdd
            // 
            this.btnTagAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTagAdd.Image = global::ImageViewer.Properties.Resources.tag_add_16;
            resources.ApplyResources(this.btnTagAdd, "btnTagAdd");
            this.btnTagAdd.Name = "btnTagAdd";
            this.btnTagAdd.Click += new System.EventHandler(this.OnTagAddClick);
            // 
            // btnTagDelete
            // 
            this.btnTagDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTagDelete.Image = global::ImageViewer.Properties.Resources.tag_delete_16;
            resources.ApplyResources(this.btnTagDelete, "btnTagDelete");
            this.btnTagDelete.Name = "btnTagDelete";
            this.btnTagDelete.Click += new System.EventHandler(this.OnTagDeleteClick);
            // 
            // layout
            // 
            resources.ApplyResources(this.layout, "layout");
            this.layout.Controls.Add(this.lblTagName, 0, 0);
            this.layout.Controls.Add(this.lblTagColor, 0, 1);
            this.layout.Controls.Add(this.txtTagName, 1, 0);
            this.layout.Controls.Add(this.btnTagColor, 1, 1);
            this.layout.Controls.Add(this.tagPreview, 0, 2);
            this.layout.Name = "layout";
            // 
            // lblTagName
            // 
            resources.ApplyResources(this.lblTagName, "lblTagName");
            this.lblTagName.Name = "lblTagName";
            // 
            // lblTagColor
            // 
            resources.ApplyResources(this.lblTagColor, "lblTagColor");
            this.lblTagColor.Name = "lblTagColor";
            // 
            // txtTagName
            // 
            resources.ApplyResources(this.txtTagName, "txtTagName");
            this.txtTagName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tagViewModelBindingSource, "Name", true));
            this.txtTagName.Name = "txtTagName";
            // 
            // tagViewModelBindingSource
            // 
            this.tagViewModelBindingSource.DataSource = typeof(ImageViewer.Models.TagViewModel);
            // 
            // btnTagColor
            // 
            this.btnTagColor.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.tagViewModelBindingSource, "Color", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.btnTagColor, "btnTagColor");
            this.btnTagColor.Name = "btnTagColor";
            this.btnTagColor.UseVisualStyleBackColor = true;
            this.btnTagColor.Click += new System.EventHandler(this.OnColorClick);
            // 
            // tagPreview
            // 
            resources.ApplyResources(this.tagPreview, "tagPreview");
            this.tagPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.layout.SetColumnSpan(this.tagPreview, 2);
            this.tagPreview.DataBindings.Add(new System.Windows.Forms.Binding("TagModel", this.tagViewModelBindingSource, "Model", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.tagPreview.ForeColor = System.Drawing.Color.Black;
            this.tagPreview.Name = "tagPreview";
            this.tagPreview.ShowRemoveButton = false;
            tagModel1.Color = -986896;
            tagModel1.CreatedDate = new System.DateTime(((long)(0)));
            tagModel1.ID = ((long)(0));
            tagModel1.LastUsedDate = new System.DateTime(((long)(0)));
            tagModel1.ModifiedDate = new System.DateTime(((long)(0)));
            tagModel1.Name = "";
            this.tagPreview.TagModel = tagModel1;
            // 
            // tagListBox
            // 
            resources.ApplyResources(this.tagListBox, "tagListBox");
            this.tagListBox.DataSource = this.tagViewModelBindingSource;
            this.tagListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tagListBox.FormattingEnabled = true;
            this.tagListBox.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tagListBox.Name = "tagListBox";
            this.tagListBox.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnTagDrawItem);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // TagManagerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagListBox);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.mainToolStrip);
            this.Name = "TagManagerForm";
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton btnTagAdd;
        private System.Windows.Forms.ToolStripButton btnTagDelete;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.Label lblTagColor;
        private System.Windows.Forms.TextBox txtTagName;
        private Controls.ColorButton btnTagColor;
        private Controls.ListBoxEx tagListBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.BindingSource tagViewModelBindingSource;
        private Controls.TagItem tagPreview;
    }
}