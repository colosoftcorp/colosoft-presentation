using System;

namespace Colosoft.Presentation
{
    public interface IViewForTypeRepository
    {
        Type Get(Type viewModelType);
    }
}
