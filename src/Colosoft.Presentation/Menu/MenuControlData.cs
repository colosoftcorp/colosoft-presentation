using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Colosoft.Presentation.Menu
{
    public class MenuControlData : IMenuControlData
    {
        private readonly IMenuItem menuItem;
        private readonly ObservableCollection<IMenuControlData> children = new ObservableCollection<IMenuControlData>();

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        public string Name => this.menuItem.Name;

        public IMessageFormattable Label => this.menuItem.Label;

        public IMessageFormattable ToolTipTitle => this.menuItem.ToolTipTitle;

        public IMessageFormattable ToolTipDescription => this.menuItem.ToolTipDescription;

        public Uri LargeImage => this.menuItem.LargeImage;

        public Uri SmallImage => this.menuItem.SmallImage;

        public string Gestures => this.menuItem.Gestures;

        public System.Windows.Input.ICommand Command
        {
            get => this.menuItem.Command;
            set
            {
                this.menuItem.Command = value;
            }
        }

        public Input.IRoutedCommand RoutedCommand
        {
            get => this.menuItem.RoutedCommand;
            set
            {
                if (this.menuItem is IConfigurableCommandData configurableCommandData)
                {
                    configurableCommandData.RoutedCommand = value;
                }
            }
        }

        public object CommandParameter
        {
            get => this.menuItem.CommandParameter;
            set => this.menuItem.CommandParameter = value;
        }

        public int Count => this.children.Count;

        public ObservableCollection<IMenuControlData> Children => this.children;

        public IMenuPosition Position => this.menuItem.Position;

        public MenuControlData(IMenuItem menuItem)
        {
            this.menuItem = menuItem;
            this.menuItem.PropertyChanged += this.MenuItem_PropertyChanged;
            this.children.CollectionChanged += this.Children_CollectionChanged;
        }

        private void MenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e.PropertyName);
        }

        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.CollectionChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (this.PropertyChanged != null)
            {
                foreach (var i in names)
                {
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(i));
                }
            }
        }

        public Input.CommandBinding CreateCommandBinding()
        {
            return this.menuItem.CreateCommandBinding();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public IEnumerator<IMenuControlData> GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.children.GetEnumerator();
        }
    }
}
