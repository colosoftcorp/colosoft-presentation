namespace Colosoft.Presentation
{
    public interface IAttachableViewModel
    {
        void Attach(object element);

        void Detach(object element);
    }
}
