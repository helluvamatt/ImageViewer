using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace ImageViewer.Properties
{
    internal sealed partial class Settings
    {
        public Settings()
        {
            string companyName = GetType().Assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            string productName = GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
            AppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), companyName, productName);
        }

        public string AppPath { get; }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Save();
        }
    }
}
