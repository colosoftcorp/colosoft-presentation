using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Input
{
    public class RelayCommand : AwaitableDelegateCommand
    {
        public RelayCommand(Func<CancellationToken, Task> executeMethod)
            : base(executeMethod)
        {
        }

        public RelayCommand(Func<CancellationToken, Task> executeMethod, Func<bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }
    }
}
