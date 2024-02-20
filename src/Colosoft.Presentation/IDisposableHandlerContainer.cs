using System;

namespace Colosoft.Presentation
{
    public interface IDisposableHandlerContainer
    {
        void AddHandler(IDisposable handler);

        bool RemoveHandler(IDisposable handler);
    }
}
