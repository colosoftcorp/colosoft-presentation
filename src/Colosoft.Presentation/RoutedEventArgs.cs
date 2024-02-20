using System;
using System.Collections.Specialized;

namespace Colosoft.Presentation
{
    public class RoutedEventArgs : EventArgs
    {
        private const int HandledIndex = 1;
        private const int UserInitiatedIndex = 2;
        private const int InvokingHandlerIndex = 4;

        private RoutedEvent routedEvent;
        private object source;
        private object originalSource;

        private BitVector32 flags = new BitVector32(0);

        public RoutedEventArgs()
        {
        }

        public RoutedEventArgs(RoutedEvent routedEvent)
            : this(routedEvent, null)
        {
        }

        public RoutedEventArgs(RoutedEvent routedEvent, object source)
        {
            this.routedEvent = routedEvent;
            this.source = this.originalSource = source;
        }

        private bool InvokingHandler
        {
            get
            {
                return this.flags[InvokingHandlerIndex];
            }
            set
            {
                this.flags[InvokingHandlerIndex] = value;
            }
        }

        public RoutedEvent RoutedEvent
        {
            get { return this.routedEvent; }
            set
            {
                if (this.UserInitiated && this.InvokingHandler)
                {
                    throw new InvalidOperationException(Properties.Resources.RoutedEventCannotChangeWhileRouting);
                }

                this.routedEvent = value;
            }
        }

        internal void OverrideRoutedEvent(RoutedEvent newRoutedEvent)
        {
            this.routedEvent = newRoutedEvent;
        }

        public bool Handled
        {
            get
            {
                return this.flags[HandledIndex];
            }

            set
            {
                if (this.routedEvent == null)
                {
                    throw new InvalidOperationException(Properties.Resources.RoutedEventArgsMustHaveRoutedEvent);
                }

                this.flags[HandledIndex] = value;
            }
        }

        public object Source
        {
            get { return this.source; }
            set
            {
                if (this.InvokingHandler && this.UserInitiated)
                {
                    throw new InvalidOperationException(Properties.Resources.RoutedEventCannotChangeWhileRouting);
                }

                if (this.routedEvent == null)
                {
                    throw new InvalidOperationException(Properties.Resources.RoutedEventArgsMustHaveRoutedEvent);
                }

                object source1 = value;
                if (this.source == null && this.originalSource == null)
                {
                    this.source = this.originalSource = source1;
                    this.OnSetSource(source1);
                }
                else if (this.source != source1)
                {
                    this.source = source1;
                    this.OnSetSource(source1);
                }
            }
        }

        internal void OverrideSource(object source)
        {
            this.source = source;
        }

        public object OriginalSource
        {
            get { return this.originalSource; }
        }

        protected virtual void OnSetSource(object source)
        {
        }

        protected virtual void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            if (genericHandler == null)
            {
                throw new ArgumentNullException(nameof(genericHandler));
            }

            if (genericTarget == null)
            {
                throw new ArgumentNullException(nameof(genericTarget));
            }

            if (this.routedEvent == null)
            {
                throw new InvalidOperationException(Properties.Resources.RoutedEventArgsMustHaveRoutedEvent);
            }

            this.InvokingHandler = true;
            try
            {
                if (genericHandler is RoutedEventHandler routedEventHandler)
                {
                    routedEventHandler(genericTarget, this);
                }
                else
                {
                    genericHandler.DynamicInvoke(new[] { genericTarget, this });
                }
            }
            finally
            {
                this.InvokingHandler = false;
            }
        }

        internal void InvokeHandler(Delegate handler, object target)
        {
            this.InvokingHandler = true;

            try
            {
                this.InvokeEventHandler(handler, target);
            }
            finally
            {
                this.InvokingHandler = false;
            }
        }

        internal bool UserInitiated
        {
            get
            {
                if (this.flags[UserInitiatedIndex])
                {
                    return true;
                }

                return false;
            }
        }

        internal void MarkAsUserInitiated()
        {
            this.flags[UserInitiatedIndex] = true;
        }

        internal void ClearUserInitiated()
        {
            this.flags[UserInitiatedIndex] = false;
        }
    }
}
