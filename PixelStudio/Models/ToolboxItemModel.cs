using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelStudio.Models
{
    internal class ToolboxItemModel
    {
        public ToolboxItemModel(Image icon, string name)
        {
            Icon = icon;
            Name = name;
        }

        public Image Icon { get; }

        public string Name { get; }
    }
}
