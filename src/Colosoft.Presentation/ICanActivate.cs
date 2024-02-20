using System;

namespace Colosoft.Presentation
{
    public interface ICanActivate
    {
        event EventHandler Activated;

        event EventHandler Deactivated;
    }
}
