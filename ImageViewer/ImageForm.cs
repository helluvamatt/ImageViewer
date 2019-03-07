using ImageViewer.Models;
using ImageViewer.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings = ImageViewer.Properties.Settings;
using R = ImageViewer.Properties.Resources;
using ImageViewer.Controls;

namespace ImageViewer
{
    internal partial class ImageForm : Form, IToolStripForm
    {
        private readonly ImageBrowser _ImageBrowser;

        public ImageForm(ImageBrowser imageBrowser, ImageModel imageModel)
        {
            _ImageBrowser = imageBrowser ?? throw new ArgumentNullException(nameof(imageBrowser));
            ImageModel = imageModel ?? throw new ArgumentNullException(nameof(imageModel));
            InitializeComponent();
            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
            imageView.ZoomLevels = new Cyotek.Windows.Forms.ZoomLevelCollection(Settings.Default.ZoomLevels.OrderBy(z => z));
            Text = ImageModel.Name;

            _ImageBrowser.ImageChanged += OnImageBrowserImageChanged;
        }

        public ImageModel ImageModel { get; }

        public double Zoom => imageView.ZoomFactor;

        public event EventHandler ZoomChanged;

        public void ShowInformation()
        {
            layout.Panel2Collapsed = false;
            imageView.ZoomToFit();
        }

        public ToolStrip ToolStrip => toolStrip;

        protected override void OnLoad(EventArgs e)
        {
            Icon = Icon?.Clone() as Icon;
            base.OnLoad(e);
            imageView.UseWaitCursor = true;
            Task.Run(() =>
            {
                var result = _ImageBrowser.LoadImage(ImageModel, false);
                if (string.IsNullOrEmpty(result.Error))
                {
                    Invoke(new Action(() =>
                    {
                        imageView.Image = result.Image;
                        imageView.UseWaitCursor = false;
                        imageView.ZoomToFit();

                        infoMetadataTable.Metadata = result.Metadata;
                    }));
                }
                else
                {
                    imageView.Image = R.error_32;
                    imageView.UseWaitCursor = false;
                    imageView.ZoomToFit();
                }
            });

            UpdateTags();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                    OnZoomInClick(this, EventArgs.Empty);
                    e.Handled = true;
                    break;
                case Keys.OemMinus:
                    OnZoomOutClick(this, EventArgs.Empty);
                    e.Handled = true;
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    OnActualSizeClick(this, EventArgs.Empty);
                    e.Handled = true;
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            imageView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            imageView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        }

        private void OnImageBrowserImageChanged(object sender, ImageEventArgs e)
        {
            if (e.Image == ImageModel)
            {
                UpdateTags();
                UpdateMetadata();
            }
        }

        private void OnTagItemRemoved(object sender, EventArgs e)
        {
            if (sender is TagItem tagItem)
            {
                _ImageBrowser.RemoveImageTag(ImageModel, tagItem.TagModel);
            }
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.ZoomLevels) == e.PropertyName)
            {
                imageView.ZoomLevels = new Cyotek.Windows.Forms.ZoomLevelCollection(Settings.Default.ZoomLevels.OrderBy(z => z));
            }
        }

        private void OnImageViewZoomed(object sender, Cyotek.Windows.Forms.ImageBoxZoomEventArgs e)
        {
            ZoomChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnInformationCloseClick(object sender, EventArgs e)
        {
            layout.Panel2Collapsed = true;
            imageView.ZoomToFit();
        }

        private void OnInfoDetailsToggleClick(object sender, EventArgs e)
        {
            layoutInfoDetails.Visible = !layoutInfoDetails.Visible;
            btnInfoDetailsToggle.Image = layoutInfoDetails.Visible ? R.toggle_collapse_16 : R.toggle_expand_16;
            btnInfoDetailsToggle.Text = layoutInfoDetails.Visible ? R.Collapse : R.Expand;
        }

        private void OnInfoTagsCollapseClick(object sender, EventArgs e)
        {
            layoutInfoTags.Visible = !layoutInfoTags.Visible;
            btnInfoTagsToggle.Image = layoutInfoTags.Visible ? R.toggle_collapse_16 : R.toggle_expand_16;
            btnInfoTagsToggle.Text = layoutInfoTags.Visible ? R.Collapse : R.Expand;
        }

        private void OnInfoTagAddClick(object sender, EventArgs e)
        {
            var dlg = new ImageTagForm(_ImageBrowser);
            if (dlg.ShowDialog() == DialogResult.OK && dlg.SelectedItem != null)
            {
                _ImageBrowser.AddImageTag(ImageModel, dlg.SelectedItem);
            }
        }

        private void OnInfoMetadataToggleClick(object sender, EventArgs e)
        {
            infoMetadataTable.Visible = !infoMetadataTable.Visible;
            btnInfoMetadataToggle.Image = infoMetadataTable.Visible ? R.toggle_collapse_16 : R.toggle_expand_16;
            btnInfoMetadataToggle.Text = infoMetadataTable.Visible ? R.Collapse : R.Expand;
        }

        private void OnZoomToFitClick(object sender, EventArgs e)
        {
            imageView.ZoomToFit();
        }

        private void OnActualSizeClick(object sender, EventArgs e)
        {
            imageView.ActualSize();
        }

        private void OnZoomInClick(object sender, EventArgs e)
        {
            imageView.ZoomIn(true);
        }

        private void OnZoomOutClick(object sender, EventArgs e)
        {
            imageView.ZoomOut(true);
        }

        private void OnInfoShowClick(object sender, EventArgs e)
        {
            ShowInformation();
        }

        private void UpdateTags()
        {
            _ImageBrowser.GetImageTags(ImageModel);
            layoutInfoTags.Controls.Clear();
            foreach (var tag in ImageModel.Tags)
            {
                var tagItem = new TagItem()
                {
                    TagModel = tag,
                    Padding = new Padding(5),
                };
                tagItem.Removed += OnTagItemRemoved;
                layoutInfoTags.Controls.Add(tagItem);
            }
            layoutInfoTags.Controls.Add(btnInfoTagAdd);
        }

        private void UpdateMetadata()
        {

        }

    }
}
