using System;
using System.ComponentModel;
using System.Drawing;

namespace PixelStudio.Models
{
    internal class LayerModel : INotifyPropertyChanged, ICloneable, IDisposable
    {
        private string _Name;
        private Rectangle _Bounds;
        private bool _IsVisible;
        private Image _Raster;

        public LayerModel(string name, Rectangle bounds, Color fillColor)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
            _Bounds = bounds;
            _Raster = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(_Raster)) g.Clear(fillColor);
            _IsVisible = true;
        }

        public LayerModel(string name, Image raster, Point offset)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
            _Raster = raster ?? throw new ArgumentNullException(nameof(raster));
            _Bounds = new Rectangle(offset, _Raster.Size);
            _IsVisible = true;
        }

        // Clone constructor
        private LayerModel(string name, Image raster, Point offset, bool isVisible)
        {
            _Name = name;
            _Raster = (Image)raster.Clone();
            _Bounds = new Rectangle(offset, _Raster.Size);
            _IsVisible = isVisible;
        }
        
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        
        public bool IsVisible
        {
            get => _IsVisible;
            set
            {
                if (_IsVisible != value)
                {
                    _IsVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public Rectangle Bounds
        {
            get => _Bounds;
            private set
            {
                if (_Bounds != value)
                {
                    _Bounds = value;
                    OnPropertyChanged(nameof(Bounds));
                }
            }
        }

        public bool IsDisposed { get; private set; }

        public void Draw(Graphics g, Rectangle imageBounds)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(LayerModel));
            g.SetClip(imageBounds);
            g.DrawImage(_Raster, _Bounds.Location);
            g.ResetClip();
        }

        public void ResizeLayer(Size newSize, Point offset, Color fillColor)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(LayerModel));
            var pt = _Bounds.Location;
            pt.X += offset.X;
            pt.Y += offset.Y;
            var resizedRaster = new Bitmap(newSize.Width, newSize.Height);
            using (var g = Graphics.FromImage(resizedRaster))
            {
                g.Clear(fillColor);
                g.DrawImage(_Raster, -offset.X, -offset.Y);
            }
            _Raster.Dispose();
            _Raster = resizedRaster;
            Bounds = new Rectangle(pt, newSize);
        }

        public void ScaleLayer(float dx, float dy)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(LayerModel));
            int offsetX = (int)Math.Round(_Bounds.X * dx);
            int offsetY = (int)Math.Round(_Bounds.Y * dy);
            int width = (int)Math.Round(_Bounds.Width * dx);
            int height = (int)Math.Round(_Bounds.Height * dy);
            var resizedRaster = new Bitmap(width, height);
            using (var g = Graphics.FromImage(resizedRaster))
            {
                g.DrawImage(_Raster, new Rectangle(0, 0, width, height));
            }
            _Raster.Dispose();
            _Raster = resizedRaster;
            Bounds = new Rectangle(offsetX, offsetY, width, height);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone() => CloneLayer();

        public LayerModel CloneLayer()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(LayerModel));
            return new LayerModel(_Name, _Raster, _Bounds.Location, _IsVisible);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (IsDisposed) return;
            _Raster.Dispose();
            IsDisposed = true;
        }

        #endregion
    }
}
