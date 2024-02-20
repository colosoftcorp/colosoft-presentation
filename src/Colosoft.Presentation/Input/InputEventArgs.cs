using System;

namespace Colosoft.Presentation.Input
{
    public class InputEventArgs : RoutedEventArgs
    {
        private static int timestamp;

        public InputEventArgs(InputDevice inputDevice, int timestamp)
            : base(new RoutedEvent("Input", RoutingStrategy.Direct, null, null))
        {
            this.Device = inputDevice;
#pragma warning disable S3010 // Static fields should not be updated in constructors
            InputEventArgs.timestamp = timestamp;
#pragma warning restore S3010 // Static fields should not be updated in constructors
        }

        public InputDevice Device { get; set; }

        public int Timestamp
        {
            get { return timestamp; }
        }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            if (genericHandler is null)
            {
                throw new ArgumentNullException(nameof(genericHandler));
            }

            var handler = (InputEventHandler)genericHandler;
            handler(genericTarget, this);
        }
    }
}