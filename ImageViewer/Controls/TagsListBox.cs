using ImageViewer.Data.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    [ComplexBindingProperties(nameof(DataSource))]
    internal class TagsListBox : FlowLayoutPanel
    {
        private readonly TextBox _NewTagTextBox;

        private BindingList<TagModel> _BindingList;

        public TagsListBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            Cursor = Cursors.IBeam;
            _NewTagTextBox = new TextBox
            {
                Height = 16,
                AcceptsReturn = true,
                BorderStyle = BorderStyle.None,
                BackColor = BackColor,
                ForeColor = ForeColor,
                Margin = new Padding(0),
            };
            _NewTagTextBox.KeyUp += OnNewTagTextBoxKeyUp;
            _NewTagTextBox.LostFocus += OnNewTagTextBoxLostFocus;
            _NewTagTextBox.TextChanged += OnNewTagTextBoxTextChanged;
        }

        #region DataSource handling

        [DefaultValue(null)]
        public object DataSource
        {
            get => _BindingList;
            set
            {
                if (_BindingList != value)
                {
                    if (_BindingList != null) _BindingList.ListChanged -= OnBindingListChanged;
                    if (value != null && !(value is BindingList<TagModel>)) throw new ArgumentException("DataSource must be of type BindingList<TagModel>", nameof(DataSource));
                    if (value is BindingList<TagModel> bindingList)
                    {
                        _BindingList = bindingList;
                        _BindingList.ListChanged += OnBindingListChanged;
                    }
                    else _BindingList = null;
                    RefreshList();
                }
            }
        }

        #endregion

        public event EventHandler<TagAddingEventArgs> TagAdding;

        protected void OnTagAdding()
        {
            TagAdding?.Invoke(this, new TagAddingEventArgs(_NewTagTextBox.Text));
            _NewTagTextBox.Text = string.Empty;
        }

        #region TagPadding property

        private Padding _TagPadding;
        [Browsable(true)]
        [Category("Layout")]
        public Padding TagPadding
        {
            get => _TagPadding;
            set
            {
                if (_TagPadding != value)
                {
                    _TagPadding = value;
                    OnTagPaddingChanged();
                }
            }
        }

        public event EventHandler TagPaddingChanged;

        protected void OnTagPaddingChanged()
        {
            foreach (var ctrl in Controls.OfType<TagItem>())
            {
                ctrl.Padding = TagPadding;
            }
            TagPaddingChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor != value) base.ForeColor = _NewTagTextBox.ForeColor = value;
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor != value) base.BackColor = _NewTagTextBox.BackColor = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            Focus();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _NewTagTextBox.Focus();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            OnNewTagTextBoxLostFocus(this, e);
        }

        private void OnNewTagTextBoxLostFocus(object sender, EventArgs e)
        {
            if (_NewTagTextBox.Text != string.Empty)
            {
                OnTagAdding();
            }
        }

        private void OnNewTagTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            var key = (e.KeyData & Keys.KeyCode);
            if (_NewTagTextBox.Text == string.Empty && key == Keys.Back && _BindingList.Count > 0)
            {
                _BindingList?.RemoveAt(_BindingList.Count - 1);
                e.Handled = true;
            }
            if (_NewTagTextBox.Text != string.Empty && key == Keys.Enter)
            {
                OnTagAdding();
            }
        }

        private void OnNewTagTextBoxTextChanged(object sender, EventArgs e)
        {
            using (var g = _NewTagTextBox.CreateGraphics())
            {
                var size = g.MeasureString(_NewTagTextBox.Text + " ", _NewTagTextBox.Font, int.MaxValue, new StringFormat(StringFormatFlags.MeasureTrailingSpaces));
                _NewTagTextBox.Width = (int)size.Width + 4;
            }
        }

        private void OnBindingListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    AddTag(_BindingList[e.NewIndex], e.NewIndex);
                    break;
                case ListChangedType.ItemChanged:
                    (Controls[e.NewIndex] as TagItem).TagModel = _BindingList[e.NewIndex];
                    break;
                case ListChangedType.ItemDeleted:
                    Controls.RemoveAt(e.NewIndex);
                    break;
                case ListChangedType.ItemMoved:
                    var ctrl = Controls[e.OldIndex];
                    Controls.SetChildIndex(ctrl, e.NewIndex);
                    break;
                default:
                    RefreshList();
                    break;
            }
        }

        private void RefreshList()
        {
            Controls.Clear();
            if (_BindingList != null)
            {
                foreach (var tag in _BindingList)
                {
                    AddTag(tag);
                }
            }
            Controls.Add(_NewTagTextBox);
        }

        private void AddTag(TagModel tag, int index = -1)
        {
            var tagCtrl = new TagItem()
            {
                TagModel = tag,
                Padding = TagPadding,
                Margin = new Padding(0, 0, 3, 3),
            };
            tagCtrl.Removed += OnTagItemRemoved;
            Controls.Add(tagCtrl);
            if (index > -1) Controls.SetChildIndex(tagCtrl, index);
        }

        private void OnTagItemRemoved(object sender, EventArgs e)
        {
            var index = Controls.IndexOf((TagItem)sender);
            if (index > -1) _BindingList?.RemoveAt(index);
        }
    }

    internal class TagAddingEventArgs : EventArgs
    {
        public TagAddingEventArgs(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
