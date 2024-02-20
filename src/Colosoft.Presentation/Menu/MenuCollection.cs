using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Colosoft.Presentation.Menu
{
    public class MenuCollection : IEnumerable<IMenuItem>, INotifyPropertyChanged
    {
        private readonly List<IMenuItem> innerList = new List<IMenuItem>();
        private bool isEnabled = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsEnabled
        {
            get => this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.RaisePropertyChanged(nameof(this.IsEnabled));
                }
            }
        }

        public int Count => this.innerList.Count;

        public IMenuItem this[int index] => this.innerList[index];

        public MenuCollection()
        {
        }

        public MenuCollection(IEnumerable<IMenuItem> entries)
        {
            if (entries == null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            this.innerList.AddRange(entries);
        }

        public MenuItemsInserter CreateInserter(string root)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            var uri = new Uri($"{MenuItem.MenuScheme}://{root}");
            var inserter = new MenuItemsInserter(this, uri);
            return inserter;
        }

        public MenuItemsInserter CreateInserter(
            string root,
            IMessageFormattable label,
            IMenuPosition position = null,
            System.Windows.Input.ICommand command = null,
            Action<IMenuFolder> configure = null)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            var uri = new Uri($"{MenuItem.MenuScheme}://{root}");
            var inserter = new MenuItemsInserter(this, uri);

            var item = new MenuFolder(uri)
            {
                Label = label ?? root.GetFormatter(),
                Position = position ?? new AbsolutePosition(-1),
                Command = command ?? new Input.DelegateCommand(() => { }, () => true),
            };

            this.Add(item);

            if (configure != null)
            {
                configure(item);
            }

            return inserter;
        }

        public void Add(IMenuItem item)
        {
            this.innerList.Add(item);
            this.RaisePropertyChanged(nameof(this.Count));
        }

        public bool Remove(IMenuItem item)
        {
            if (this.innerList.Remove(item))
            {
                this.RaisePropertyChanged(nameof(this.Count));
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
            this.RaisePropertyChanged(nameof(this.Count));
        }

#pragma warning disable CA1030 // Use events where appropriate
        protected void RaisePropertyChanged(params string[] names)
#pragma warning restore CA1030 // Use events where appropriate
        {
            if (this.PropertyChanged != null)
            {
                foreach (var i in names)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(i));
                }
            }
        }

        public IEnumerator<IMenuItem> GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }
    }
}
