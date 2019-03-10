namespace ImageViewer
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblLicense = new ImageViewer.Controls.LinkLabelEx();
            this.lblCredits = new ImageViewer.Controls.LinkLabelEx();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            resources.ApplyResources(this.layout, "layout");
            this.layout.Controls.Add(this.lblAppName, 1, 0);
            this.layout.Controls.Add(this.lblVersion, 1, 1);
            this.layout.Controls.Add(this.lblIcon, 0, 0);
            this.layout.Controls.Add(this.lblCopyright, 1, 2);
            this.layout.Controls.Add(this.btnOK, 0, 5);
            this.layout.Controls.Add(this.lblLicense, 1, 3);
            this.layout.Controls.Add(this.lblCredits, 1, 4);
            this.layout.Name = "layout";
            // 
            // lblAppName
            // 
            resources.ApplyResources(this.lblAppName, "lblAppName");
            this.lblAppName.Name = "lblAppName";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = global::ImageViewer.Properties.Resources.pictures_32;
            resources.ApplyResources(this.lblIcon, "lblIcon");
            this.lblIcon.Name = "lblIcon";
            // 
            // lblCopyright
            // 
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.Name = "lblCopyright";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.layout.SetColumnSpan(this.btnOK, 2);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblLicense
            // 
            resources.ApplyResources(this.lblLicense, "lblLicense");
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.UseCompatibleTextRendering = true;
            this.lblLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // lblCredits
            // 
            resources.ApplyResources(this.lblCredits, "lblCredits");
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AboutForm";
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Button btnOK;
        private Controls.LinkLabelEx lblLicense;
        private Controls.LinkLabelEx lblCredits;
    }
}