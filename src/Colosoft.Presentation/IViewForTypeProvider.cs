using System;

namespace Colosoft.Presentation
{
    public interface IViewForTypeProvider
    {
        Type Get(Type viewModelType);
    }
}
