namespace Colosoft.Presentation
{
    public interface IAttacher
    {
        void AttachViewModel(object element, object viewModel);

        void Attach(ICommandData value);

        void Attach(object element, ICommandData value);

        void DetachViewModel(object element, object viewModel);

        void Detach(ICommandData value);

        void Detach(object element, ICommandData value);
    }
}
