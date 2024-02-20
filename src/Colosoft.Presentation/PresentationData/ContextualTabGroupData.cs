using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class ContextualTabGroupData : INotifyPropertyChanged, IEnumerable<ControlData>, IControlDataContainer
    {
        private string header;
        private bool isVisible;
        private ObservableCollection<TabData> tabDataCollection;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.OnPropertyChanged(nameof(this.IsVisible));
                }
            }
        }

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                if (this.tabDataCollection == null)
                {
                    this.tabDataCollection = new ObservableCollection<TabData>();
                }

                return this.tabDataCollection;
            }
        }

        public ContextualTabGroupData()
            : this(null)
        {
        }

        public ContextualTabGroupData(string header)
        {
            this.Header = header;
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            foreach (var i in this.TabDataCollection)
            {
                foreach (var j in i)
                {
                    yield return j;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var i in this.TabDataCollection)
            {
                foreach (var j in i)
                {
                    yield return j;
                }
            }
        }

        public int Count
        {
            get { return this.TabDataCollection.Count; }
        }

        public void Add(object data)
        {
            if (!(data is TabData))
            {
                throw new InvalidCastException($"data to '{typeof(TabData).FullName}'");
            }

            this.TabDataCollection.Add((TabData)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is TabData))
            {
                throw new InvalidCastException($"data to '{typeof(TabData).FullName}'");
            }

            this.TabDataCollection.Insert(index, (TabData)data);
        }

        public bool Remove(object data)
        {
            if (!(data is TabData))
            {
                throw new InvalidCastException($"data to '{typeof(TabData).FullName}'");
            }

            return this.TabDataCollection.Remove((TabData)data);
        }

        public void RemoveAt(int index)
        {
            this.TabDataCollection.RemoveAt(index);
        }
    }
}
