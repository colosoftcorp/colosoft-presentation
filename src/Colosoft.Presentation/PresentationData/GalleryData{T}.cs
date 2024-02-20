using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    public class GalleryData<T> : ControlData, IEnumerable<ControlData>, IControlDataContainer
    {
        private ObservableCollection<GalleryCategoryData<T>> controlDataCollection;
        private T selectedItem;
        private bool canUserFilter;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<GalleryCategoryData<T>> CategoryDataCollection
        {
            get
            {
                if (this.controlDataCollection == null)
                {
                    this.controlDataCollection = new ObservableCollection<GalleryCategoryData<T>>();
                }

                return this.controlDataCollection;
            }
        }

        public T SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (!object.Equals(value, this.selectedItem))
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
            if (!(data is GalleryCategoryData<T>))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData<T>).FullName}'");
            }

            this.CategoryDataCollection.Add((GalleryCategoryData<T>)data);
        }

        public void Insert(int index, object data)
        {
            if (!(data is GalleryCategoryData<T>))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData<T>).FullName}'");
            }

            this.CategoryDataCollection.Insert(index, (GalleryCategoryData<T>)data);
        }

        public bool Remove(object data)
        {
            if (!(data is GalleryCategoryData<T>))
            {
                throw new InvalidCastException($"data to '{typeof(GalleryCategoryData<T>).FullName}'");
            }

            return this.CategoryDataCollection.Remove((GalleryCategoryData<T>)data);
        }

        public void RemoveAt(int index)
        {
            this.CategoryDataCollection.RemoveAt(index);
        }
    }
}
