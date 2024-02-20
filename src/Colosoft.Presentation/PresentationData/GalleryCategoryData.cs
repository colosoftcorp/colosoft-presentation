using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class GalleryCategoryData : ControlData, IEnumerable<ControlData>, IControlDataContainer
    {
        private ObservableCollection<GalleryItemData> controlDataCollection;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<GalleryItemData> GalleryItemDataCollection
        {
            get
            {
                if (this.controlDataCollection == null)
                {
                    this.controlDataCollection = new ObservableCollection<GalleryItemData>();
                }

                return this.controlDataCollection;
            }
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            return this.GalleryItemDataCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GalleryItemDataCollection.GetEnumerator();
        }

        public int Count
        {
            get { return this.GalleryItemDataCollection.Count; }
        }

        public void Add(object data)
        {
            if (!(data is GalleryItemData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryItemData).FullName}'");
            }

            this.GalleryItemDataCollection.Add((GalleryItemData)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is GalleryItemData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryItemData).FullName}'");
            }

            this.GalleryItemDataCollection.Insert(index, (GalleryItemData)data);
        }

        public bool Remove(object data)
        {
            if (!(data is GalleryItemData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryItemData).FullName}'");
            }

            return this.GalleryItemDataCollection.Remove((GalleryItemData)data);
        }

        public void RemoveAt(int index)
        {
            this.GalleryItemDataCollection.RemoveAt(index);
        }
    }
}
