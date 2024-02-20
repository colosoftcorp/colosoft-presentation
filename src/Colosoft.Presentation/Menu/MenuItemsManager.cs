using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation.Menu
{
    public class MenuItemsManager
    {
        private readonly List<MenuCollection> collections = new List<MenuCollection>();
        private readonly Dictionary<string, MenuItemParents> allItems = new Dictionary<string, MenuItemParents>();
        private readonly Collections.IObservableCollection<IMenuControlData> items = new Collections.BaseObservableCollection<IMenuControlData>();

        public Collections.IObservableCollection<IMenuControlData> Items => this.items;

        public int AllItemsCount => this.allItems.Count;

        private void MenuItemsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MenuCollection.IsEnabled))
            {
                this.ProcessCollection((MenuCollection)sender);
            }
        }

        private Uri GetParentKey(Uri from)
        {
            var segments = from.Segments.Where(f => f != "/" && f != "\\").Select(f => f.TrimEnd('/', '\\')).ToArray();

            if (segments.Length <= 1)
            {
                return null;
            }

            return new Uri($"{from.Scheme}://{from.Host}/{string.Join("/", segments.Take(segments.Length - 1))}");
        }

        private void ProcessCollection(MenuCollection collection)
        {
            if (collection.IsEnabled)
            {
                var conflicts = new List<IMenuItem>();

                foreach (IMenuItem i in collection)
                {
                    MenuItemParents aux = null;

                    if (this.allItems.TryGetValue(i.Path.ToString(), out aux) &&
                        !(aux.Item is IMenuFolder) &&
                        conflicts.Any(f => f == aux.Item))
                    {
                        conflicts.Add(aux.Item);
                    }
                }

                var collection2 = new List<IMenuItem>();
                foreach (IMenuItem i in collection)
                {
                    var parentKey = this.GetParentKey(i.Path);

                    if (parentKey != null && this.allItems.TryGetValue(parentKey.ToString(), out var item))
                    {
                        item.Parents.Add(collection);
                    }

                    collection2.Add(i);
                    var key = i.Path.ToString();
                    this.allItems.Add(key, new MenuItemParents(i, collection));
                }

                foreach (var group in collection2.GroupBy(f => f.Path.Segments.Length))
                {
                    foreach (var i in group.OrderBy(f => f.Position, MenuPositionComparer.Instance))
                    {
                        this.Add(i);
                    }
                }
            }
            else
            {
#pragma warning disable S6607 // The collection should be filtered before sorting by using "Where" before "OrderBy"
                foreach (var i in collection.OrderByDescending(f => f.ToString()).Where(i => this.Remove(i, collection)))
                {
                    this.allItems.Remove(i.Path.ToString());
                }
#pragma warning restore S6607 // The collection should be filtered before sorting by using "Where" before "OrderBy"
            }
        }

        private bool Remove(IMenuItem item, MenuCollection collection)
        {
            var itemParents = this.allItems[item.ToString()];
            itemParents.Parents.Remove(collection);

            IMenuControlData parent = null;

            var currentNode = parent = this.items.FirstOrDefault(f => f.Name == item.Path.Host);

            if (currentNode == null)
            {
                return false;
            }

            var parts = item.Path.Segments.Where(f => f != "/" && f != "\\").Select(f => f.Replace("/", string.Empty)).ToArray();

            if (parts.Length == 0 && (!(currentNode is IMenuFolderControlData) || ((IMenuFolderControlData)currentNode).Count == 0))
            {
                this.items.Remove(currentNode);
            }

            for (var i = 0; i < parts.Length; i++)
            {
                if (currentNode == null)
                {
                    throw new InvalidOperationException();
                }

                parent = currentNode;
                currentNode = parent.FirstOrDefault(f => f.Name == parts[i]);
            }

            if (itemParents.Parents.Count == 0)
            {
                parent.Children.Remove(currentNode);
                return true;
            }

            return false;
        }

        private void Add(IMenuItem item)
        {
            var root = this.items.FirstOrDefault(f => f.Name == item.Path.Host);

            if (root == null)
            {
                root = this.CreateControlData(item);
                var index = Collections.ListExtensions.BinarySearch(this.items, root, MenuControlDataPositionComparer.Instance);
                this.items.Insert((index < 0) ? ~index : index, root);
                return;
            }

            var currentNode = root;

            var parts = item.Path.Segments
                .Where(f => f != "/" && f != "\\").Select(f => f.TrimEnd('/', '\\'))
                .ToArray();

            for (var i = 0; i < parts.Length - 1; i++)
            {
                if (currentNode == null)
                {
                    throw new InvalidOperationException();
                }

                currentNode = currentNode.FirstOrDefault(f => f.Name == parts[i]);
            }

            if (currentNode != null)
            {
                IMenuControlData itemNode = currentNode.FirstOrDefault(f => f.Name == parts[parts.Length - 1]);

                if (itemNode == null)
                {
                    itemNode = this.CreateControlData(item);

                    var index = Collections.ListExtensions.BinarySearch(currentNode.Children, itemNode, MenuControlDataPositionComparer.Instance);

                    currentNode.Children.Insert((index < 0) ? ~index : index, itemNode);
                }
            }
        }

        private IMenuControlData CreateControlData(IMenuItem item)
        {
            if (item is IMenuFolder)
            {
                return new MenuFolderControlData(item);
            }

            return new MenuControlData(item);
        }

        public void Add(MenuCollection items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            items.PropertyChanged += this.MenuItemsPropertyChanged;
            this.collections.Add(items);

            this.ProcessCollection(items);
        }

        public void Clear()
        {
            this.allItems.Clear();
            this.items.Clear();

            foreach (var i in this.collections)
            {
                i.PropertyChanged -= this.MenuItemsPropertyChanged;
            }
        }

        private sealed class MenuItemParents
        {
            public IMenuItem Item { get; }

            public List<MenuCollection> Parents { get; } = new List<MenuCollection>();

            public MenuItemParents(IMenuItem item, MenuCollection parent)
            {
                this.Item = item;
                this.Parents.Add(parent);
            }
        }
    }
}
