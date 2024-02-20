using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public interface ICommand<in T> : ICommand
    {
        bool CanExecute(T parameter);

        void Execute(T parameter);
    }
}