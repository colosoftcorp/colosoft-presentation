using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Colosoft.Presentation.PresentationData
{
    public class MenuButtonData : ControlData, IEnumerable<ControlData>, IControlDataContainer
    {
        private bool isVerticallyResizable;
        private bool isHorizontallyResizable;
        private ObservableCollection<ControlData> controlDataCollection;

        public bool IsApplicationMenu { get; }

        public bool IsVerticallyResizable
        {
            get
            {
                return this.isVerticallyResizable;
            }

            set
            {
                if (this.isVerticallyResizable != value)
                {
                    this.isVerticallyResizable = value;
                    this.OnPropertyChanged(nameof(this.IsVerticallyResizable));
                }
            }
        }

        public bool IsHorizontallyResizable
        {
            get
            {
                return this.isHorizontallyResizable;
            }

            set
            {
                if (this.isHorizontallyResizable != value)
                {
                    this.isHorizontallyResizable = value;
                    this.OnPropertyChanged(nameof(this.IsHorizontallyResizable));
                }
            }
        }

        public ObservableCollection<ControlData> ControlDataCollection
        {
            get
            {
                if (this.controlDataCollection == null)
                {
                    this.controlDataCollection = new ObservableCollection<ControlData>();
                }

                return this.controlDataCollection;
            }
        }

        public MenuButtonData()
            : this(false)
        {
        }

        public MenuButtonData(bool isApplicationMenu)
        {
            this.IsApplicationMenu = isApplicationMenu;
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            return this.ControlDataCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.ControlDataCollection.GetEnumerator();
        }

        public int Count
        {
            get { return this.ControlDataCollection.Count; }
        }

        public void Add(object data)
        {
            if (!(data is ControlData))
            {
                throw new InvalidCastException($"data to '{typeof(ControlData).FullName}'");
            }

            this.ControlDataCollection.Add((ControlData)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is ControlData))
            {
                throw new InvalidCastException($"data to '{typeof(ControlData).FullName}'");
            }

            this.ControlDataCollection.Insert(index, (ControlData)data);
        }

        public bool Remove(object data)
        {
            if (!(data is ControlData))
            {
                throw new InvalidCastException($"data to '{typeof(ControlData).FullName}'");
            }

            return this.ControlDataCollection.Remove((ControlData)data);
        }

        public void RemoveAt(int index)
        {
            this.ControlDataCollection.RemoveAt(index);
        }
    }
}
