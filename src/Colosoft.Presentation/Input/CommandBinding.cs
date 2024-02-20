using System;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public class CommandBinding
    {
        private ICommand command;

        public event ExecutedRoutedEventHandler PreviewExecuted;

        public event ExecutedRoutedEventHandler Executed;

        public event CanExecuteRoutedEventHandler PreviewCanExecute;

        public event CanExecuteRoutedEventHandler CanExecute;

        public CommandBinding()
        {
        }

        public CommandBinding(ICommand command)
            : this(command, null, null)
        {
        }

        public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed)
            : this(command, executed, null)
        {
        }

        public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));

            if (executed != null)
            {
                this.Executed += executed;
            }

            if (canExecute != null)
            {
                this.CanExecute += canExecute;
            }
        }

        public ICommand Command
        {
            get => this.command;
            set => this.command = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

            if (e.RoutedEvent == CommandManager.CanExecuteEvent)
            {
                if (this.CanExecute is null)
                {
                    if (e.CanExecute)
                    {
                        return;
                    }

                    if (this.Executed is null)
                    {
                        return;
                    }

                    e.CanExecute = true;
                    e.Handled = true;
                }
                else
                {
                    this.CanExecute(sender, e);
                    if (e.CanExecute)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                if (this.PreviewCanExecute is null)
                {
                    return;
                }

                this.PreviewCanExecute(sender, e);

                if (e.CanExecute)
                {
                    e.Handled = true;
                }
            }
        }

        private bool CheckCanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var canExecuteArgs = new CanExecuteRoutedEventArgs(e.Command, e.Parameter)
            {
                RoutedEvent = CommandManager.CanExecuteEvent,
                Source = e.OriginalSource,
            };

            canExecuteArgs.OverrideSource(e.Source);

            this.OnCanExecute(sender, canExecuteArgs);

            return canExecuteArgs.CanExecute;
        }

        internal void OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

            if (e.RoutedEvent == CommandManager.ExecutedEvent)
            {
                if (this.Executed is null)
                {
                    return;
                }

                if (!this.CheckCanExecute(sender, e))
                {
                    return;
                }

                this.Executed(sender, e);
                e.Handled = true;
            }
            else
            {
                if (this.PreviewExecuted is null)
                {
                    return;
                }

                if (!this.CheckCanExecute(sender, e))
                {
                    return;
                }

                this.PreviewExecuted(sender, e);
                e.Handled = true;
            }
        }
    }
}
