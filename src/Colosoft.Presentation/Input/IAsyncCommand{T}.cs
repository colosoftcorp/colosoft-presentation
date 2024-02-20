using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Input
{
    public interface IAsyncCommand<in T> : IRaiseCanExecuteChanged
    {
        Task ExecuteAsync(T parameter, CancellationToken cancellationToken);

        bool CanExecute(object parameter);

        System.Windows.Input.ICommand Command { get; }
    }
}
