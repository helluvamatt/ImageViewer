using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            lblVersion.Text = string.Format(R.VersionText, GetType().Assembly.GetName().Version.ToString());
            lblCopyright.Text = string.Format(R.CopyrightText, DateTime.Now);
            lblLicense.Text = R.LicenseText;
            lblCredits.Text = R.CreditsText;
        }

        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData is string url)
            {
                System.Diagnostics.Process.Start(url);
            }
        }
    }
}
