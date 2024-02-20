using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IDialogAccessor : INotifyPropertyChanged
    {
        event DialogClosingEventHandler Closing;

        event ActionDialogRequestedEventHandler Closed;

        event ActionDialogRequestedEventHandler Activated;

        event ActionDialogRequestedEventHandler Deactivated;

        event ActionDialogRequestedEventHandler GotFocus;

        event ActionDialogRequestedEventHandler LostFocus;

        bool? DialogResult { get; set; }

        bool IsFocused { get; }

        Task<bool?> ShowDialog(CancellationToken cancellationToken);

        Task Show(CancellationToken cancellationToken);

        Task Close(CancellationToken cancellationToken);

        Task Hide(CancellationToken cancellationToken);

        Task Focus(CancellationToken cancellationToken);
    }
}