using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Svg;

namespace ImageViewer.Controls
{
    internal static class DrawingUtils
    {
        #region SVG

        public static void DrawSvg(this Graphics g, byte[] iconData, Rectangle bounds)
        {
            using (var stream = new MemoryStream(iconData))
            {
                var svg = SvgDocument.Open<SvgDocument>(stream);
                svg.X = new SvgUnit(SvgUnitType.Pixel, bounds.X);
                svg.Y = new SvgUnit(SvgUnitType.Pixel, bounds.Y);
                svg.Width = new SvgUnit(SvgUnitType.Pixel, bounds.Width);
                svg.Height = new SvgUnit(SvgUnitType.Pixel, bounds.Height);
                svg.Draw(g);
            }
        }

        public static void DrawSvg(this Graphics g, byte[] iconData, Rectangle bounds, Color foreColor)
        {
            using (var stream = new MemoryStream(iconData))
            {
                var svg = SvgDocument.Open<SvgDocument>(stream);
                svg.Fill = new SingleColorPaintServer(foreColor);
                svg.X = new SvgUnit(SvgUnitType.Pixel, bounds.X);
                svg.Y = new SvgUnit(SvgUnitType.Pixel, bounds.Y);
                svg.Width = new SvgUnit(SvgUnitType.Pixel, bounds.Width);
                svg.Height = new SvgUnit(SvgUnitType.Pixel, bounds.Height);
                svg.Draw(g);
            }
        }

