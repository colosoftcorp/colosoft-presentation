using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class GroupData : ControlData, IEnumerable<ControlData>, IControlDataContainer
    {
        private ObservableCollection<ControlData> controlDataCollection;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

        public GroupData(IMessageFormattable header)
        {
            this.Label = header;
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
