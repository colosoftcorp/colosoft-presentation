using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class GalleryData : ControlData, IEnumerable<ControlData>, IControlDataContainer
    {
        private ObservableCollection<GalleryCategoryData> controlDataCollection;
        private GalleryItemData selectedItem;
        private bool canUserFilter;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<GalleryCategoryData> CategoryDataCollection
        {
            get
            {
                if (this.controlDataCollection == null)
                {
                    this.controlDataCollection = new ObservableCollection<GalleryCategoryData>();
                }

                return this.controlDataCollection;
            }
        }

        public GalleryItemData SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged(nameof(this.SelectedItem));
                }
            }
        }

        public bool CanUserFilter
        {
            get
            {
                return this.canUserFilter;
            }

            set
            {
                if (this.canUserFilter != value)
                {
                    this.canUserFilter = value;
                    this.OnPropertyChanged(nameof(this.CanUserFilter));
                }
            }
        }

        public IEnumerator<ControlData> GetEnumerator()
        {
            return this.CategoryDataCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.CategoryDataCollection.GetEnumerator();
        }

        public int Count
        {
            get { return this.CategoryDataCollection.Count; }
        }

        public void Add(object data)
        {
            if (!(data is GalleryCategoryData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData).FullName}'");
            }

            this.CategoryDataCollection.Add((GalleryCategoryData)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is GalleryCategoryData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData).FullName}'");
            }

            this.CategoryDataCollection.Insert(index, (GalleryCategoryData)data);
        }

        public bool Remove(object data)
        {
            if (!(data is GalleryCategoryData))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData).FullName}'");
            }

            return this.CategoryDataCollection.Remove((GalleryCategoryData)data);
        }

        public void RemoveAt(int index)
        {
            this.CategoryDataCollection.RemoveAt(index);
        }
    }
}
