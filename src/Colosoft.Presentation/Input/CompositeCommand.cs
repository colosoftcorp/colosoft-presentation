using Colosoft.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public class CompositeCommand : ICommand
    {
        private readonly List<ICommand> registeredCommands = new List<ICommand>();
        private readonly bool monitorCommandActivity;
        private readonly EventHandler onRegisteredCommandCanExecuteChangedHandler;
        private readonly SynchronizationContext synchronizationContext;

        public event EventHandler CanExecuteChanged;

        public CompositeCommand()
        {
            this.onRegisteredCommandCanExecuteChangedHandler = new EventHandler(this.OnRegisteredCommandCanExecuteChanged);
            this.synchronizationContext = SynchronizationContext.Current;
        }

        public CompositeCommand(bool monitorCommandActivity)
            : this()
        {
            this.monitorCommandActivity = monitorCommandActivity;
        }

        public virtual void RegisterCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (command == this)
            {
                throw new ArgumentException(Resources.CannotRegisterCompositeCommandInItself);
            }

            lock (this.registeredCommands)
            {
                if (this.registeredCommands.Contains(command))
                {
                    throw new InvalidOperationException(Resources.CannotRegisterSameCommandTwice);
                }

                this.registeredCommands.Add(command);
            }

            command.CanExecuteChanged += this.onRegisteredCommandCanExecuteChangedHandler;
            this.OnCanExecuteChanged();

            if (this.monitorCommandActivity)
            {
                var activeAwareCommand = command as IActiveAware;
                if (activeAwareCommand != null)
                {
                    activeAwareCommand.IsActiveChanged += this.Command_IsActiveChanged;
                }
            }
        }

        public virtual void UnregisterCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            bool removed;
            lock (this.registeredCommands)
            {
                removed = this.registeredCommands.Remove(command);
            }

            if (removed)
            {
                command.CanExecuteChanged -= this.onRegisteredCommandCanExecuteChangedHandler;
                this.OnCanExecuteChanged();

                if (this.monitorCommandActivity)
                {
                    var activeAwareCommand = command as IActiveAware;
                    if (activeAwareCommand != null)
                    {
                        activeAwareCommand.IsActiveChanged -= this.Command_IsActiveChanged;
                    }
                }
            }
        }

        private void OnRegisteredCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.OnCanExecuteChanged();
        }

        public virtual bool CanExecute(object parameter)
        {
            bool hasEnabledCommandsThatShouldBeExecuted = false;

            ICommand[] commandList;
            lock (this.registeredCommands)
            {
                commandList = this.registeredCommands.ToArray();
            }

            foreach (var command in commandList.Where(command => this.ShouldExecute(command)))
            {
                if (!command.CanExecute(parameter))
                {
                    return false;
                }

                hasEnabledCommandsThatShouldBeExecuted = true;
            }

            return hasEnabledCommandsThatShouldBeExecuted;
        }

        public virtual void Execute(object parameter)
        {
            Queue<ICommand> commands;
            lock (this.registeredCommands)
            {
                commands = new Queue<ICommand>(this.registeredCommands.Where(this.ShouldExecute).ToList());
            }

            while (commands.Count > 0)
            {
                ICommand command = commands.Dequeue();
                command.Execute(parameter);
            }
        }

        protected virtual bool ShouldExecute(ICommand command)
        {
            var activeAwareCommand = command as IActiveAware;

            if (this.monitorCommandActivity && activeAwareCommand != null)
            {
                return activeAwareCommand.IsActive;
            }

            return true;
        }

        public IList<ICommand> RegisteredCommands
        {
            get
            {
                IList<ICommand> commandList;
                lock (this.registeredCommands)
                {
                    commandList = this.registeredCommands.ToList();
                }

                return commandList;
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                if (this.synchronizationContext != null && this.synchronizationContext != SynchronizationContext.Current)
                {
                    this.synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                }
                else
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void Command_IsActiveChanged(object sender, EventArgs e)
        {
            this.OnCanExecuteChanged();
        }
    }
}
