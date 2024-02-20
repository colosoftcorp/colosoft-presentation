namespace Colosoft.Presentation
{
    public interface INavigationQueryString
    {
        void Serialize<T>(T instance, bool replace = false);

        T Deserialize<T>()
            where T : new();

        void Deserialize<T>(T instance);

        string Get(string parameterName);
    }
}
