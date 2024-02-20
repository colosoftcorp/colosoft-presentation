using System;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public sealed class CanExecuteRoutedEventArgs : RoutedEventArgs
    {
        internal CanExecuteRoutedEventArgs(ICommand command, object parameter)
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

        public bool CanExecute { get; set; }

        public bool ContinueRouting { get; set; }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            var handler = (CanExecuteRoutedEventHandler)genericHandler;
            handler(genericTarget, this);
        }
    }
}
