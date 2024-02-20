using System;

namespace Colosoft.Presentation
{
    public sealed class RoutedEvent
    {
        private readonly string name;
        private readonly RoutingStrategy routingStrategy;
        private readonly Type handlerType;
        private readonly Type ownerType;

        private readonly int globalIndex;

        public RoutedEvent AddOwner(Type ownerType)
        {
            GlobalEventManager.AddOwner(this, ownerType);
            return this;
        }

        public string Name
        {
            get { return this.name; }
        }

        public RoutingStrategy RoutingStrategy
        {
            get { return this.routingStrategy; }
        }

        public Type HandlerType
        {
            get { return this.handlerType; }
        }

        internal bool IsLegalHandler(Delegate handler)
        {
            var handlerType1 = handler.GetType();

            return (handlerType1 == this.HandlerType) ||
                     (handlerType1 == typeof(RoutedEventHandler));
        }

        public Type OwnerType => this.ownerType;

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}", this.ownerType.Name, this.name);
        }

        internal RoutedEvent(
            string name,
            RoutingStrategy routingStrategy,
            Type handlerType,
            Type ownerType)
        {
            this.name = name;
            this.routingStrategy = routingStrategy;
            this.handlerType = handlerType;
            this.ownerType = ownerType;

            this.globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
        }

        internal int GlobalIndex
        {
            get { return this.globalIndex; }
        }
    }
}