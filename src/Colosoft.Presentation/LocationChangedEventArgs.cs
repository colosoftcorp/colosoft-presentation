namespace Colosoft.Presentation
{
    public class LocationChangedEventArgs
    {
        public LocationChangedEventArgs(string location, bool isNavigationIntercepted)
        {
            this.Location = location;
            this.IsNavigationIntercepted = isNavigationIntercepted;
        }

        public string Location { get; }

        public bool IsNavigationIntercepted { get; }
    }
}