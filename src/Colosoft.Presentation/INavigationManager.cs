using System;

namespace Colosoft.Presentation
{
    public interface INavigationManager
    {
        event EventHandler<LocationChangedEventArgs> LocationChanged;

        string BaseUri { get; }

        string Uri { get; }

        INavigationQueryString QueryString { get; }

        void NavigateTo(string path, bool forceLoad = false);

        bool CanNavigateBack { get; }

        bool GoBack(string alternativeUrl = null);
    }
}
