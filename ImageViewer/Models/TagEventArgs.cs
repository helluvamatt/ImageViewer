using ImageViewer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.Models
{
    internal class TagEventArgs : EventArgs
    {
        public TagEventArgs(TagModel tagModel)
        {
            TagModel = tagModel;
        }

        public TagModel TagModel { get; }
    }
}
