using ReactiveUI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IErrorNotificationService
    {
        IDisposable Subscribe(IHandleObservableErrors observableErrors, string summary, Func<Exception, CancellationToken, Task<string>> detailsGetter = null);
    }
}