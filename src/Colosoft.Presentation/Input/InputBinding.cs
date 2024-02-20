using System;
using System.Security;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public class InputBinding : ICommandSource
    {
        internal static readonly object DataLock = new object();

        private InputGesture gesture = null;

        protected InputBinding()
        {
        }

        [SecurityCritical]
        public InputBinding(ICommand command, InputGesture gesture)
        {
            this.Command = command ?? throw new ArgumentNullException(nameof(command));
            this.gesture = gesture ?? throw new ArgumentNullException(nameof(gesture));
        }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public object CommandTarget { get; set; }

        public virtual InputGesture Gesture
        {
            get
            {
                return this.gesture;
            }

            [SecurityCritical]
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                lock (DataLock)
                {
                    this.gesture = value;
                }
            }
        }
    }
}
