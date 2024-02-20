using System.ComponentModel;

namespace Colosoft.Presentation
{
    public interface ICommandData : INotifyPropertyChanged
    {
        System.Windows.Input.ICommand Command { get; set; }

        Input.IRoutedCommand RoutedCommand { get; }

        Input.CommandBinding CreateCommandBinding();
    }
}
