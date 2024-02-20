using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IDialogEventsNotifier
    {
        Task NotifyClosing(object dialog, CancelEventArgs e, CancellationToken cancellationToken);

        Task NotifyClosed(object dialog, CancellationToken cancellationToken);

        Task NotifyActivated(object dialog, CancellationToken cancellationToken);

        Task NotifyDeactivated(object dialog, CancellationToken cancellationToken);

        Task NotifyLoaded(object dialog, CancellationToken cancellationToken);

        Task NotifyGotFocus(object dialog, CancellationToken cancellationToken);

        Task NotifyLostFocus(object dialog, CancellationToken cancellationToken);
    }
}
