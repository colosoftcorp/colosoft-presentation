using Colosoft.Presentation.Input;
using System;
using System.Linq;
using System.Windows.Input;

namespace Colosoft.Presentation
{
    public class RoutingState : NotificationObject
    {
        public RoutingState()
        {
            this.NavigationStack = new Collections.BaseObservableCollection<IRoutableViewModel>();
            this.NavigationStack.CollectionChanged += this.NavigationStackCollectionChanged;

            this.NavigateBack = new DelegateCommand(
                () => this.NavigationStack.RemoveAt(this.NavigationStack.Count - 1),
                () => this.NavigationStack.Any());

            this.Navigate = new DelegateCommand<IRoutableViewModel>(
                (viewModel) =>
                {
                    if (viewModel == null)
                    {
                        throw new ArgumentNullException(nameof(viewModel));
                    }

                    this.NavigationStack.Add(viewModel);
                });

            this.NavigateAndReset = new DelegateCommand<IRoutableViewModel>(
                (viewModel) =>
                {
                    this.NavigationStack.Clear();
                    this.Navigate.Execute(viewModel);
                });
        }

        public Collections.IObservableCollection<IRoutableViewModel> NavigationStack { get; }

        public ICommand NavigateBack { get; protected set; }

        public ICommand<IRoutableViewModel> Navigate { get; protected set; }

        public ICommand<IRoutableViewModel> NavigateAndReset { get; protected set;  }

        public IRoutableViewModel CurrentViewModel => this.NavigationStack.FirstOrDefault();

        private void NavigationStackCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
    }
}
