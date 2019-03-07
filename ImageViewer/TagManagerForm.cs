using ImageViewer.Controls;
using ImageViewer.Data.Models;
using ImageViewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = ImageViewer.Properties.Resources;

namespace ImageViewer
{
    internal partial class TagManagerForm : Form, IToolStripForm
    {
        private readonly ImageBrowser _ImageBrowser;
        private readonly BindingListEx<TagViewModel> _TagItems;

        public TagManagerForm(ImageBrowser imageBrowser)
        {
            InitializeComponent();
            _ImageBrowser = imageBrowser;
            _TagItems = new BindingListEx<TagViewModel>();
            _TagItems.ListChanged += OnTagItemsListChanged;
            _TagItems.ListItemRemoving += OnTagItemsListItemRemoving;
            tagViewModelBindingSource.DataSource = _TagItems;
        }

        public ToolStrip ToolStrip => mainToolStrip;

        protected override void OnLoad(EventArgs e)
        {
            Icon = Icon?.Clone() as Icon;
            base.OnLoad(e);
            UseWaitCursor = true;
            Task.Run(() =>
            {
                var items = _ImageBrowser.GetTags().Select(t => new TagViewModel(t)).ToList();
                Invoke(new Action(() =>
                {
                    _TagItems.SetItems(items);
                    UseWaitCursor = false;
                }));
            });
        }

        private void OnTagItemsListItemRemoving(object sender, ListItemRemovingEventArgs<TagViewModel> e)
        {
            _ImageBrowser.RemoveTag(e.OldItem.Model);
        }

        private void OnTagItemsListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.ListChangedType == ListChangedType.ItemChanged) tagListBox.Invalidate(tagListBox.GetItemRectangle(e.NewIndex));
                var item = _TagItems[e.NewIndex].Model;
                Task.Run(() => _ImageBrowser.SaveTag(item));
            }
        }

        private void OnTagDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < _TagItems.Count)
            {
                var item = _TagItems[e.Index];
                e.Graphics.DrawListItemForColor(item.Color, new Size(20, 20), item.Name, e.Font, e.ForeColor, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void OnTagAddClick(object sender, EventArgs e)
        {
            int newTagIndex = 1;
            string newTagName = $"New Tag {newTagIndex}";
            while (_TagItems.Any(t => t.Name == newTagName))
            {
                newTagIndex++;
                newTagName = $"New Tag {newTagIndex}";
            }
            _TagItems.Add(new TagViewModel(new TagModel()) { Name = newTagName, Color = DrawingUtils.RandomColor() });
        }

        private void OnTagDeleteClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, R.AreYouSureDeleteTag, R.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tagViewModelBindingSource.RemoveCurrent();
            }
        }

        private void OnColorClick(object sender, EventArgs e)
        {
            colorDialog.Color = btnTagColor.ForeColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                btnTagColor.ForeColor = colorDialog.Color;
            }
        }
    }
}
