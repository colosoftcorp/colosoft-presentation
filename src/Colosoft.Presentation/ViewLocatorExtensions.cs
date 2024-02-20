using System;

namespace Colosoft.Presentation
{
    public static class ViewLocatorExtensions
    {
        public static IDialog ResolveDialog<T>(this IViewLocator viewLocator, T viewModel)
        {
            if (viewLocator is null)
            {
                throw new ArgumentNullException(nameof(viewLocator));
            }

            return (IDialog)viewLocator.ResolveView<T>(viewModel);
        }
    }
}
