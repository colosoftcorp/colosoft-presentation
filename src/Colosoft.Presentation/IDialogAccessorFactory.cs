namespace Colosoft.Presentation
{
    public interface IDialogAccessorFactory
    {
        IDialogAccessor Create(object viewModel);
    }
}
