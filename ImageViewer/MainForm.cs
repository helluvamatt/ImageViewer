using ImageViewer.Models;
using ImageViewer.Data.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer
{
    public partial class MainForm : Form
    {
        private readonly ImageBrowser _ImageBrowser;
        private readonly BindingList<ImageForm> _ImageForms;
        private readonly BindingList<ComponentErrorEventArgs> _Errors;

        private LibraryBrowserForm _LibraryBrowserForm;
        private TagManagerForm _TagManagerForm;
        private SettingsForm _SettingsForm;
        private ErrorsForm _ErrorsForm;

        public MainForm(string[] args)
        {
            InitializeComponent();

            _ImageBrowser = new ImageBrowser(Settings.Default.AppPath);
            _ImageBrowser.IsScanningChanged += OnIsScanningChanged;
            _ImageBrowser.Error += OnImageBrowserError;
            _ImageBrowser.DatabaseReset += OnImageBrowserDatabaseReset;

            _ImageForms = new BindingList<ImageForm>();
            _ImageForms.ListChanged += OnImageFormsListChanged;

            _Errors = new BindingList<ComponentErrorEventArgs>();
        }

        #region Form overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OnShowLibraryBrowserClick(this, EventArgs.Empty);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    e.Handled = true;
                    break;
                case Keys.F11:
                    if (_LibraryBrowserForm != null)
                    {
                        _LibraryBrowserForm.StartSlideshow();
                        e.Handled = true;
                    }
                    break;
            }

            base.OnKeyUp(e);
        }

        protected override void OnMdiChildActivate(EventArgs e)
        {
            ToolStripManager.RevertMerge(toolStrip);
            if (ActiveMdiChild is ImageForm imageForm)
            {
                SetStatusBarStats(imageForm.ImageModel);
                lblZoom.Text = string.Format(R.LabelZoomText, imageForm.Zoom);
            }
            else if (ActiveMdiChild is LibraryBrowserForm libraryBrowser)
            {
                SetStatusBarStats(libraryBrowser.SelectedImage);
                lblZoom.Text = string.Format(R.LabelZoomText, 1);
            }
            else
            {
                SetStatusBarStats(null);
                lblZoom.Text = R.LabelZoomTextNone;
            }
            if (ActiveMdiChild is IToolStripForm toolStripForm)
            {
                ToolStripManager.Merge(toolStripForm.ToolStrip, toolStrip);
            }

            base.OnMdiChildActivate(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ImageBrowser.Close();
            base.OnFormClosed(e);
        }

        #endregion

        #region Global/Form event handlers

        private void OnIsScanningChanged(object sender, EventArgs e)
        {
            lblStatus.Text = _ImageBrowser.IsScanning ? string.IsNullOrEmpty(_ImageBrowser.CurrentScanPath) ? R.ScanInProgress : string.Format(R.ScanInProgressWithDirectory, _ImageBrowser.CurrentScanPath) : R.Ready;
        }

        private void OnImageBrowserError(object sender, ComponentErrorEventArgs e)
        {
            _Errors.Add(e);
            OnShowErrorLogClick(this, EventArgs.Empty);
        }

        private void OnImageBrowserDatabaseReset(object sender, EventArgs e)
        {
            // Close all image forms
            foreach (var imageForm in _ImageForms) imageForm.Close();
            _ImageForms.Clear();
        }

        private void OnImageFormsListChanged(object sender, ListChangedEventArgs e)
        {
            menuItemWindowImages.DropDownItems.Clear();
            foreach (var item in _ImageForms)
            {
                var newMenuItem = new ToolStripMenuItem(item.Text)
                {
                    Tag = item,
                    Image = R.picture_16,
                };
                newMenuItem.Click += OnWindowImageItemClick;
                menuItemWindowImages.DropDownItems.Add(newMenuItem);
            }
            menuItemWindowImages.Visible = toolStripSeparatorWindowImages.Visible = menuItemWindowImages.DropDownItems.Count > 0;
        }

        private void OnWindowImageItemClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is ImageForm form)
            {
                ActivateMdiChild(form);
                form.BringToFront();
            }
        }

        private void OnShowLibraryBrowserClick(object sender, EventArgs e)
        {
            if (_LibraryBrowserForm == null)
            {
                _LibraryBrowserForm = new LibraryBrowserForm(_ImageBrowser);
                _LibraryBrowserForm.FormClosed += OnLibraryBrowserFormClosed;
                _LibraryBrowserForm.OpenImage += OnOpenImage;
                _LibraryBrowserForm.OpenImageInformation += OnOpenImageInformation;
                _LibraryBrowserForm.ManageTags += OnShowTagManagerClick;
                _LibraryBrowserForm.SelectedImageChanged += OnSelectedImageChanged;
                _LibraryBrowserForm.MdiParent = this;
            }
            _LibraryBrowserForm.WindowState = FormWindowState.Maximized;
            _LibraryBrowserForm.Show();
            ActivateMdiChild(_LibraryBrowserForm);
        }

        private void OnShowTagManagerClick(object sender, EventArgs e)
        {
            if (_TagManagerForm == null)
            {
                _TagManagerForm = new TagManagerForm(_ImageBrowser);
                _TagManagerForm.FormClosed += OnTagManagerFormClosed;
                _TagManagerForm.MdiParent = this;
            }
            _TagManagerForm.WindowState = FormWindowState.Maximized;
            _TagManagerForm.Show();
            ActivateMdiChild(_TagManagerForm);
        }

        private void OnShowSettingsClick(object sender, EventArgs e)
        {
            if (_SettingsForm == null)
            {
                _SettingsForm = new SettingsForm(_ImageBrowser);
                _SettingsForm.FormClosed += OnSettingsFormClosed;
                _SettingsForm.MdiParent = this;
            }
            _SettingsForm.WindowState = FormWindowState.Maximized;
            _SettingsForm.Show();
            ActivateMdiChild(_SettingsForm);
        }

        private void OnShowErrorLogClick(object sender, EventArgs e)
        {
            if (_ErrorsForm == null)
            {
                _ErrorsForm = new ErrorsForm(_Errors);
                _ErrorsForm.FormClosed += OnErrorsFormClosed;
                _ErrorsForm.MdiParent = this;
            }

            if (ActiveMdiChild != null) ActiveMdiChild.WindowState = FormWindowState.Normal;

            _ErrorsForm.Dock = DockStyle.Bottom;
            _ErrorsForm.Height = ClientSize.Height / 2;
            _ErrorsForm.Show();
            ActivateMdiChild(_ErrorsForm);
        }

        private void OnViewLogsClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Path.GetFullPath("Logs"));
        }

        private void OnMenuItemAboutClick(object sender, EventArgs e)
        {
            var dlg = new AboutForm();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(this);
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Library Browser event handlers

        private void OnOpenImage(object sender, ImageEventArgs e)
        {
            OpenImage(e.Image);
        }

        private void OnOpenImageInformation(object sender, ImageEventArgs e)
        {
            OpenImageInformation(e.Image);
        }

        private void OnLibraryBrowserFormClosed(object sender, FormClosedEventArgs e)
        {
            _LibraryBrowserForm = null;
        }

        private void OnSelectedImageChanged(object sender, EventArgs e)
        {
            if (sender == ActiveMdiChild && sender is LibraryBrowserForm libraryBrowser)
            {
                SetStatusBarStats(libraryBrowser.SelectedImage);
            }
        }

        #endregion

        #region Tag Manager event handlers

        private void OnTagManagerFormClosed(object sender, FormClosedEventArgs e)
        {
            _TagManagerForm = null;
        }

        #endregion

        #region Image Form event handlers

        private void OnImageZoomed(object sender, EventArgs e)
        {
            if (sender == ActiveMdiChild && ActiveMdiChild is ImageForm imageForm)
            {
                lblZoom.Text = string.Format(R.LabelZoomText, imageForm.Zoom);
            }
        }

        #endregion

        #region Settings Form event handlers

        private void OnSettingsFormClosed(object sender, FormClosedEventArgs e)
        {
            _SettingsForm = null;
        }

        #endregion

        #region Errors Form event handlers

        private void OnErrorsFormClosed(object sender, FormClosedEventArgs e)
        {
            _ErrorsForm = null;
        }

        #endregion

        private ImageForm OpenImage(ImageModel imageModel)
        {
            var imageForm = new ImageForm(_ImageBrowser, imageModel);
            imageForm.FormClosed += (sender, e) => _ImageForms.Remove(imageForm);
            imageForm.ZoomChanged += OnImageZoomed;
            imageForm.MdiParent = this;
            imageForm.WindowState = FormWindowState.Maximized;
            _ImageForms.Add(imageForm);
            imageForm.Show();
            ActivateMdiChild(imageForm);
            return imageForm;
        }

        private void OpenImageInformation(ImageModel imageModel)
        {
            var existingForm = _ImageForms.FirstOrDefault(f => f.ImageModel == imageModel);
            if (existingForm != null) existingForm.ShowInformation();
            else OpenImage(imageModel).ShowInformation();
        }

        private void SetStatusBarStats(ImageModel imageModel)
        {
            if (imageModel != null)
            {
                lblFileSize.Text = string.Format(R.LabelFileSizeText, FileUtils.GetFileSizeString(imageModel.FileSize), imageModel.FileSize);
                lblImageSize.Text = string.Format(R.LabelImageSizeText, imageModel.Width, imageModel.Height);
            }
            else
            {
                lblFileSize.Text = string.Format(R.LabelFileSizeText, FileUtils.GetFileSizeString(0), 0);
                lblImageSize.Text = string.Format(R.LabelImageSizeText, 0, 0);
            }
        }
    }

    internal interface IToolStripForm
    {
        ToolStrip ToolStrip { get; }
    }
}
