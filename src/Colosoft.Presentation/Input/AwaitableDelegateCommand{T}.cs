using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public class AwaitableDelegateCommand<T> : IAsyncCommand<T>, ICommand<T>, IDisposable
    {
        private readonly Func<T, CancellationToken, Task> executeMethod;
        private readonly DelegateCommand<T> underlyingCommand;
        private bool isExecuting;
        private CancellationTokenSource currentCancellationTokenSource;

        public event EventHandler CanExecuteChanged
        {
            add => this.underlyingCommand.CanExecuteChanged += value;
            remove => this.underlyingCommand.CanExecuteChanged -= value;
        }

        public ICommand Command => this;

        public bool UseNewThread { get; set; }

        public AwaitableDelegateCommand(Func<T, CancellationToken, Task> executeMethod)
            : this(executeMethod, _ => true)
        {
        }

        public AwaitableDelegateCommand(Func<T, CancellationToken, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.underlyingCommand = new DelegateCommand<T>(x => { }, canExecuteMethod);
        }

        ~AwaitableDelegateCommand() => this.Dispose(false);

        private async Task InternalExecuteAsync(T parameter, CancellationToken cancellationToken)
        {
            try
            {
                this.isExecuting = true;
                this.NotifyCanExecuteChanged();
                await this.executeMethod(parameter, cancellationToken);
            }
            finally
            {
                this.isExecuting = false;
                this.NotifyCanExecuteChanged();
            }
        }

        public async Task ExecuteAsync(T parameter, CancellationToken cancellationToken)
        {
            if (this.UseNewThread)
            {
                _ = Task.Factory.StartNew(
                    async () => await this.InternalExecuteAsync(parameter, cancellationToken),
                    cancellationToken,
                    TaskCreationOptions.RunContinuationsAsynchronously,
                    TaskScheduler.Current);
            }
            else
            {
                await this.InternalExecuteAsync(parameter, cancellationToken);
            }
        }

        public void Cancel()
        {
            if (this.currentCancellationTokenSource != null)
            {
                this.currentCancellationTokenSource.Cancel();
            }
        }

        public bool CanExecute(object parameter)
        {
            T reference;

            if (!ReferenceEquals(parameter, null) && parameter is T)
            {
                reference = (T)parameter;
            }
            else
            {
                reference = default;
            }

            return !this.isExecuting && this.underlyingCommand.CanExecute(reference);
        }

        public async void Execute(object parameter)
        {
            if (this.UseNewThread)
            {
                this.currentCancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = this.currentCancellationTokenSource.Token;

                _ = Task.Factory.StartNew(
                    async () =>
                    {
                        try
                        {
                            await this.InternalExecuteAsync((T)parameter, cancellationToken);
                        }
                        finally
                        {
                            this.currentCancellationTokenSource = null;
                        }
                    },
                    cancellationToken,
                    TaskCreationOptions.RunContinuationsAsynchronously,
                    TaskScheduler.Current);
            }
            else
            {
                await this.InternalExecuteAsync((T)parameter, default);
            }
        }

        public void NotifyCanExecuteChanged()
        {
            this.underlyingCommand.NotifyCanExecuteChanged();
        }

        public bool CanExecute(T parameter) => ((ICommand)this).CanExecute(parameter);

        public void Execute(T parameter) => ((ICommand)this).Execute(parameter);

        protected virtual void Dispose(bool disposing)
        {
            if (this.currentCancellationTokenSource != null)
            {
                try
                {
                    this.currentCancellationTokenSource.Dispose();
                }
                catch
                {
                    // ignore
                }

                this.currentCancellationTokenSource = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
