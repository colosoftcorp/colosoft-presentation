using System;
using System.Collections;
using System.Collections.Generic;

namespace Colosoft.Presentation.Input
{
    public sealed class InputGestureCollection : IList, IList<InputGesture>
    {
        private List<InputGesture> innerGestureList;
        private bool isReadOnly;

        public InputGestureCollection()
        {
        }

        public InputGestureCollection(IList inputGestures)
        {
            if (inputGestures != null && inputGestures.Count > 0)
            {
                this.AddRange(inputGestures as ICollection);
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (this.innerGestureList != null)
            {
                ((ICollection)this.innerGestureList).CopyTo(array, index);
            }
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as InputGesture);
        }

        int IList.IndexOf(object value)
        {
            var inputGesture = value as InputGesture;
            return (inputGesture != null) ? this.IndexOf(inputGesture) : -1;
        }

        void IList.Insert(int index, object value)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            this.Insert(index, value as InputGesture);
        }

        int IList.Add(object value)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            return this.Add(value as InputGesture);
        }

        void IList.Remove(object value)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            this.Remove(value as InputGesture);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                var inputGesture = value as InputGesture;
                if (inputGesture == null)
                {
                    throw new NotSupportedException(Properties.Resources.CollectionOnlyAcceptsInputGestures);
                }

                this[index] = inputGesture;
            }
        }

        public IEnumerator GetEnumerator()
        {
            if (this.innerGestureList != null)
            {
                return this.innerGestureList.GetEnumerator();
            }

            return System.Linq.Enumerable.Empty<InputGesture>().GetEnumerator();
        }

        public InputGesture this[int index]
        {
            get
            {
                return this.innerGestureList != null ? this.innerGestureList[index] : null;
            }
            set
            {
                if (this.IsReadOnly)
                {
                    throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
                }

                this.EnsureList();

                if (this.innerGestureList != null)
                {
                    this.innerGestureList[index] = value;
                }
            }
        }

        public bool IsSynchronized
        {
            get
            {
                if (this.innerGestureList != null)
                {
                    return ((IList)this.innerGestureList).IsSynchronized;
                }

                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.innerGestureList != null ? ((IList)this.innerGestureList).SyncRoot : this;
            }
        }

        public int IndexOf(InputGesture value)
        {
            return (this.innerGestureList != null) ? this.innerGestureList.IndexOf(value) : -1;
        }

        public void RemoveAt(int index)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (this.innerGestureList != null)
            {
                this.innerGestureList.RemoveAt(index);
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return this.IsReadOnly;
            }
        }

        public int Add(InputGesture inputGesture)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (inputGesture == null)
            {
                throw new ArgumentNullException(nameof(inputGesture));
            }

            this.EnsureList();
            this.innerGestureList.Add(inputGesture);
            return 0;
        }

        public void AddRange(ICollection collection)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count > 0)
            {
                if (this.innerGestureList == null)
                {
                    this.innerGestureList = new List<InputGesture>(collection.Count);
                }

                IEnumerator collectionEnum = collection.GetEnumerator();
                while (collectionEnum.MoveNext())
                {
                    InputGesture inputGesture = collectionEnum.Current as InputGesture;
                    if (inputGesture != null)
                    {
                        this.innerGestureList.Add(inputGesture);
                    }
                    else
                    {
                        throw new NotSupportedException(Properties.Resources.CollectionOnlyAcceptsInputGestures);
                    }
                }
            }
        }

        public void Insert(int index, InputGesture inputGesture)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (inputGesture == null)
            {
                throw new NotSupportedException(Properties.Resources.CollectionOnlyAcceptsInputGestures);
            }

            if (this.innerGestureList != null)
            {
                this.innerGestureList.Insert(index, inputGesture);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.isReadOnly;
            }
        }

        public bool Remove(InputGesture inputGesture)
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (inputGesture == null)
            {
                throw new ArgumentNullException(nameof(inputGesture));
            }

            if (this.innerGestureList != null && this.innerGestureList.Contains(inputGesture))
            {
                return this.innerGestureList.Remove(inputGesture);
            }

            return false;
        }

        public int Count
        {
            get
            {
                return this.innerGestureList != null ? this.innerGestureList.Count : 0;
            }
        }

        public void Clear()
        {
            if (this.IsReadOnly)
            {
                throw new NotSupportedException(Properties.Resources.ReadOnlyInputGesturesCollection);
            }

            if (this.innerGestureList != null)
            {
                this.innerGestureList.Clear();
                this.innerGestureList = null;
            }
        }

        public bool Contains(InputGesture key)
        {
            if (this.innerGestureList != null && key != null)
            {
                return this.innerGestureList.Contains(key);
            }

            return false;
        }

        public void CopyTo(InputGesture[] array, int arrayIndex)
        {
            if (this.innerGestureList != null)
            {
                this.innerGestureList.CopyTo(array, arrayIndex);
            }
        }

        public void Seal()
        {
            this.isReadOnly = true;
        }

        private void EnsureList()
        {
            if (this.innerGestureList == null)
            {
                this.innerGestureList = new List<InputGesture>(1);
            }
        }

        internal InputGesture FindMatch(object targetElement, InputEventArgs inputEventArgs)
        {
            for (int i = 0; i < this.Count; i++)
            {
                InputGesture inputGesture = this[i];
                if (inputGesture.Matches(targetElement, inputEventArgs))
                {
                    return inputGesture;
                }
            }

            return null;
        }

        void ICollection<InputGesture>.Add(InputGesture item)
        {
            this.Add(item);
        }

        IEnumerator<InputGesture> IEnumerable<InputGesture>.GetEnumerator()
        {
            if (this.innerGestureList != null)
            {
                return this.innerGestureList.GetEnumerator();
            }

            return System.Linq.Enumerable.Empty<InputGesture>().GetEnumerator();
        }
    }
}
