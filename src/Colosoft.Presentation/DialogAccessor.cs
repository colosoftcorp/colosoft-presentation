using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public class DialogAccessor : NotificationObject, IDialogAccessor, IDialogEventsNotifier
    {
        private bool? dialogResult;
        private bool isFocused;

        public event ShowDialogRequestedEventHandler ShowDialogRequested;
        public event ActionDialogRequestedEventHandler CloseRequested;
        public event ActionDialogRequestedEventHandler HideRequested;
        public event ActionDialogRequestedEventHandler ShowRequested;
        public event ActionDialogRequestedEventHandler FocusRequested;
        public event DialogClosingEventHandler Closing;
        public event ActionDialogRequestedEventHandler Closed;
        public event ActionDialogRequestedEventHandler Activated;
        public event ActionDialogRequestedEventHandler Deactivated;
        public event ActionDialogRequestedEventHandler Loaded;
        public event ActionDialogRequestedEventHandler GotFocus;
        public event ActionDialogRequestedEventHandler LostFocus;

        public bool? DialogResult
        {
            get => this.dialogResult;
            set
            {
                if (this.dialogResult != value)
                {
                    this.dialogResult = value;
                    this.OnPropertyChanged(nameof(this.DialogResult));
                }
            }
        }

        public bool IsFocused
        {
            get => this.isFocused;
            private set
            {
                if (this.isFocused != value)
                {
                    this.isFocused = value;
                    this.OnPropertyChanged(nameof(this.IsFocused));
                }
            }
        }

        public async virtual Task<bool?> ShowDialog(CancellationToken cancellationToken)
        {
            if (this.ShowDialogRequested != null)
            {
                return await this.ShowDialogRequested(this, cancellationToken);
            }

            return null;
        }

        public async virtual Task Show(CancellationToken cancellationToken)
        {
            if (this.ShowRequested != null)
            {
                await this.ShowRequested(this, cancellationToken);
            }
        }

        public async Task Close(CancellationToken cancellationToken)
        {
            if (this.CloseRequested != null)
            {
                await this.CloseRequested(this, cancellationToken);
            }
        }

        public async Task Hide(CancellationToken cancellationToken)
        {
            if (this.HideRequested != null)
            {
                await this.HideRequested(this, cancellationToken);
            }
        }

        public async Task Focus(CancellationToken cancellationToken)
        {
            if (this.FocusRequested != null)
            {
                await this.FocusRequested(this, cancellationToken);
            }
        }

        public async Task NotifyClosing(object dialog, CancelEventArgs e, CancellationToken cancellationToken)
        {
            if (this.Closing != null)
            {
                await this.Closing(dialog, e, cancellationToken);
            }
        }

        public async Task NotifyClosed(object dialog, CancellationToken cancellationToken)
        {
            if (this.Closed != null)
            {
                await this.Closed(dialog, cancellationToken);
            }
        }

        public async Task NotifyActivated(object dialog, CancellationToken cancellationToken)
        {
            if (this.Activated != null)
            {
                await this.Activated(dialog, cancellationToken);
            }
        }

        public async Task NotifyDeactivated(object dialog, CancellationToken cancellationToken)
        {
            if (this.Deactivated != null)
            {
                await this.Deactivated(dialog, cancellationToken);
            }
        }

        public async Task NotifyLoaded(object dialog, CancellationToken cancellationToken)
        {
            if (this.Loaded != null)
            {
                await this.Loaded(dialog, cancellationToken);
            }
        }

        public async Task NotifyGotFocus(object dialog, CancellationToken cancellationToken)
        {
            this.IsFocused = true;
            if (this.GotFocus != null)
            {
                await this.GotFocus(dialog, cancellationToken);
            }
        }

        public async Task NotifyLostFocus(object dialog, CancellationToken cancellationToken)
        {
            this.IsFocused = false;
            if (this.LostFocus != null)
            {
                await this.LostFocus(dialog, cancellationToken);
            }
        }
    }
}
