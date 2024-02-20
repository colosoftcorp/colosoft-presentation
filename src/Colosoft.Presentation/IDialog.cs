using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IDialog : ITitleContainerView
    {
        event System.ComponentModel.CancelEventHandler Closing;

        event EventHandler Closed;

        event EventHandler Loaded;

        event EventHandler Activated;

        event EventHandler Deactivated;

        bool IsActive { get; }

        WindowShowMode ShowMode { get; }

        Task<bool?> ShowDialog(CancellationToken cancellationToken);

        Task Show(CancellationToken cancellationToken);

        Task Close(CancellationToken cancellationToken);

        Task Hide(CancellationToken cancellationToken);

        Task Focus(CancellationToken cancellationToken);
    }
}
