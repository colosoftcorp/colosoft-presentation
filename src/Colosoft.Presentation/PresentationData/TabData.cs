using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class TabData : INotifyPropertyChanged, IEnumerable<ControlData>, IControlDataContainer
    {
        private string name;
        private string header;
        private string contextualTabGroupHeader;
        private bool isSelected;
        private ObservableCollection<GroupData> groupDataCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged(nameof(this.Name));
                }
            }
        }

        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                if (this.header != value)
                {
                    this.header = value;
                    this.OnPropertyChanged(nameof(this.Header));
                }
            }
        }

        public string ContextualTabGroupHeader
        {
            get
            {
                return this.contextualTabGroupHeader;
            }

            set
            {
                if (this.contextualTabGroupHeader != value)
                {
                    this.contextualTabGroupHeader = value;
                    this.OnPropertyChanged(nameof(this.ContextualTabGroupHeader));
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged(nameof(this.IsSelected));
                }
            }
        }

        public ObservableCollection<GroupData> GroupDataCollection
        {
            get
            {
                if (this.groupDataCollection == null)
                {
                    this.groupDataCollection = new ObservableCollection<GroupData>();
                }

                return this.groupDataCollection;
            }
        }

        public TabData()
            : this(null)
        {
        }

        public TabData(string header)
        {
            this.Header = header;
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            return this.GroupDataCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GroupDataCollection.GetEnumerator();
        }

        public int Count => this.GroupDataCollection.Count;

        public void Add(object data)
        {
            if (!(data is GroupData))
            {
                throw new InvalidCastException($"data to '{typeof(GroupData).FullName}'");
            }

            this.GroupDataCollection.Add((GroupData)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is GroupData))
            {
                throw new InvalidCastException($"data to '{typeof(GroupData).FullName}'");
            }

            this.GroupDataCollection.Insert(index, (GroupData)data);
        }

        public bool Remove(object data)
        {
            if (!(data is GroupData))
            {
                throw new InvalidCastException($"data to '{typeof(GroupData).FullName}'");
            }

            return this.GroupDataCollection.Remove((GroupData)data);
        }

        public void RemoveAt(int index)
        {
            this.GroupDataCollection.RemoveAt(index);
        }
    }
}
