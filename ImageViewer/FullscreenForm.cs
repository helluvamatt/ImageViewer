using ImageViewer.Controls;
using ImageViewer.Data.Models;
using ImageViewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer
{
    internal partial class FullscreenForm : Form
    {
        private readonly ImageBrowser _ImageBrowser;

        private int _CurrentIndex;
        private BrowseHistoryPage _HistoryPage;
        private List<ImageModel> _Images;
        
        private Image _CurrentImage;
        private Rectangle _CurrentImageBounds;

        public FullscreenForm(ImageBrowser imageBrowser)
        {
            InitializeComponent();
            _ImageBrowser = imageBrowser;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BrowseHistoryPage HistoryPage
        {
            get => _HistoryPage;
            set
            {
                if (_HistoryPage != value)
                {
                    _HistoryPage = value;
                    _Images = _HistoryPage?.GetImages(_ImageBrowser).ToList() ?? new List<ImageModel>();
                    timer.Enabled = true;
                    CurrentIndex = 0;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentIndex
        {
            get => _CurrentIndex;
            set
            {
                if (_CurrentIndex != value)
                {
                    _CurrentIndex = value;
                    if (_CurrentImage != null)
                    {
                        _CurrentImage.Dispose();
                        _CurrentImage = null;
                    }
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FrameDuration
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.F11:
                    Close();
                    e.Handled = true;
                    break;
                case Keys.Left:
                    timer.Enabled = false;
                    if (_Images != null && _Images.Count > 0)
                    {
                        if (CurrentIndex > 0) CurrentIndex--;
                        else CurrentIndex = _Images.Count - 1;
                    }
                    e.Handled = true;
                    break;
                case Keys.Right:
                    timer.Enabled = false;
                    if (_Images != null && _Images.Count > 0)
                    {
                        if (CurrentIndex < _Images.Count - 1) CurrentIndex++;
                        else CurrentIndex = 0;
                    }
                    e.Handled = true;
                    break;
                case Keys.Space:
                    timer.Enabled = !timer.Enabled;
                    Invalidate();
                    e.Handled = true;
                    break;
            }

            base.OnKeyUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_CurrentImage == null)
            {
                var img = _Images[CurrentIndex];
                var result = _ImageBrowser.LoadImage(img, false);
                if (string.IsNullOrEmpty(result.Error))
                {
                    if (result.IsVector)
                    {
                        _CurrentImage = DrawingUtils.RenderSvg(result.SvgDocument, ClientSize);
                    }
                    else
                    {
                        _CurrentImage = result.Image;
                    }
                }
                else
                {
                    string msg = string.Format(R.ErrorFailedToLoadImage, img.Name, result.Error);
                    var size = Size.Ceiling(e.Graphics.MeasureString(msg, Font));
                    int iconWidth = R.delete_16.Width + 3; // + padding
                    size.Width += iconWidth;
                    var errorRect = ComputeBounds(size, ClientSize);
                    e.Graphics.DrawImage(R.delete_16, errorRect.Location);
                    var errorTextRect = new Rectangle(errorRect.X + iconWidth, errorRect.Y, errorRect.Width - iconWidth, errorRect.Height);
                    e.Graphics.DrawString(msg, Font, new SolidBrush(ForeColor), errorTextRect);
                    _CurrentImage = null;
                }
                _CurrentImageBounds = _CurrentImage != null ? ComputeBounds(_CurrentImage.Size, ClientSize) : Rectangle.Empty;
            }

            if (_CurrentImage != null)
            {
                e.Graphics.DrawImageUnscaled(_CurrentImage, _CurrentImageBounds);
            }

            var statusIcon = timer.Enabled ? R.control_play_blue_16 : R.control_pause_blue_16;
            var iconLocation = new Point(ClientRectangle.Right - (statusIcon.Width + 3), ClientRectangle.Bottom - (statusIcon.Height + 3));
            e.Graphics.DrawImage(statusIcon, iconLocation);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_Images != null && _Images.Count > 0)
            {
                if (CurrentIndex < _Images.Count - 1) CurrentIndex++;
                else CurrentIndex = 0;
            }
        }

        private Rectangle ComputeBounds(Size imageSize, Size canvasSize)
        {
            if (canvasSize.Width < 1 || canvasSize.Height < 1) return Rectangle.Empty;
            float ratio = Math.Max((float)imageSize.Width / canvasSize.Width, (float)imageSize.Height / canvasSize.Height);
            int width, height;
            if (ratio < 1)
            {
                width = imageSize.Width;
                height = imageSize.Height;
            }
            else
            {
                width = (int)(imageSize.Width / ratio);
                height = (int)(imageSize.Height / ratio);
            }
            return new Rectangle((canvasSize.Width - width) / 2, (canvasSize.Height - height) / 2, width, height);
        }
    }
}
