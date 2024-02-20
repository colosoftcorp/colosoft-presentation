using System;
using System.ComponentModel;
using System.Reflection;

namespace Colosoft.Presentation.Input
{
    internal class PropertyObserverNode
    {
        private readonly Action action;
        private INotifyPropertyChanged inpcObject;

        public PropertyInfo PropertyInfo { get; }
        public PropertyObserverNode Next { get; set; }

        public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
        {
            this.PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            this.action = () =>
            {
                action?.Invoke();
                if (this.Next == null)
                {
                    return;
                }

                this.Next.UnsubscribeListener();
                this.GenerateNextNode();
            };
        }

        public void SubscribeListenerFor(INotifyPropertyChanged inpcObject)
        {
            this.inpcObject = inpcObject;
            this.inpcObject.PropertyChanged += this.OnPropertyChanged;

            if (this.Next != null)
            {
                this.GenerateNextNode();
            }
        }

        private void GenerateNextNode()
        {
            var nextProperty = this.PropertyInfo.GetValue(this.inpcObject);
            if (nextProperty == null)
            {
                return;
            }

            if (!(nextProperty is INotifyPropertyChanged nextInpcObject))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that " +
                    $"owns '{this.Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");
            }

            this.Next.SubscribeListenerFor(nextInpcObject);
        }

        private void UnsubscribeListener()
        {
            if (this.inpcObject != null)
            {
                this.inpcObject.PropertyChanged -= this.OnPropertyChanged;
            }

            this.Next?.UnsubscribeListener();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == this.PropertyInfo.Name || string.IsNullOrEmpty(e?.PropertyName))
            {
                this.action?.Invoke();
            }
        }
    }
}