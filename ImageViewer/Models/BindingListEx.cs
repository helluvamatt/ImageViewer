using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageViewer.Models
{
    internal class BindingListEx<T> : BindingList<T>
    {
        public event EventHandler<ListItemRemovingEventArgs<T>> ListItemRemoving;
        public event EventHandler<ListItemChangingEventArgs<T>> ListItemChanging;
        public event CancelEventHandler ListClearing;

        protected override void RemoveItem(int index)
        {
            var args = new ListItemRemovingEventArgs<T>(this[index], index);
            ListItemRemoving?.Invoke(this, args);
            if (args.Cancel) return;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            var args = new ListItemChangingEventArgs<T>(this[index], item, index);
            ListItemChanging?.Invoke(this, args);
            if (args.Cancel) return;
            base.SetItem(index, item);
        }

        protected override void ClearItems()
        {
            var e = new CancelEventArgs();
            ListClearing?.Invoke(this, e);
            if (e.Cancel) return;
            base.ClearItems();
        }

        public void SetItems(IEnumerable<T> items)
        {
            RaiseListChangedEvents = false;
            Clear();
            AddItems(items);
            RaiseListChangedEvents = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public void AddItems(IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    Add(item);
                }
            }
        }
    }

    internal class ListItemRemovingEventArgs<T> : CancelEventArgs
    {
        public ListItemRemovingEventArgs(T oldItem, int index)
        {
            OldItem = oldItem;
        }

        public T OldItem { get; }

        public int Index { get; }
    }

    internal class ListItemChangingEventArgs<T> : ListItemRemovingEventArgs<T>
    {
        public ListItemChangingEventArgs(T oldItem, T newItem, int index) : base(oldItem, index)
        {
            NewItem = newItem;
        }

        public T NewItem { get; }
    }
}
