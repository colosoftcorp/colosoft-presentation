using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public abstract class DelegateCommandBase : ICommand
    {
        private readonly SynchronizationContext synchronizationContext;
        private readonly HashSet<string> observedPropertiesExpressions = new HashSet<string>();

        private bool isActive;

        public event EventHandler CanExecuteChanged;
        public event EventHandler IsActiveChanged;

        protected DelegateCommandBase()
        {
            this.synchronizationContext = SynchronizationContext.Current;
        }

        public bool IsActive
        {
            get => this.isActive;
            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.OnIsActiveChanged();
                }
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

        public void NotifyCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute(parameter);
        }

        protected abstract void Execute(object parameter);

        protected abstract bool CanExecute(object parameter);

        protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (this.observedPropertiesExpressions.Contains(propertyExpression.ToString()))
            {
                throw new ArgumentException(
                    $"{propertyExpression} is already being observed.",
                    nameof(propertyExpression));
            }
            else
            {
                this.observedPropertiesExpressions.Add(propertyExpression.ToString());
                PropertyObserver.Observes(propertyExpression, this.NotifyCanExecuteChanged);
            }
        }

        protected virtual void OnIsActiveChanged()
        {
            this.IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
