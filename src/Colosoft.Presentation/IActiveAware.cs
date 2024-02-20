using System;

namespace Colosoft.Presentation
{
    public interface IActiveAware
    {
        bool IsActive { get; set; }

        event EventHandler IsActiveChanged;
    }
}
