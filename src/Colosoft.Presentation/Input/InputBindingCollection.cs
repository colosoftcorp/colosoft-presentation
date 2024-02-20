using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Colosoft.Presentation.Input
{
    public sealed class InputBindingCollection : IList, IList<InputBinding>, INotifyCollectionChanged, IDisposable
    {
        private readonly bool isReadOnly = false;
        private ObservableCollection<InputBinding> innerBindingList;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public InputBindingCollection()
        {
        }

        public InputBindingCollection(IList inputBindings)
        {
            if (inputBindings != null && inputBindings.Count > 0)
            {
                this.AddRange(inputBindings);
            }
        }

        ~InputBindingCollection() => this.Dispose();

        private void InnerBindingListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.CollectionChanged?.Invoke(this, e);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (this.innerBindingList != null)
            {
                ((ICollection)this.innerBindingList).CopyTo(array, index);
            }
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as InputBinding);
        }

        int IList.IndexOf(object value)
        {
            var inputBinding = value as InputBinding;
            return (inputBinding != null) ? this.IndexOf(inputBinding) : -1;
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, value as InputBinding);
        }

        int IList.Add(object value)
        {
            this.Add(value as InputBinding);
            return 0;
        }

        void IList.Remove(object value)
        {
            this.Remove(value as InputBinding);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                var inputBinding = value as InputBinding;
                if (inputBinding == null)
                {
                    throw new NotSupportedException("CollectionOnlyAcceptsInputBindings");
                }

                this[index] = inputBinding;
            }
        }

        public InputBinding this[int index]
        {
            get
            {
                if (this.innerBindingList != null)
                {
                    return this.innerBindingList[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                if (this.innerBindingList != null)
                {
                    this.innerBindingList[index] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public int Add(InputBinding inputBinding)
        {
            if (inputBinding != null)
            {
                if (this.innerBindingList == null)
                {
                    this.innerBindingList = new ObservableCollection<InputBinding>();
                    this.innerBindingList.CollectionChanged += this.InnerBindingListCollectionChanged;
                }

                this.innerBindingList.Add(inputBinding);
                return 0;
            }
            else
            {
                throw new NotSupportedException("CollectionOnlyAcceptsInputBindings");
            }
        }

        public bool IsSynchronized
        {
            get
            {
                if (this.innerBindingList != null)
                {
                    return ((IList)this.innerBindingList).IsSynchronized;
                }

                return false;
            }
        }

        public int IndexOf(InputBinding value)
        {
            return (this.innerBindingList != null) ? this.innerBindingList.IndexOf(value) : -1;
        }

        public void AddRange(ICollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count > 0)
            {
                if (this.innerBindingList == null)
                {
                    this.innerBindingList = new ObservableCollection<InputBinding>();
                    this.innerBindingList.CollectionChanged += this.InnerBindingListCollectionChanged;
                }

                IEnumerator collectionEnum = collection.GetEnumerator();
                while (collectionEnum.MoveNext())
                {
                    InputBinding inputBinding = collectionEnum.Current as InputBinding;
                    if (inputBinding != null)
                    {
                        this.innerBindingList.Add(inputBinding);
                    }
                    else
                    {
                        throw new NotSupportedException("CollectionOnlyAcceptsInputBindings");
                    }
                }
            }
        }

        public void Insert(int index, InputBinding inputBinding)
        {
            if (inputBinding == null)
            {
                throw new NotSupportedException("CollectionOnlyAcceptsInputBindings");
            }

            if (this.innerBindingList != null)
            {
                this.innerBindingList.Insert(index, inputBinding);
            }
        }

        public bool Remove(InputBinding inputBinding)
        {
            if (this.innerBindingList != null && inputBinding != null)
            {
                return this.innerBindingList.Remove(inputBinding);
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (this.innerBindingList != null)
            {
                this.innerBindingList.RemoveAt(index);
            }
        }

        public bool IsFixedSize
        {
            get { return this.IsReadOnly; }
        }

        public int Count
        {
            get
            {
                return this.innerBindingList != null ? this.innerBindingList.Count : 0;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public void Clear()
        {
            if (this.innerBindingList != null)
            {
                this.innerBindingList.Clear();
                this.innerBindingList = null;
            }
        }

        public IEnumerator GetEnumerator()
        {
            if (this.innerBindingList != null)
            {
                return this.innerBindingList.GetEnumerator();
            }

            return Enumerable.Empty<InputBinding>().GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
        }

        public bool Contains(InputBinding key)
        {
            if (this.innerBindingList != null && key != null)
            {
                return this.innerBindingList.Contains(key);
            }

            return false;
        }

        public void CopyTo(InputBinding[] array, int arrayIndex)
        {
            if (this.innerBindingList != null)
            {
                this.innerBindingList.CopyTo(array, arrayIndex);
            }
        }

        void ICollection<InputBinding>.Add(InputBinding item)
        {
            this.Add(item);
        }

        IEnumerator<InputBinding> IEnumerable<InputBinding>.GetEnumerator()
        {
            if (this.innerBindingList != null)
            {
                return this.innerBindingList.GetEnumerator();
            }

            return Enumerable.Empty<InputBinding>().GetEnumerator();
        }

        public void Dispose()
        {
            if (this.innerBindingList != null)
            {
                this.innerBindingList.CollectionChanged -= this.InnerBindingListCollectionChanged;
            }

            GC.SuppressFinalize(this);
        }
    }
}
