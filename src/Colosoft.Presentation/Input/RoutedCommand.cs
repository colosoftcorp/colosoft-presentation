using System;

namespace Colosoft.Presentation.Input
{
    public abstract class RoutedCommand : IRoutedCommand
    {
        public event EventHandler CanExecuteChanged;

        protected RoutedCommand()
        {
            this.Name = string.Empty;
            this.OwnerType = null;
            this.InputGestures = null;
        }

        protected RoutedCommand(string name, Type ownerType)
            : this(name, ownerType, null)
        {
        }

        protected RoutedCommand(string name, Type ownerType, InputGestureCollection inputGestures)
        {
            this.Name = name ?? string.Empty;
            this.OwnerType = ownerType;
            this.InputGestures = inputGestures;
        }

        public InputGestureCollection InputGestures { get; private set; }

        public string Name { get; }

        public Type OwnerType { get; }

        protected void NotifyCanExecuteChanged() =>
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(CommandContext commandContext)
        {
            return this.CanExecute(commandContext?.CommandParameter);
        }

        public virtual void Execute(CommandContext commandContext)
        {
            this.Execute(commandContext?.CommandParameter);
        }
    }
}
