using ImageViewer.Controls;
using ImageViewer.Data.Models;
using System.Drawing;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer.Models
{
    internal abstract class TagSelectModel
    {
        public abstract string Name { get; }

        public abstract TagModel Model { get; }

        public abstract void DrawItem(Graphics g, Font font, Color foreColor, Rectangle bounds, int itemHeight);
    }

    internal class ExistingTagSelectModel : TagSelectModel
    {
        public ExistingTagSelectModel(TagModel tagModel)
        {
            Model = tagModel;
        }

        public override TagModel Model { get; }

        public override string Name => Model.Name;

        public override void DrawItem(Graphics g, Font font, Color foreColor, Rectangle bounds, int itemHeight)
        {
            g.DrawTagListItem(Name, font, Color.FromArgb(Model.Color), bounds, itemHeight, false);
        }
    }

    internal class NewTagSelectModel : TagSelectModel
    {
        public override string Name => R.AddTag;

        public override TagModel Model => null;

        public override void DrawItem(Graphics g, Font font, Color foreColor, Rectangle bounds, int itemHeight)
        {
            g.DrawListItem(R.tag_add_16, R.AddTag, font, foreColor, bounds);
        }
    }
}
