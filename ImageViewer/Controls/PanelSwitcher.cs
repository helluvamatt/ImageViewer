using System;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    internal class PanelSwitcher : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }

    internal class TabPageEntry
    {
        public TabPageEntry(byte[] icon, string text)
        {
            Icon = icon;
            Text = text;
        }

        public byte[] Icon { get; }
        public string Text { get; }
    }
}
