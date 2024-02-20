using System;

namespace Colosoft.Presentation
{
    public interface IRequestFocus
    {
        event EventHandler<FocusRequestedEventArgs> FocusRequested;
    }
}
