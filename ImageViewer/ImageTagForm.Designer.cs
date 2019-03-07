namespace ImageViewer
{
    partial class ImageTagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageTagForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTagSearch = new System.Windows.Forms.TextBox();
            this.listBoxTags = new ImageViewer.Controls.ListBoxEx();
            this.tagSelectModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tagSelectModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtTagSearch
            // 
            resources.ApplyResources(this.txtTagSearch, "txtTagSearch");
            this.txtTagSearch.Name = "txtTagSearch";
            this.txtTagSearch.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            // 
            // listBoxTags
            // 
            resources.ApplyResources(this.listBoxTags, "listBoxTags");
            this.listBoxTags.DataSource = this.tagSelectModelBindingSource;
            this.listBoxTags.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.DrawItemEx += new System.EventHandler<System.Windows.Forms.DrawItemEventArgs>(this.OnTagsDrawItem);
            // 
            // tagSelectModelBindingSource
            // 
            this.tagSelectModelBindingSource.DataSource = typeof(ImageViewer.Models.TagSelectModel);
            this.tagSelectModelBindingSource.CurrentChanged += new System.EventHandler(this.OnDataSourceCurrentChanged);
            // 
            // ImageTagForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxTags);
            this.Controls.Add(this.txtTagSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageTagForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.tagSelectModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTagSearch;
        private Controls.ListBoxEx listBoxTags;
        private System.Windows.Forms.BindingSource tagSelectModelBindingSource;
    }
}