using ReactiveUI;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public static class ViewModelCommandExtensions
    {
        public static ReactiveCommand<Unit, TResult> CreateLoadingCommandFromTaskForgetMessage<TResult>(
            this IViewModel viewModel,
            Func<CancellationToken, Task<TResult>> execute,
            IObservable<bool> canExecute = null,
            System.Reactive.Concurrency.IScheduler outputScheduler = null,
            Func<CancellationToken, Task> cancelCallback = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return ReactiveCommand.CreateFromTask(
                async (cancellationToken) =>
                {
                    using (viewModel.StateMonitor.EnterForgetMessage(cancelCallback))
                    {
                        return await execute(cancellationToken);
                    }
                },
                canExecute,
                outputScheduler);
        }

        public static ReactiveCommand<Unit, TResult> CreateLoadingCommandFromTask<TResult>(
           this IViewModel viewModel,
           Func<CancellationToken, Task<TResult>> execute,
           IObservable<bool> canExecute = null,
           string message = null,
           System.Reactive.Concurrency.IScheduler outputScheduler = null,
           Func<CancellationToken, Task> cancelCallback = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return ReactiveCommand.CreateFromTask(
                async (cancellationToken) =>
                {
                    using (viewModel.StateMonitor.Enter(message, cancelCallback))
                    {
                        return await execute(cancellationToken);
                    }
                },
                canExecute,
                outputScheduler);
        }

        public static ReactiveCommand<Unit, Unit> CreateLoadingCommandFromTask(
            this IViewModel viewModel,
            Func<CancellationToken, Task> execute,
            IObservable<bool> canExecute = null,
            string message = null,
            System.Reactive.Concurrency.IScheduler outputScheduler = null,
            Func<CancellationToken, Task> cancelCallback = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return ReactiveCommand.CreateFromTask(
                async (cancellationToken) =>
                {
                    using (viewModel.StateMonitor.Enter(message, cancelCallback))
                    {
                        await execute(cancellationToken);
                    }
                },
                canExecute,
                outputScheduler);
        }

        public static ReactiveCommand<TParam, Unit> CreateLoadingCommandFromTask<TParam>(
            this IViewModel viewModel,
            Func<TParam, CancellationToken, Task> execute,
            IObservable<bool> canExecute = null,
            string message = null,
            System.Reactive.Concurrency.IScheduler outputScheduler = null,
            Func<CancellationToken, Task> cancelCallback = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return ReactiveCommand.CreateFromTask<TParam>(
                async (parameter, cancellationToken) =>
                {
                    using (viewModel.StateMonitor.Enter(message, cancelCallback))
                    {
                        await execute(parameter, cancellationToken);
                    }
                },
                canExecute,
                outputScheduler);
        }

        public static ReactiveCommand<TParam, TResult> CreateLoadingCommandFromTask<TParam, TResult>(
           this IViewModel viewModel,
           Func<TParam, CancellationToken, Task<TResult>> execute,
           IObservable<bool> canExecute = null,
           string message = null,
           System.Reactive.Concurrency.IScheduler outputScheduler = null,
           Func<CancellationToken, Task> cancelCallback = null)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return ReactiveCommand.CreateFromTask<TParam, TResult>(
                async (parameter, cancellationToken) =>
                {
                    using (viewModel.StateMonitor.Enter(message, cancelCallback))
                    {
                        return await execute(parameter, cancellationToken);
                    }
                },
                canExecute,
                outputScheduler);
        }
    }
}