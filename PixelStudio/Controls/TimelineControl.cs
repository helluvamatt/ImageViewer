using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelStudio.Controls
{
    internal class TimelineControl : Control
    {
        private readonly HScrollBar _HScrollBar;

        public TimelineControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            _HScrollBar = new HScrollBar();
            _HScrollBar.Dock = DockStyle.Bottom;
            _HScrollBar.Scroll += OnScroll;
            Controls.Add(_HScrollBar);
        }

        public void ZoomIn()
        {

        }

        public void ZoomOut()
        {

        }

        public void ZoomToFit()
        {

        }

        private Size ViewportSize
        {
            get
            {
                var sz = ClientSize;
                if (_HScrollBar.Visible) sz.Height -= _HScrollBar.Height;
                return sz;
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {


            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
