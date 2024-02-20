using System;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IBusyNotificationService
    {
        Task<IDisposable> NotifyBusyAsync(NotifyBusyOptions options);

        IDisposable NotifyBusy(NotifyBusyOptions options);
    }
}
