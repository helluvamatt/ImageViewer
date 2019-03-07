using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetadataExtractor;

namespace ImageViewer.Controls
{
    internal class MetadataTable : Label
    {
        private IReadOnlyList<Directory> _Metadata;

        #region Metadata property

        public IReadOnlyList<Directory> Metadata
        {
            get => _Metadata;
            set
            {
                if (_Metadata != value)
                {
                    _Metadata = value;
                    OnMetadataChanged();
                }
            }
        }

        public event EventHandler MetadataChanged;

        private void OnMetadataChanged()
        {
            var size = GetPreferredSize(Size);
            Height = size.Height;
            MetadataChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IndentionSize changed

        private int _IndentionSize;
        public int IndentionSize
        {
            get => _IndentionSize;
            set
            {
                if (_IndentionSize != value)
                {
                    _IndentionSize = value;
                    
                }
            }
        }

        public event EventHandler IndentionSizeChanged;

        private void OnIndentionSizeChanged()
        {
            OnTextChanged(EventArgs.Empty);
            IndentionSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (Metadata != null)
            {
                using (var g = CreateGraphics()) // For measuring text
                {
                    var boldFont = new Font(Font, FontStyle.Bold);
                    proposedSize.Height = 0;
                    foreach (var directory in Metadata)
                    {
                        if (!directory.IsEmpty)
                        {
                            proposedSize.Height += Size.Ceiling(g.MeasureString(directory.Name, boldFont)).Height;
                            foreach (var tag in directory.Tags)
                            {
                                proposedSize.Height += Size.Ceiling(g.MeasureString(tag.Name, boldFont)).Height;
                            }
                        }
                    }
                    return proposedSize;
                }
            }
            return base.GetPreferredSize(proposedSize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Metadata != null)
            {
                var brush = new SolidBrush(ForeColor);
                var boldFont = new Font(Font, FontStyle.Bold);
                var format = new StringFormat(StringFormatFlags.NoWrap) { Trimming = StringTrimming.EllipsisCharacter };
                int y = 0;
                int nameWidth = (Width / 2) - IndentionSize;
                foreach (var directory in Metadata)
                {
                    if (!directory.IsEmpty)
                    {
                        var directorySize = Size.Ceiling(e.Graphics.MeasureString(directory.Name, boldFont));
                        var directoryRect = new Rectangle(0, y, Width, directorySize.Height);
                        e.Graphics.DrawString(directory.Name, boldFont, brush, directoryRect, format);
                        y += directoryRect.Height;
                        foreach (var tag in directory.Tags)
                        {
                            var nameSize = Size.Ceiling(e.Graphics.MeasureString(tag.Name, boldFont));
                            var nameRect = new Rectangle(_IndentionSize, y, nameWidth, nameSize.Height);
                            e.Graphics.DrawString(tag.Name, boldFont, brush, nameRect, format);

                            // TODO More user-friendly metadata display
                            var value = directory.GetObject(tag.Type)?.ToString() ?? "N/A";
                            var valueSize = Size.Ceiling(e.Graphics.MeasureString(value, Font));
                            var valueRect = new Rectangle(Width / 2, y, Width / 2, valueSize.Height);
                            e.Graphics.DrawString(value, Font, brush, valueRect, format);

                            y += nameRect.Height;
                        }
                    }
                }
            }
            else base.OnPaint(e);
        }
    }
}
