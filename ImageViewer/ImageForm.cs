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
            Text = ImageModel.Title;

            menuItemBackgroundBlack.Image = Color.Black.RenderColorSquare(new Size(16, 16));
            menuItemBackgroundWhite.Image = Color.White.RenderColorSquare(new Size(16, 16));
            menuItemBackgroundCustom.Image = Color.White.RenderColorSquare(new Size(16, 16));

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

        #region Form overrides

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

                        layoutMetadata.Nodes.Clear();
                        if (result.Metadata != null && result.Metadata.Any())
                        {
                            var boldFont = new Font(layoutMetadata.Font, FontStyle.Bold);
                            foreach (var metadataDirectory in result.Metadata)
                            {
                                if (!metadataDirectory.IsEmpty && metadataDirectory.Tags.Any())
                                {
                                    var directoryNode = new TreeNode
                                    {
                                        NodeFont = boldFont,
                                        Text = metadataDirectory.Name,
                                    };
                                    foreach (var metadataTag in metadataDirectory.Tags)
                                    {
                                        var tagNode = new TreeNode
                                        {
                                            NodeFont = boldFont,
                                            Text = metadataTag.Name,
                                        };
                                        var tagValueNode = new TreeNode
                                        {
                                            Text = metadataTag.Description,
                                        };
                                        tagNode.Nodes.Add(tagValueNode);
                                        directoryNode.Nodes.Add(tagNode);
                                    }
                                    layoutMetadata.Nodes.Add(directoryNode);
                                }

                            }
                        }
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        imageView.Image = R.error_32;
                        imageView.UseWaitCursor = false;
                        imageView.ZoomToFit();
                    }));
                }
            });

            UpdateDetails();
            UpdateTags();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Settings.Default.PropertyChanged -= OnSettingsPropertyChanged;
            _ImageBrowser.ImageChanged -= OnImageBrowserImageChanged;
            base.OnFormClosed(e);
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
                UpdateDetails();
                UpdateTags();
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
            layoutMetadata.Visible = !layoutMetadata.Visible;
            btnInfoMetadataToggle.Image = layoutMetadata.Visible ? R.toggle_collapse_16 : R.toggle_expand_16;
            btnInfoMetadataToggle.Text = layoutMetadata.Visible ? R.Collapse : R.Expand;
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

        private void OnInfoDetailsTitleSaveClick(object sender, EventArgs e)
        {
            ImageModel.Title = txtInfoDetailsTitle.Text;
            _ImageBrowser.SaveImage(ImageModel);
            btnInfoDetailsTitleSave.Enabled = false;
        }

        private void OnInfoDetailsTitleTextChanged(object sender, EventArgs e)
        {
            btnInfoDetailsTitleSave.Enabled = txtInfoDetailsTitle.Text != ImageModel.Title;
        }

        private void OnBackgroundNoneChecked(object sender, EventArgs e)
        {
            if (menuItemBackgroundNone.Checked)
            {
                menuItemBackgroundBlack.Checked = false;
                menuItemBackgroundWhite.Checked = false;
                menuItemBackgroundCustom.Checked = false;
                imageView.GridColor = Color.White;
                imageView.GridColorAlternate = Color.Gainsboro;
                btnDropDownBackColor.Image = menuItemBackgroundNone.Image;
            }
        }

        private void OnBackgroundBlackChecked(object sender, EventArgs e)
        {
            if (menuItemBackgroundBlack.Checked)
            {
                menuItemBackgroundNone.Checked = false;
                menuItemBackgroundWhite.Checked = false;
                menuItemBackgroundCustom.Checked = false;
                imageView.GridColor = imageView.GridColorAlternate = Color.Black;
                btnDropDownBackColor.Image = menuItemBackgroundBlack.Image;
            }
        }
        
        private void OnBackgroundWhiteChecked(object sender, EventArgs e)
        {
            if (menuItemBackgroundWhite.Checked)
            {
                menuItemBackgroundNone.Checked = false;
                menuItemBackgroundBlack.Checked = false;
                menuItemBackgroundCustom.Checked = false;
                imageView.GridColor = imageView.GridColorAlternate = Color.White;
                btnDropDownBackColor.Image = menuItemBackgroundWhite.Image;
            }
        }

        private void OnBackgroundCustomChecked(object sender, EventArgs e)
        {
            if (menuItemBackgroundCustom.Checked)
            {
                menuItemBackgroundNone.Checked = false;
                menuItemBackgroundBlack.Checked = false;
                menuItemBackgroundWhite.Checked = false;
                colorDialog.Color = imageView.GridColor;
                if (colorDialog.ShowDialog(this) == DialogResult.OK)
                {
                    imageView.GridColor = imageView.GridColorAlternate = colorDialog.Color;
                    btnDropDownBackColor.Image = menuItemBackgroundCustom.Image = colorDialog.Color.RenderColorSquare(new Size(16, 16));
                }
                else
                {
                    menuItemBackgroundNone.Checked = true;
                }
            }
        }

        #endregion

        private void UpdateDetails()
        {
            if (ImageModel != null)
            {
                txtInfoDetailsTitle.Text = ImageModel.Title;
                btnInfoDetailsTitleSave.Enabled = false;
                lblInfoDetailsSizeValue.Text = string.Format(R.LabelImageSizeText, ImageModel.Width, ImageModel.Height);
                lblInfoDetailsFileSizeValue.Text = string.Format(R.LabelFileSizeText, FileUtils.GetFileSizeString(ImageModel.FileSize), ImageModel.FileSize);
                lblInfoDetailsFormatValue.Text = ImageModel.Format;
                lblInfoDetailsBitsPerPixelValue.Text = ImageModel.BitsPerPixel.ToString();
                lblInfoDetailsModifiedDateValue.Text = ImageModel.FileModifiedDate.ToString();
            }
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
    }
}
