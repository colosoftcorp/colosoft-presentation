using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class RibbonData : INotifyPropertyChanged, IEnumerable<ControlData>, IControlDataContainer
    {
        private ObservableCollection<TabData> tabDataCollection;
        private ObservableCollection<ContextualTabGroupData> contextualTabGroupDataCollection;
        private MenuButtonData applicationMenuData;

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

        public ObservableCollection<ContextualTabGroupData> ContextualTabGroupDataCollection
        {
            get
            {
                if (this.contextualTabGroupDataCollection == null)
                {
                    this.contextualTabGroupDataCollection = new ObservableCollection<ContextualTabGroupData>();
                }

                return this.contextualTabGroupDataCollection;
            }
        }

        public MenuButtonData ApplicationMenuData
        {
            get
            {
                if (this.applicationMenuData == null)
                {
                    this.applicationMenuData = new MenuButtonData(true)
                    {
                        Label = "AppMenu ".GetFormatter(),
                        ToolTipTitle = "ToolTip Title".GetFormatter(),
                        ToolTipDescription = "ToolTip Description".GetFormatter(),
                    };
                }

                return this.applicationMenuData;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
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

            foreach (var i in this.ApplicationMenuData)
            {
                yield return i;
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

            foreach (var i in this.ApplicationMenuData)
            {
                yield return i;
            }
        }

        public int Count
        {
            get { return this.TabDataCollection.Count + this.ApplicationMenuData.Count; }
        }

        public void Add(object data)
        {
            if (data is TabData data1)
            {
                this.TabDataCollection.Add(data1);
            }
            else if (data is ControlData data2)
            {
                this.ApplicationMenuData.Add(data2);
            }
        }

        public void Insert(int index, object data)
        {
            if (data is TabData data1)
            {
                this.TabDataCollection.Insert(index, data1);
            }
            else if (data is ControlData data2)
            {
                this.ApplicationMenuData.Insert(index, data2);
            }
        }

        public bool Remove(object data)
        {
            if (data is TabData data1)
            {
                return this.TabDataCollection.Remove(data1);
            }
            else if (data is ControlData data2)
            {
                return this.ApplicationMenuData.Remove(data2);
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
    }
}
