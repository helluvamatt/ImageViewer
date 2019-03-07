using ImageViewer.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer.Controls
{
    internal class LogDetailsView : ScrollableControl
    {
        private Padding _CellPadding;
        private int _CellSpacing;
        private ComponentErrorEventArgs _SelectedItem;

        public LogDetailsView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        [DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public Padding CellPadding
        {
            get => _CellPadding;
            set
            {
                if (_CellPadding != value)
                {
                    _CellPadding = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        public int CellSpacing
        {
            get => _CellSpacing;
            set
            {
                if (_CellSpacing != value)
                {
                    _CellSpacing = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        public ComponentErrorEventArgs SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (SelectedItem != null)
            {
                using (var g = CreateGraphics())
                {
                    var lineHeight = Size.Ceiling(g.MeasureString("M", Font)).Height;
                    var messageSize = Size.Ceiling(g.MeasureString(SelectedItem.Message, new Font(Font, FontStyle.Bold), ClientSize.Width - CellPadding.Horizontal));
                    var stackTraceSize = Size.Ceiling(g.MeasureString(SelectedItem.StackTrace, Font));
                    var totalHeight = (lineHeight + CellPadding.Vertical) * 2 + CellSpacing * 3 + messageSize.Height + CellPadding.Vertical * 2 + stackTraceSize.Height;
                    AutoScrollMinSize = new Size(Math.Max(ClientSize.Width, stackTraceSize.Width + CellPadding.Horizontal), totalHeight);
                }
            }
            else AutoScrollMinSize = Size.Empty;

            base.OnLayout(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (SelectedItem != null)
            {
                e.Graphics.TranslateTransform(0, AutoScrollPosition.Y);
                var textBrush = new SolidBrush(ForeColor);
                var boldFont = new Font(Font, FontStyle.Bold);

                var lineSize = Size.Ceiling(e.Graphics.MeasureString(R.Timestamp, Font));
                var labelColWidth = Math.Max(lineSize.Width, Size.Ceiling(e.Graphics.MeasureString(R.Component, boldFont)).Width);
                var messageSize = Size.Ceiling(e.Graphics.MeasureString(SelectedItem.Message, boldFont, ClientSize.Width - CellPadding.Horizontal));
                var stackTraceSize = Size.Ceiling(e.Graphics.MeasureString(SelectedItem.StackTrace, Font));

                int y = CellPadding.Top;
                int x = CellPadding.Left;

                // Draw timestamp
                e.Graphics.DrawString(R.Timestamp, boldFont, textBrush, x, y);
                e.Graphics.DrawString(SelectedItem.Timestamp.ToString(), Font, textBrush, x + labelColWidth + CellSpacing, y);
                y += CellPadding.Vertical + lineSize.Height + CellSpacing;

                // Draw component
                e.Graphics.DrawString(R.Component, boldFont, textBrush, x, y);
                e.Graphics.DrawString(SelectedItem.Component, Font, textBrush, x + labelColWidth + CellSpacing, y);
                y += CellPadding.Vertical + lineSize.Height + CellSpacing;

                // Draw message
                var messageRect = new Rectangle(new Point(CellPadding.Left, y), messageSize);
                e.Graphics.DrawString(SelectedItem.Message, boldFont, textBrush, messageRect, new StringFormat());
                y += CellPadding.Vertical + messageSize.Height + CellSpacing;

                // Draw stack trace
                e.Graphics.TranslateTransform(AutoScrollPosition.X, 0);
                e.Graphics.DrawString(SelectedItem.StackTrace, Font, textBrush, CellPadding.Left, y);
                e.Graphics.ResetTransform();
            }
            else
            {
                e.Graphics.DrawStringVerticallyCentered(R.NoItemSelected, Font, ForeColor, ClientRectangle);
            }
        }

    }
}
