using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    internal static class TableRenderer
    {
        public static Size ComputeTableSize(Graphics g, string[][] data, Font[] fonts, int cellSpacing, Padding cellPadding, int fitWidth, out int[] columnWidths)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (fonts == null) throw new ArgumentNullException(nameof(fonts));
            if (fonts.Length < 1) throw new ArgumentException("fonts array must not be empty");

            // Number of columns = max size of inner array
            int columnCount = data.Max(row => row?.Length ?? 0);
            columnWidths = new int[columnCount];

            // Build a proper sized array of fonts to use
            var fontArray = new Font[columnCount];
            for (int i = 0; i < fontArray.Length; i++)
            {
                if (i < fonts.Length) fontArray[i] = fonts[i];
                else fontArray[i] = fonts[fonts.Length - 1]; // Last font is copied until the array is filled
            }

            // First pass: find sizes of auto-size columns
            var rowHeights = new int[data.Length];
            for (int y = 0; y < data.Length; y++)
            {
                var row = data[y];
                for (int x = 0; x < columnCount - 1; x++)
                {
                    if (x < row.Length)
                    {
                        var colSize = Size.Ceiling(g.MeasureString(row[x], fontArray[x]));
                        if (colSize.Width > columnWidths[x]) columnWidths[x] = colSize.Width;
                        if (colSize.Height > rowHeights[y]) rowHeights[y] = colSize.Height;
                    }
                }
            }

            // Second pass: measure last column that will wrap to fit the remaining width and compute total height of each row
            int totalHeight = 0;
            int availableSpace = fitWidth;
            availableSpace -= columnWidths.Sum();
            availableSpace -= cellSpacing * (columnCount - 1);
            availableSpace -= cellPadding.Horizontal * columnCount;
            for (int y = 0; y < data.Length; y++)
            {
                var row = data[y];
                if (row.Length == columnCount)
                {
                    var colSize = Size.Ceiling(g.MeasureString(row[columnCount - 1], fontArray[columnCount - 1], availableSpace, new StringFormat()));
                    if (colSize.Height > rowHeights[y]) rowHeights[y] = colSize.Height;
                }
                totalHeight += rowHeights[y] + cellPadding.Vertical;
            }
            totalHeight += cellSpacing * (columnCount - 1);

            return new Size(fitWidth, totalHeight);
        }

        public static void RenderTable(Graphics g, string[][] columns, Font[] fonts, Color foreColor, int cellSpacing, Padding cellPadding, int fitWidth = 0)
        {

        }
    }
}
