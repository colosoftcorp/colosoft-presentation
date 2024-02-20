using Colosoft.Presentation.PresentationData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation
{
    public abstract class ViewModel : ReactiveObject, IViewModel, IRequestFocus, IDisposableHandlerContainer
    {
        private readonly ObservableAsPropertyHelper<bool> isBusyResult;
        private readonly IDictionary<string, ControlDataInfo> controlsData = new Dictionary<string, ControlDataInfo>();
        private readonly IList<IDisposable> handlers = new List<IDisposable>();

        public event EventHandler<FocusRequestedEventArgs> FocusRequested;

        protected ViewModel()
        {
            this.StateMonitor = new Threading.SimpleMonitor();
            this.isBusyResult = this
                .WhenAnyValue(f => f.StateMonitor.Busy)
                .ToProperty(this, f => f.IsBusy);
        }

        public bool IsBusy => this.isBusyResult.Value;

        public Threading.SimpleMonitor StateMonitor { get; }

        protected IEnumerable<IDisposable> Handlers => this.handlers.AsEnumerable();

        protected void AddHandler(IDisposable handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.handlers.Add(handler);
        }

        protected bool RemoveHandler(IDisposable handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return this.handlers.Remove(handler);
        }

        protected void OnError(
            IErrorNotificationService errorNotificationService,
            IHandleObservableErrors observableErrors,
            string summary)
        {
            this.AddHandler(errorNotificationService.Subscribe(observableErrors, summary));
        }

        public void AddStateListen(IViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel is ViewModel viewModel2)
            {
                this.StateMonitor.AddListen(viewModel2.StateMonitor);
            }
        }

        public bool RemoveStateListen(IViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel is ViewModel viewModel2)
            {
                return this.StateMonitor.RemoveListen(viewModel2.StateMonitor);
            }

            return false;
        }

        public void Focus(string propertyName)
        {
            this.FocusRequested?.Invoke(this, new FocusRequestedEventArgs(propertyName));
        }

        protected TControlData GetControlData<TControlData>(
            Func<TControlData> controlCreator,
            [System.Runtime.CompilerServices.CallerMemberName] string name = null)
            where TControlData : ControlData
        {
            if (controlCreator is null)
            {
                throw new ArgumentNullException(nameof(controlCreator));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!this.controlsData.TryGetValue(name, out var info))
            {
                info = new ControlDataInfo(controlCreator);
                this.controlsData.Add(name, info);
            }

            return info.Get<TControlData>();
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            this.RaisePropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            foreach (var name in propertyNames)
            {
                this.RaisePropertyChanged(name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            this.StateMonitor.Dispose();
            this.isBusyResult.Dispose();

            foreach (var handler in this.handlers)
            {
                handler.Dispose();
            }

            this.handlers.Clear();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposableHandlerContainer.AddHandler(IDisposable handler) => this.AddHandler(handler);

        bool IDisposableHandlerContainer.RemoveHandler(IDisposable handler) => this.RemoveHandler(handler);

        private sealed class ControlDataInfo
        {
            private readonly Func<ControlData> controlCreator;
            private ControlData control;

            public ControlDataInfo(Func<ControlData> controlCreator)
            {
                this.controlCreator = controlCreator;
            }

            public TControlData Get<TControlData>()
                where TControlData : ControlData
            {
                if (this.control == null)
                {
                    this.control = this.controlCreator();
                }

                return (TControlData)this.control;
            }
        }
    }
}
