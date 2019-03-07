using ImageViewer.Data.Models;
using System;
using System.ComponentModel;
using System.Drawing;

namespace ImageViewer.Models
{
    internal class TagViewModel : INotifyPropertyChanged
    {
        public TagViewModel(TagModel model)
        {
            Model = model;
        }

        public TagModel Model { get; }

        public event EventHandler ModelChanged;

        public string Name
        {
            get => Model.Name;
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(nameof(Name));
                    ModelChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Color Color
        {
            get => Color.FromArgb(Model.Color);
            set
            {
                int c = value.ToArgb();
                if (Model.Color != c)
                {
                    Model.Color = c;
                    OnPropertyChanged(nameof(Color));
                    ModelChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
