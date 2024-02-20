namespace Colosoft.Presentation
{
    public interface IConfigurableCommandData : ICommandData
    {
        new Input.IRoutedCommand RoutedCommand { get; set; }
    }
}
