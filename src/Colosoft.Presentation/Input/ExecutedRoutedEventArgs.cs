using System;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public sealed class ExecutedRoutedEventArgs : RoutedEventArgs
    {
        internal ExecutedRoutedEventArgs(ICommand command, object parameter)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            this.Command = command;
            this.Parameter = parameter;
        }

        public ICommand Command { get; }

        public object Parameter { get; }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            var handler = (ExecutedRoutedEventHandler)genericHandler;
            handler(genericTarget, this);
        }
    }
}
