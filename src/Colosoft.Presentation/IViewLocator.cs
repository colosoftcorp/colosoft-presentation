using System;

namespace Colosoft.Presentation
{
    public interface IViewLocator
    {
        IViewFor ResolveView<T>(T viewModel);

        IViewFor ResolveView(object viewModel, Type viewModelType);
    }
}
