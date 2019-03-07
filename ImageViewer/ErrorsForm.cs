using ImageViewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer
{
    internal partial class ErrorsForm : Form
    {
        private readonly BindingList<ComponentErrorEventArgs> _Errors;

        public ErrorsForm(BindingList<ComponentErrorEventArgs> errors)
        {
            InitializeComponent();
            logList.DataSource = _Errors = errors;
        }

        protected override void OnLoad(EventArgs e)
        {
            Icon = Icon?.Clone() as Icon;
            base.OnLoad(e);
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            _Errors.Clear();
        }

        private void OnInfoClick(object sender, EventArgs e)
        {
            layout.Panel2Collapsed = false;
        }

        private void OnInfoCloseClick(object sender, EventArgs e)
        {
            layout.Panel2Collapsed = true;
        }

        private void OnLogListSelectedIndexChanged(object sender, EventArgs e)
        {
            btnInfo.Enabled = _Errors.Count > 0;
            logDetailsView.SelectedItem = _Errors.Count > 0 ? _Errors[logList.SelectedIndex] : null;
        }
    }
}
