namespace Colosoft.Presentation.Input
{
    public interface IRoutedCommandFactory
    {
        IRoutedCommand Create(string name, System.Windows.Input.ICommand command, CommandDescription description);
    }
}
