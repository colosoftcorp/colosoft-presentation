using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Input
{
#pragma warning disable CS1956 // Member implements interface member with multiple matches at run-time
    public class AwaitableDelegateCommand : AwaitableDelegateCommand<object>, IAsyncCommand
#pragma warning restore CS1956 // Member implements interface member with multiple matches at run-time
    {
        public AwaitableDelegateCommand(Func<CancellationToken, Task> executeMethod)
            : base((parameter, cancellationToken) => executeMethod(cancellationToken))
        {
        }

        public AwaitableDelegateCommand(Func<CancellationToken, Task> executeMethod, Func<bool> canExecuteMethod)
            : base((parameter, cancellationToken) => executeMethod(cancellationToken), o => canExecuteMethod == null || canExecuteMethod())
        {
        }
    }
}
