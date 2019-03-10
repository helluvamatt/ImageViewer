using System.Drawing;
using Cyotek.Windows.Forms;

namespace ImageViewer.Controls
{
    internal class ImageBoxEx : ImageBox
    {
        public bool HasHorizontalScroll => HScroll;
        public bool HasVerticalScroll => VScroll;
    }
}
