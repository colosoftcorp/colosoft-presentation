using ReactiveUI;
using System;

namespace Colosoft.Presentation
{
    public interface IViewModel : IReactiveObject, IDisposable
    {
        bool IsBusy { get; }

        Threading.SimpleMonitor StateMonitor { get; }
    }
}
