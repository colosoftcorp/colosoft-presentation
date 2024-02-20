using System;
using System.Linq;

namespace Colosoft.Presentation.Menu
{
    public class MenuItemsInserter
    {
        private readonly MenuCollection collection;
        private readonly Uri currentPath;
        private MenuItemsInserter inserterOpened;
        private MenuItemsInserter parent;

        internal MenuItemsInserter(MenuCollection collection, Uri currentPath)
        {
            this.collection = collection;
            this.currentPath = currentPath;
        }

        private static Uri Merge(Uri from, string append)
        {
            var segments = from.Segments.Where(f => f != "/" && f != "\\").Select(f => f.TrimEnd('/', '\\')).ToArray();
            return new Uri($"{from.Scheme}://{from.Host}/{string.Join("/", segments.Concat(new string[] { append }))}");
        }

        public MenuItemsInserter Begin(
            string name,
            IMessageFormattable label = null,
            IMenuPosition position = null,
            Action<IMenuFolder> configure = null)
        {
            if (this.inserterOpened != null)
            {
                throw new InvalidOperationException(ResourceMessageFormatter.Create(
                    () => Properties.Resources.MenuItemsInserter_InvalidOperation_ThereIsOpenInstance).Format());
            }

            var uri = Merge(this.currentPath, name);
            var folder = new MenuFolder(uri)
            {
                Label = label ?? name.GetFormatter(),
                Position = position ?? new AbsolutePosition(-1),
            };

            this.Add(folder);

            if (configure != null)
            {
                configure(folder);
            }

            this.inserterOpened = new MenuItemsInserter(this.collection, uri)
            {
                parent = this,
            };

            return this.inserterOpened;
        }

        public MenuItemsInserter Begin(IMenuItem item)
        {
            if (this.inserterOpened != null)
            {
                throw new InvalidOperationException(ResourceMessageFormatter.Create(
                    () => Properties.Resources.MenuItemsInserter_InvalidOperation_ThereIsOpenInstance).Format());
            }

            this.Add(item);

            this.inserterOpened = new MenuItemsInserter(
                this.collection,
                Merge(this.currentPath, item.Name))
            {
                parent = this,
            };

            return this.inserterOpened;
        }

        public MenuItemsInserter Close()
        {
            this.parent.inserterOpened = null;
            return this.parent;
        }

        public MenuCollection Collection()
        {
            return this.collection;
        }

        public MenuItemsInserter Add(IMenuItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Path = Merge(this.currentPath, item.Name);
            this.collection.Add(item);
            return this;
        }

        public MenuItemsInserter Add(
            string name,
            IMessageFormattable label,
            System.Windows.Input.ICommand command,
            object commandParameter = null,
            IMessageFormattable toolTipTitle = null,
            string gestures = null,
            Action<IMenuItem> configure = null)
        {
            var item = new MenuItem(name)
            {
                Label = label,
                Command = command,
                CommandParameter = commandParameter,
                ToolTipTitle = toolTipTitle ?? label,
                Gestures = gestures,
            };

            var positions = this.collection.Select(f => f.Position).OfType<AbsolutePosition>();

            if (positions.Any())
            {
                item.Position = new AbsolutePosition(positions.Max(f => f.Index) + 1);
            }

            var result = this.Add(item);

            if (configure != null)
            {
                configure(item);
            }

            return result;
        }
    }
}
