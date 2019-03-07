using System.Drawing;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    internal class LabelEx : Label
    {
        public LabelEx()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (proposedSize.IsEmpty) proposedSize = Size;
            if (proposedSize.Width > 0)
            {
                using (var g = CreateGraphics())
                {
                    return Size.Ceiling(g.MeasureString(Text, Font, proposedSize.Width, new StringFormat()));
                }
            }
            return base.GetPreferredSize(proposedSize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, brush, ClientRectangle, new StringFormat());
            }
        }
    }
}
