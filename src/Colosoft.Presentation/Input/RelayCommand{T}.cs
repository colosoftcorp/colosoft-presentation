using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Input
{
    public class RelayCommand<T> : AwaitableDelegateCommand<T>
    {
        public RelayCommand(Func<T, CancellationToken, Task> executeMethod)
            : base(executeMethod)
        {
        }

        public RelayCommand(Func<T, CancellationToken, Task> executeMethod, Func<T, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }
    }
}
