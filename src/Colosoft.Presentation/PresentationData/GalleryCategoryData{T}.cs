using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class GalleryCategoryData<T> : ControlData, IEnumerable<ControlData>
    {
        private ObservableCollection<T> controlDataCollection;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<T> GalleryItemDataCollection
        {
            get
            {
                if (this.controlDataCollection == null)
                {
                    this.controlDataCollection = new ObservableCollection<T>();
                }

                return this.controlDataCollection;
            }
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            foreach (var i in this.GalleryItemDataCollection)
            {
                if (i is ControlData)
                {
                    yield return i as ControlData;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var i in this.GalleryItemDataCollection)
            {
                if (i is ControlData)
                {
                    yield return i as ControlData;
                }
            }
        }

        public int Count
        {
            get { return this.GalleryItemDataCollection.Count; }
        }

        public void Add(object data)
        {
            if (!(data is T))
            {
                throw new InvalidCastException($"data to '{typeof(T).FullName}'");
            }

            this.GalleryItemDataCollection.Add((T)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is T))
            {
                throw new InvalidCastException($"data to '{typeof(T).FullName}'");
            }

            this.GalleryItemDataCollection.Insert(index, (T)data);
        }

        public bool Remove(object data)
        {
            if (!(data is T))
            {
                throw new InvalidCastException($"data to '{typeof(T).FullName}'");
            }

            return this.GalleryItemDataCollection.Remove((T)data);
        }

        public void RemoveAt(int index)
        {
            this.GalleryItemDataCollection.RemoveAt(index);
        }
    }
}
