using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public delegate Task<bool?> ShowDialogRequestedEventHandler(object sender, CancellationToken cancellationToken);

    public delegate Task ActionDialogRequestedEventHandler(object sender, CancellationToken cancellationToken);

    public delegate Task DialogClosingEventHandler(object sender, CancelEventArgs e, CancellationToken cancellationToken);

    public interface IDialogObserver
    {
        event ShowDialogRequestedEventHandler ShowDialogRequested;

        event ActionDialogRequestedEventHandler CloseRequested;

        event ActionDialogRequestedEventHandler HideRequested;

        event ActionDialogRequestedEventHandler ShowRequested;

        event ActionDialogRequestedEventHandler FocusRequested;
    }
}