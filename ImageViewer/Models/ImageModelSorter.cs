using ImageViewer.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ImageViewer.Models
{
    internal class ImageModelSorter : IComparer<ImageModel>, IComparer<DirectoryInfo>, INotifyPropertyChanged
    {
        private Sort _OrderBy;
        public Sort OrderBy
        {
            get => _OrderBy;
            set
            {
                if (_OrderBy != value)
                {
                    _OrderBy = value;
                    OnPropertyChanged(nameof(OrderBy));
                }
            }
        }

        private SortOrder _Order;
        public SortOrder Order
        {
            get => _Order;
            set
            {
                if (_Order != value)
                {
                    _Order = value;
                    OnPropertyChanged(nameof(Order));
                }
            }
        }

        public void SetSort(Sort sort, SortOrder order)
        {
            _OrderBy = sort;
            _Order = order;

            // We changed both, but only fire event once
            OnPropertyChanged(nameof(OrderBy) + "," + nameof(Order));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Compare(ImageModel x, ImageModel y)
        {
            if (Order == SortOrder.None) return 0;
            int result;
            switch (OrderBy)
            {
                case Sort.FileSize:
                    result = x.FileSize.CompareTo(y.FileSize);
                    break;
                case Sort.ModifiedDate:
                    result = x.FileModifiedDate.CompareTo(y.FileModifiedDate);
                    break;
                case Sort.CreatedDate:
                    result = x.FileCreatedDate.CompareTo(y.FileCreatedDate);
                    break;
                case Sort.ImageSize:
                    result = (x.Width * x.Height).CompareTo(y.Width * y.Height);
                    break;
                case Sort.Name:
                default:
                    result = x.Name.CompareTo(y.Name);
                    break;
            }
            if (Order == SortOrder.Descending) result = -result;
            return result;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            if (Order == SortOrder.None) return 0;
            int result;
            switch (OrderBy)
            {
                case Sort.ModifiedDate:
                    result = x.LastWriteTime.CompareTo(y.LastWriteTime);
                    break;
                case Sort.CreatedDate:
                    result = x.CreationTime.CompareTo(y.CreationTime);
                    break;
                case Sort.Name:
                case Sort.FileSize:
                case Sort.ImageSize:
                default:
                    result = x.Name.CompareTo(y.Name);
                    break;
            }
            if (Order == SortOrder.Descending) result = -result;
            return result;
        }
    }
}
