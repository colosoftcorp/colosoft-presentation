using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public interface ICommandSource
    {
        ICommand Command { get; }

        object CommandParameter { get; }

        object CommandTarget { get; }
    }
}
