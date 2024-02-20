using System;
using System.Linq.Expressions;

namespace Colosoft.Presentation.Input
{
    public class DelegateCommand : DelegateCommandBase
    {
        private readonly Action executeMethod;
        private Func<bool> canExecuteMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base()
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), Properties.Resources.DelegateCommandDelegatesCannotBeNull);
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public void Execute()
        {
            this.executeMethod();
        }

        public bool CanExecute()
        {
            return this.canExecuteMethod == null || this.canExecuteMethod();
        }

        protected override void Execute(object parameter)
        {
            this.Execute();
        }

        protected override bool CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        public DelegateCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            this.ObservesPropertyInternal(propertyExpression);
            return this;
        }

        public DelegateCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            this.canExecuteMethod = canExecuteExpression.Compile();
            this.ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }
}
