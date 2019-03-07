using System;
using System.ComponentModel;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer.Models
{
    [AttributeUsage(AttributeTargets.All)]
    internal class DescriptionResAttribute : DescriptionAttribute
    {
        public DescriptionResAttribute(string resourceName) : base(R.ResourceManager.GetString(resourceName) ?? resourceName) { }
    }
}
