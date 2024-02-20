using Colosoft.Presentation.Input;

namespace Colosoft.Presentation
{
    public static class CommandDataExtensions
    {
        public static void NotityCanExecuteChanged(this ICommandData commandData)
        {
            if (commandData?.Command is IRaiseCanExecuteChanged raiseCanExecuteChanged)
            {
                raiseCanExecuteChanged.NotifyCanExecuteChanged();
            }
        }
    }
}
