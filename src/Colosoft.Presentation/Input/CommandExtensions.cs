using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Input
{
    public static class CommandExtensions
    {
        public static Task ExecuteAsync(this System.Windows.Input.ICommand command, CancellationToken cancellationToken = default) =>
            command.ExecuteAsync(null, cancellationToken);

        public static async Task ExecuteAsync(this System.Windows.Input.ICommand command, object parameter, CancellationToken cancellationToken = default)
        {
            if (command is IAsyncCommand asyncCommand)
            {
                await asyncCommand.ExecuteAsync(parameter, cancellationToken);
            }
            else
            {
                command.Execute(parameter);
            }
        }

        public static Task ExecuteAsync<T>(this System.Windows.Input.ICommand command, CancellationToken cancellationToken = default) =>
            command.ExecuteAsync<T>(default, cancellationToken);

        public static async Task ExecuteAsync<T>(this System.Windows.Input.ICommand command, T parameter, CancellationToken cancellationToken = default)
        {
            if (command is IAsyncCommand<T> asyncCommand)
            {
                await asyncCommand.ExecuteAsync(parameter, cancellationToken);
            }
            else
            {
                command.Execute(parameter);
            }
        }
    }
}
