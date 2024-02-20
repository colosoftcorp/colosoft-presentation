namespace Colosoft.Presentation.PresentationData
{
#pragma warning disable CA1010 // Generic interface should also be implemented
    public interface IControlDataContainer : System.Collections.IEnumerable
#pragma warning restore CA1010 // Generic interface should also be implemented
    {
        int Count { get; }

        void Add(object data);

        void Insert(int index, object data);

        bool Remove(object data);

        void RemoveAt(int index);
    }
}
