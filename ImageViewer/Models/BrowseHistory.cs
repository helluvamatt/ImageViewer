using ImageViewer.Controls;
using ImageViewer.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Settings = ImageViewer.Properties.Settings;

namespace ImageViewer.Models
{
    internal sealed class BrowseHistory : IDisposable
    {
        private readonly ImageBrowser _Browser;
        private readonly List<BrowseHistoryPage> _Pages;

        private int _CurrentPageIndex;

        public BrowseHistory(ImageBrowser browser)
        {
            _Browser = browser;
            _Pages = new List<BrowseHistoryPage>();

            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
        }

        public BrowseHistoryPage CurrentPage => _CurrentPageIndex < _Pages.Count ? _Pages[_CurrentPageIndex] : null;
        public event EventHandler CurrentPageChanged;

        public bool BackEnabled => _CurrentPageIndex < _Pages.Count - 1;
        public event EventHandler BackEnabledChanged;

        public bool ForwardEnabled => _CurrentPageIndex > 0;
        public event EventHandler ForwardEnabledChanged;

        public bool UpEnabled => CurrentPage != null && CurrentPage.CanNavigateUp(_Browser);
        public event EventHandler UpEnabledChanged;

        public void GoUp()
        {
            if (CurrentPage != null && CurrentPage.CanNavigateUp(_Browser))
            {
                var target = CurrentPage.GetParent(_Browser);
                if (target != null) NavigateTo(target);
            }
        }

        public void Reload()
        {
            // Force listeners to refresh
            OnCurrentPageChanged();
        }

        public void GoBack()
        {
            if (BackEnabled) _CurrentPageIndex++;
            OnCurrentPageChanged();
        }

        public void GoForward()
        {
            if (ForwardEnabled) _CurrentPageIndex--;
            OnCurrentPageChanged();
        }

        public void NavigateTo(BrowseHistoryPage page)
        {
            if (_CurrentPageIndex > 0)
            {
                _Pages.RemoveRange(0, _CurrentPageIndex);
                _CurrentPageIndex = 0;
            }
            _Pages.Insert(0, page);
            OnCurrentPageChanged();
        }

        public void Clear()
        {
            _Pages.Clear();
            OnCurrentPageChanged();
        }

        private void OnCurrentPageChanged()
        {
            BackEnabledChanged?.Invoke(this, EventArgs.Empty);
            ForwardEnabledChanged?.Invoke(this, EventArgs.Empty);
            UpEnabledChanged?.Invoke(this, EventArgs.Empty);
            CurrentPageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.LibraryBrowserShowFolders) == e.PropertyName)
            {
                // Force listeners to refresh
                OnCurrentPageChanged();
            }
        }

        public void Dispose()
        {
            Settings.Default.PropertyChanged -= OnSettingsPropertyChanged;
        }
    }

    internal abstract class BrowseHistoryPage
    {
        public abstract IEnumerable<ListItem> GetListItems(ImageBrowser browser);

        public abstract bool IsIncluded(ImageBrowser browser, ImageModel imageModel);

        public abstract bool CanNavigateUp(ImageBrowser browser);

        public abstract BrowseHistoryPage GetParent(ImageBrowser browser);

        //public abstract int GetImageCount(ImageBrowser browser);

        public abstract IEnumerable<ImageModel> GetImages(ImageBrowser browser);
    }

    internal class BrowseHistoryTagPage : BrowseHistoryPage
    {
        public BrowseHistoryTagPage(TagModel tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public TagModel Tag { get; }

        public override bool CanNavigateUp(ImageBrowser browser) => false;
        public override BrowseHistoryPage GetParent(ImageBrowser browser) => null;
        public override IEnumerable<ListItem> GetListItems(ImageBrowser browser) => GetImages(browser).Select(m => new ImageListItem(m));
        public override bool IsIncluded(ImageBrowser browser, ImageModel imageModel) => browser.GetImageHasTag(imageModel, Tag);
        //public override int GetImageCount(ImageBrowser browser) => browser.CountImagesWithTag(Tag);
        public override IEnumerable<ImageModel> GetImages(ImageBrowser browser) => browser.GetImagesWithTag(Tag);
    }

    internal class BrowseHistoryFolderPage : BrowseHistoryPage
    {
        public BrowseHistoryFolderPage(string folderPath)
        {
            FolderPath = folderPath ?? throw new ArgumentNullException(nameof(folderPath));
        }

        public string FolderPath { get; }

        public override IEnumerable<ImageModel> GetImages(ImageBrowser browser) => browser.GetImagesInFolder(FolderPath);
        //public override int GetImageCount(ImageBrowser browser) => browser.CountImagesInFolder(FolderPath);

        public override IEnumerable<ListItem> GetListItems(ImageBrowser browser)
        {
            var list = new List<ListItem>();
            if (Settings.Default.LibraryBrowserShowFolders) list.AddRange(browser.GetFolders(FolderPath).Select(p => new FolderListItem(p, browser.CountImagesInFolder(p), browser.CountFolders(p), browser.GetFolderLastModified(p))));
            list.AddRange(GetImages(browser).Select(m => new ImageListItem(m)));
            return list;
        }

        public override bool IsIncluded(ImageBrowser browser, ImageModel imageModel) =>  imageModel.FolderPath.StartsWith(FolderPath);

        public override bool CanNavigateUp(ImageBrowser browser) => !string.IsNullOrEmpty(browser.GetRoot(FolderPath));
        public override BrowseHistoryPage GetParent(ImageBrowser browser) => new BrowseHistoryFolderPage(Directory.GetParent(FolderPath)?.FullName);
    }
}
