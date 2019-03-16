using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelStudio.Controls
{
    internal static class DrawingUtils
    {
        public static void DrawListItem(this Graphics g, Image image, string text, Font font, Color foreColor, Rectangle bounds)
        {
            if (image != null)
            {
                var imageRect = new Rectangle(bounds.X, bounds.Y, bounds.Height, bounds.Height);
                DrawImageCentered(g, image, imageRect);
            }
            var textRect = new Rectangle(bounds.X + bounds.Height, bounds.Y, bounds.Width - bounds.Height, bounds.Height);
            DrawStringVerticallyCentered(g, text, font, foreColor, textRect);
        }

        public static Rectangle GetCenteredRegion(Size size, Rectangle bounds) => new Rectangle(bounds.X + (bounds.Width - size.Width) / 2, bounds.Y + (bounds.Height - size.Height) / 2, size.Width, size.Height);

        public static Rectangle GetCenteredImageRegion(Image image, Rectangle bounds) => GetCenteredRegion(image.Size, bounds);

        public static void DrawImageCentered(this Graphics g, Image image, Rectangle bounds)
        {
            g.DrawImage(image, GetCenteredImageRegion(image, bounds).Location);
        }

        public static void DrawImageZoomed(this Graphics g, Image image, RectangleF bounds)
        {
            float scale = Math.Min(bounds.Width / image.Width, bounds.Height / image.Height);
            float width = image.Width * scale;
            float height = image.Height * scale;
            float x = bounds.X + (bounds.Width - width) / 2;
            float y = bounds.Y + (bounds.Height - height) / 2;
            var targetRect = new RectangleF(x, y, width, height);
            g.DrawImage(image, targetRect);
        }

        public static void DrawImageFit(this Graphics g, Image image, Rectangle bounds)
        {
            if (image.Width > bounds.Width || image.Height > bounds.Height) DrawImageZoomed(g, image, bounds);
            else DrawImageCentered(g, image, bounds);
        }

        public static void DrawStringVerticallyCentered(this Graphics g, string s, Font font, Color foreColor, RectangleF bounds, StringFormat stringFormat = null)
        {
            if (stringFormat == null) stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(s, font, new SolidBrush(foreColor), bounds, stringFormat);
        }

        public static void DrawImageAndStringVerticallyCentered(this Graphics g, string s, Image image, Font font, Color foreColor, Rectangle bounds, int spacing = 4, StringFormat stringFormat = null)
        {
            if (stringFormat == null) stringFormat = new StringFormat();
            var textSize = Size.Ceiling(g.MeasureString(s, font, bounds.Width - image.Width, stringFormat));
            var totalWidth = image.Width + textSize.Width + spacing;
            var totalHeight = Math.Max(image.Height, textSize.Height);
            var totalBounds = GetCenteredRegion(new Size(totalWidth, totalHeight), bounds);
            g.DrawImageCentered(image, new Rectangle(totalBounds.X, totalBounds.Y, image.Width, totalBounds.Height));
            g.DrawStringVerticallyCentered(s, font, foreColor, new Rectangle(totalBounds.X + image.Width + spacing, totalBounds.Y, totalBounds.Width - (image.Width + spacing), totalBounds.Height), stringFormat);
        }
    }
}
