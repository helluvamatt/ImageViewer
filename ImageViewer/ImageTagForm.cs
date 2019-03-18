using ImageViewer.Controls;
using ImageViewer.Data.Models;
using ImageViewer.Models;
using PixelStudio.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer
{
    internal partial class ImageTagForm : Form
    {
        private readonly ImageBrowser _ImageBrowser;
        private readonly BindingListEx<TagSelectModel> _Tags;

        public ImageTagForm(ImageBrowser imageBrowser)
        {
            InitializeComponent();
            _ImageBrowser = imageBrowser;
            _Tags = new BindingListEx<TagSelectModel>();
            tagSelectModelBindingSource.DataSource = _Tags;
        }

        public TagModel SelectedItem { get; private set; }

        private void OnTagsDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < _Tags.Count)
            {
                var tag = _Tags[e.Index];
                tag.DrawItem(e.Graphics, e.Font, e.ForeColor, e.Bounds, listBoxTags.ItemHeight);
            }
            e.DrawFocusRectangle();
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            if (txtTagSearch.Text.Length > 0)
            {
                var list = new List<TagSelectModel>();
                list.AddRange(_ImageBrowser.GetTagsForAutoComplete(txtTagSearch.Text).Select(t => new ExistingTagSelectModel(t)));
                list.Add(new NewTagSelectModel());
                _Tags.SetItems(list);
            }
            else
            {
                _Tags.Clear();
            }
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            if (tagSelectModelBindingSource.Current is TagSelectModel item)
            {
                var model = item.Model;
                if (model == null)
                {
                    model = new TagModel
                    {
                        Name = txtTagSearch.Text,
                        Color = DrawingUtils.RandomColor().ToArgb()
                    };
                    _ImageBrowser.SaveTag(model);
                }
                SelectedItem = model;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void OnDataSourceCurrentChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = tagSelectModelBindingSource.Current is TagSelectModel;
        }
    }
}
