namespace Colosoft.Presentation
{
    public interface IViewFor<T> : IViewFor
        where T : class
    {
        new T ViewModel { get; set; }
    }
}