        public static Bitmap RenderSvg(byte[] iconData, Color foreColor, Size size)
        {
            var bm = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bm))
            {
                g.DrawSvg(iconData, new Rectangle(new Point(0, 0), size), foreColor);
            }
            return bm;
        }

        #endregion

        #region Rounded rectangle

        public static GraphicsPath RoundedRectangle(RectangleF bounds, Padding fromPadding)
        {
            var topLeft = Math.Max(fromPadding.Top, fromPadding.Left);
            var topRight = Math.Max(fromPadding.Top, fromPadding.Right);
            var bottomLeft = Math.Max(fromPadding.Bottom, fromPadding.Left);
            var bottomRight = Math.Max(fromPadding.Bottom, fromPadding.Right);
            return RoundedRectangle(bounds, topLeft, topRight, bottomLeft, bottomRight);
        }

        public static GraphicsPath RoundedRectangle(RectangleF bounds, int radius)
        {
            return RoundedRectangle(bounds, radius, radius, radius, radius);
        }

        public static GraphicsPath RoundedRectangle(RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomLeft, int radiusBottomRight)
        {
            GraphicsPath path = new GraphicsPath();

            // Short-circuit for no rounding
            if (radiusTopLeft == 0 && radiusTopRight == 0 && radiusBottomLeft == 0 && radiusBottomRight == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            if (radiusTopLeft > 0)
            {
                path.AddArc(new RectangleF(bounds.Left, bounds.Top, radiusTopLeft * 2, radiusTopLeft * 2), 180, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Left, bounds.Top), new PointF(bounds.Left, bounds.Top));
            }

            // top right arc
            if (radiusTopRight > 0)
            {
                path.AddArc(new RectangleF(bounds.Right - radiusTopRight * 2, bounds.Top, radiusTopRight * 2, radiusTopRight * 2), 270, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Right, bounds.Top), new PointF(bounds.Right, bounds.Top));
            }

            // bottom right arc
            if (radiusBottomRight > 0)
            {
                path.AddArc(new RectangleF(bounds.Right - radiusBottomRight * 2, bounds.Bottom - radiusBottomRight * 2, radiusBottomRight * 2, radiusBottomRight * 2), 0, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Right, bounds.Bottom), new PointF(bounds.Right, bounds.Bottom));
            }

            // bottom left arc
            if (radiusBottomLeft > 0)
            {
                path.AddArc(new RectangleF(bounds.Left, bounds.Bottom - radiusBottomLeft * 2, radiusBottomLeft * 2, radiusBottomLeft * 2), 90, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Left, bounds.Bottom), new PointF(bounds.Left, bounds.Bottom));
            }

            path.CloseFigure();
            return path;
        }

        #endregion

        public static void DrawListItem(this Graphics g, Image image, string text, Font font, Color foreColor, RectangleF bounds)
        {
            if (image != null)
            {
                var imageRect = new RectangleF(bounds.X, bounds.Y, bounds.Height, bounds.Height);
                DrawImageCentered(g, image, imageRect);
            }
            var textRect = new RectangleF(bounds.X + bounds.Height, bounds.Y, bounds.Width - bounds.Height, bounds.Height);
            DrawStringVerticallyCentered(g, text, font, foreColor, textRect);
        }

        public static void DrawListItemForColor(this Graphics g, Color color, SizeF colorSize, string text, Font font, Color foreColor, RectangleF bounds)
        {
            float colorBoxPadding = (bounds.Height - colorSize.Height) / 2;
            var colorBoxRect = new RectangleF(bounds.X + colorBoxPadding, bounds.Y + colorBoxPadding, colorSize.Width, colorSize.Height);
            g.FillRectangle(new SolidBrush(color), colorBoxRect);
            var textRect = new RectangleF(bounds.X + colorSize.Width + colorBoxPadding * 2, bounds.Y, bounds.Width - (colorSize.Width + colorBoxPadding * 2), bounds.Height);
            DrawStringVerticallyCentered(g, text, font, foreColor, textRect);
        }

        public static RectangleF GetCenteredImageRegion(Image image, RectangleF bounds)
        {
            return new RectangleF(bounds.X + (bounds.Width - image.Width) / 2, bounds.Y + (bounds.Height - image.Height) / 2, image.Width, image.Height);
        }

        public static void DrawImageCentered(this Graphics g, Image image, RectangleF bounds)
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

        public static void DrawStringVerticallyCentered(this Graphics g, string s, Font font, Color foreColor, RectangleF bounds, StringFormat stringFormat = null)
        {
            if (stringFormat == null) stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(s, font, new SolidBrush(foreColor), bounds, stringFormat);
        }

        public static void DrawTag(this Graphics g, string s, Font font, Color tagColor, Point location, Padding padding)
        {
            var textBounds = Size.Ceiling(g.MeasureString(s, font));
            var tagBounds = new Rectangle(location, new Size(textBounds.Width + padding.Horizontal, textBounds.Height + padding.Vertical));
            DrawTag(g, s, font, tagColor, tagBounds, true);
        }

        public static void DrawTagListItem(this Graphics g, string s, Font font, Color tagColor, Rectangle bounds, int itemHeight, bool fillWidth = false, StringAlignment textAlign = StringAlignment.Near)
        {
            var textBounds = Size.Ceiling(g.MeasureString(s, font));
            int availableSpace = bounds.Height - textBounds.Height;
            var padding = new Padding(availableSpace / 4);
            bounds = new Rectangle(availableSpace / 4, availableSpace / 4, bounds.Width - availableSpace / 2, bounds.Height - availableSpace / 2);
            if (!fillWidth) bounds = new Rectangle(bounds.Location, new Size(textBounds.Width + padding.Horizontal, bounds.Height));
            DrawTag(g, s, font, tagColor, GetIdealTextColor(tagColor), bounds, padding, textAlign);
        }

        public static void DrawTag(this Graphics g, string s, Font font, Color tagColor, Rectangle bounds, bool fillWidth = false, StringAlignment textAlign = StringAlignment.Near)
        {
            var textBounds = Size.Ceiling(g.MeasureString(s, font));
            var padding = new Padding((bounds.Height - textBounds.Height) / 2);
            if (!fillWidth) bounds = new Rectangle(bounds.Location, new Size(textBounds.Width + padding.Horizontal, bounds.Height));
            DrawTag(g, s, font, tagColor, GetIdealTextColor(tagColor), bounds, padding, textAlign);
        }

        public static void DrawTag(this Graphics g, string s, Font font, Color backColor, Color foreColor, RectangleF bounds, Padding padding, StringAlignment textAlign = StringAlignment.Near)
        {
            var borderRect = new RectangleF(bounds.Location, new SizeF(bounds.Width - 1, bounds.Height - 1));
            var borderPath = RoundedRectangle(borderRect, padding);
            g.FillPath(new SolidBrush(backColor), borderPath);
            g.DrawPath(new Pen(foreColor), borderPath);

            var textRect = new RectangleF(bounds.X + padding.Left, bounds.Y + padding.Top, bounds.Width - padding.Horizontal, bounds.Height - padding.Vertical);
            g.DrawStringVerticallyCentered(s, font, foreColor, textRect, new StringFormat { Alignment = textAlign });
        }

        // Stolen from: https://www.codeproject.com/Articles/16565/Determining-Ideal-Text-Color-Based-on-Specified-Ba
        public static Color GetIdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            return (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
        }

        public static Color RandomColor()
        {
            var rng = new Random();
            return Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));
        }

    }

    internal class SingleColorPaintServer : SvgPaintServer
    {
        private readonly Color _Color;

        public SingleColorPaintServer(Color color)
        {
            _Color = color;
        }

        public override SvgElement DeepCopy()
        {
            return new SingleColorPaintServer(_Color);
        }

        public override Brush GetBrush(SvgVisualElement styleOwner, ISvgRenderer renderer, float opacity, bool forStroke = false)
        {
            return new SolidBrush(_Color);
        }
    }
}
