namespace Colosoft.Presentation
{
    public interface IDialogViewModel : IViewModel
    {
        IDialogAccessor DialogAccessor { get; }
    }
}
