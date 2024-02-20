using System;

namespace Colosoft.Presentation.Input
{
    public abstract class Command : RoutedUICommand
    {
        public CommandDescription Description { get; }

        public object Target { get; set; }

        public bool IsExecuting { get; private set; }

        public virtual bool HasRequirements
        {
            get { return true; }
        }

        public virtual bool HasImage
        {
            get { return true; }
        }

        public virtual Uri ImageUri
        {
            get { return null; }
        }

        protected Command(string name, Type ownerType, CommandDescription description)
            : this(
                  (description?.Text ?? Guid.NewGuid().ToString().GetFormatter()).Format(System.Globalization.CultureInfo.CurrentUICulture),
                  name ?? Guid.NewGuid().ToString("N"),
                  ownerType,
                  description)
        {
        }

        protected Command(string text, string name, Type ownerType, CommandDescription description)
            : base(text, name, ownerType, description?.Gestures)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Description = description;
        }

        public CommandContext CreateCanExecuteContext(object bindingSource, object commandSource, object commandParameter)
        {
            return this.OnCreateCanExecuteContext(bindingSource, commandSource, commandParameter);
        }

        public override bool CanExecute(object parameter)
        {
            var context = this.CreateExecuteContext(null, null, parameter);
            return this.CanExecute(context);
        }

        public override bool CanExecute(CommandContext commandContext)
        {
            return this.OnCanExecute(commandContext);
        }

        public CommandContext CreateExecuteContext(object bindingSource, object commandSource, object commandParameter)
        {
            return this.OnCreateExecuteContext(bindingSource, commandSource, commandParameter);
        }

        public override void Execute(object parameter)
        {
            var context = this.CreateExecuteContext(null, null, parameter);
            this.Execute(context);
        }

        public override void Execute(CommandContext commandContext)
        {
            if (this.IsExecuting)
            {
                throw new InvalidOperationException();
            }

            this.IsExecuting = true;

#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                this.OnExecute(commandContext);
            }
            catch
            {
                // ignore
            }
            finally
            {
                this.IsExecuting = false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        protected virtual CommandContext OnCreateCanExecuteContext(object bindingSource, object commandSource, object commandParameter)
        {
            return this.OnCreateContext(bindingSource, commandSource, commandParameter);
        }

        protected virtual bool OnCanExecute(CommandContext commandContext)
        {
            return false;
        }

        protected virtual CommandContext OnCreateExecuteContext(object bindingSource, object commandSource, object commandParameter)
        {
            return this.OnCreateContext(bindingSource, commandSource, commandParameter);
        }

        protected virtual CommandContext OnCreateContext(object bindingSource, object commandSource, object commandParameter)
        {
            return new CommandContext(bindingSource, commandSource, commandParameter);
        }

        protected abstract void OnExecute(CommandContext commandContext);
    }
}
