using System;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
    public interface IRoutedCommand : ICommand
    {
        InputGestureCollection InputGestures { get; }

        string Name { get; }

        Type OwnerType { get; }

        bool CanExecute(CommandContext commandContext);

        void Execute(CommandContext commandContext);
    }
}