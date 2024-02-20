namespace Colosoft.Presentation
{
    public interface IRoutableViewModel
    {
#pragma warning disable CA1056 // URI-like properties should not be strings
        string UrlPathSegment { get; }
#pragma warning restore CA1056 // URI-like properties should not be strings

        IScreen HostScreen { get; }
    }
}
