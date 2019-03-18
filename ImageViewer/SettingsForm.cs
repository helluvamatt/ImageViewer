using ImageViewer.Controls;
using ImageViewer.Models;
using PixelStudio.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer
{
    internal partial class SettingsForm : Form
    {
        private readonly ImageBrowser _ImageBrowser;
        private readonly BindingListEx<string> _LibraryPaths;

        public SettingsForm(ImageBrowser imageBrowser)
        {
            InitializeComponent();
            _ImageBrowser = imageBrowser;
            _LibraryPaths = new BindingListEx<string>();
            _LibraryPaths.SetItems(_ImageBrowser.LibraryPaths);

            tabPageEntryBindingSource.DataSource = new List<TabPageEntry>
            {
                new TabPageEntry(R.libraries, R.SettingsTabLibraries),
                new TabPageEntry(R.browsing, R.SettingsTabBrowsing),
                new TabPageEntry(R.viewing, R.SettingsTabViewing),
            };
            
            libraryPathsBindingSource.DataSource = _LibraryPaths;

            _LibraryPaths.ListItemRemoving += OnLibraryPathsListItemRemoving;

            _ImageBrowser.LibraryPathAdded += OnLibraryPathAdded;
            _ImageBrowser.LibraryPathRemoved += OnLibraryPathRemoved;
            _ImageBrowser.IsScanningChanged += OnImageBrowserIsScanningChanged;

            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;

            btnLibraryMaintenanceScan.Enabled = !_ImageBrowser.IsScanning;
            btnLibraryMaintenanceScan.Text = _ImageBrowser.IsScanning ? R.ScanInProgress : R.StartScan;

            txtViewingZoomLevels.Text = string.Join(",", Settings.Default.ZoomLevels.Select(i => i.ToString()));
            numViewingFullscreenTimeout.Value = Settings.Default.FullscreenAutoPlayTimeout / 1000;
        }

        protected override void OnLoad(EventArgs e)
        {
            Icon = Icon?.Clone() as Icon;
            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ImageBrowser.LibraryPathAdded -= OnLibraryPathAdded;
            _ImageBrowser.LibraryPathRemoved -= OnLibraryPathRemoved;
            _ImageBrowser.IsScanningChanged -= OnImageBrowserIsScanningChanged;
            Settings.Default.PropertyChanged -= OnSettingsPropertyChanged;
            base.OnFormClosed(e);
        }

        #region Event handlers

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.ZoomLevels) == e.PropertyName)
            {
                txtViewingZoomLevels.Text = string.Join(",", Settings.Default.ZoomLevels.Select(i => i.ToString()));
            }
            else if (nameof(Settings.FullscreenAutoPlayTimeout) == e.PropertyName)
            {
                numViewingFullscreenTimeout.Value = Settings.Default.FullscreenAutoPlayTimeout / 1000;
            }
        }

        private void OnDrawItemEx(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < tabPageEntryBindingSource.Count)
            {
                var item = (TabPageEntry)tabPageEntryBindingSource[e.Index];
                if (item.Icon != null)
                {
                    var iconBounds = new Rectangle(e.Bounds.X + (e.Bounds.Height - 32) / 2, e.Bounds.Y + (e.Bounds.Height - 32) / 2, 32, 32);
                    e.Graphics.DrawSvg(item.Icon, iconBounds, e.ForeColor);
                }
                var textBounds = e.Graphics.MeasureString(item.Text, e.Font);
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + e.Bounds.Height, e.Bounds.Y + (e.Bounds.Height - textBounds.Height) / 2);
            }
            e.DrawFocusRectangle();
        }

        private void OnTabPageChange(object sender, EventArgs e)
        {
            panelSwitcher.SelectedIndex = tabPageEntryBindingSource.Position;
        }

        #region Library tab

        private void OnLibraryPathsAddClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                var newPath = folderBrowserDialog.SelectedPath;
                if (!_ImageBrowser.AddPath(newPath))
                {
                    MessageBox.Show(this, R.WarningLibraryPathAlreadyIncluded, R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void OnLibraryPathsRemoveClick(object sender, EventArgs e)
        {
            libraryPathsBindingSource.RemoveCurrent();
        }

        private void OnLibraryPathsListItemRemoving(object sender, ListItemRemovingEventArgs<string> e)
        {
            _ImageBrowser.RemovePath(e.OldItem);
        }

        private void OnLibraryPathRemoved(object sender, PathEventArgs e)
        {
            _LibraryPaths.Remove(e.Path);
        }

        private void OnLibraryPathAdded(object sender, PathEventArgs e)
        {
            _LibraryPaths.Add(e.Path);
        }

        private void OnLibraryOpenLogsClick(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new Uri(GetType().Assembly.CodeBase).AbsolutePath)), "Logs");
            System.Diagnostics.Process.Start(path);
        }

        private void OnLibraryPathsListDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < libraryPathsBindingSource.Count && libraryPathsBindingSource[e.Index] is string path)
            {
                e.Graphics.DrawListItem(R.folder_picture_16, path, e.Font, e.ForeColor, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void OnImageBrowserIsScanningChanged(object sender, EventArgs e)
        {
            btnLibraryMaintenanceScan.Enabled = !_ImageBrowser.IsScanning;
            btnLibraryMaintenanceScan.Text = _ImageBrowser.IsScanning ? R.ScanInProgress : R.StartScan;
        }

        private void OnLibraryMaintenanceScanClick(object sender, EventArgs e)
        {
            if (!_ImageBrowser.IsScanning) _ImageBrowser.Rescan();
        }

        private void OnLibraryMaintenanceResetDeletedClick(object sender, EventArgs e)
        {
            int deleted = _ImageBrowser.RemoveDeletedImages();
            MessageBox.Show(this, string.Format(R.InfoResetDeleted, deleted), R.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnLibraryMaintenanceResetDatabaseClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, R.AreYouSureDeleteDatabase, R.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                _ImageBrowser.ResetDatabase();
            }
        }

        #endregion

        #region Browsing tab

        private void OnBrowsingImageBorderColorClick(object sender, EventArgs e)
        {
            colorDialog.Color = btnBrowsingImageBorderColor.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBrowsingImageBorderColor.ForeColor = colorDialog.Color;
            }
        }

        private void OnBrowsingImageBackColorClick(object sender, EventArgs e)
        {
            colorDialog.Color = btnBrowsingImageBackColor.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBrowsingImageBackColor.ForeColor = colorDialog.Color;
            }
        }

        #endregion

        #region Viewing tab

        private void OnViewingZoomLevelsTextChanged(object sender, EventArgs e)
        {
            Settings.Default.ZoomLevels = txtViewingZoomLevels.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(str => int.TryParse(str.Trim(), out int result) ? result : 100).Distinct().OrderBy(i => i).ToArray();
        }

        private void OnViewingFullscreenTimeoutValueChanged(object sender, EventArgs e)
        {
            Settings.Default.FullscreenAutoPlayTimeout = (int)numViewingFullscreenTimeout.Value * 1000;
        }

        private void OnViewingFullscreenBackColorClick(object sender, EventArgs e)
        {
            colorDialog.Color = btnViewingFullscreenBackColor.ForeColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                btnViewingFullscreenBackColor.ForeColor = colorDialog.Color;
            }
        }

        #endregion

        #endregion
    }
}
