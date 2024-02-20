using System;

namespace Colosoft.Presentation
{
    public class DefaultViewLocator : IViewLocator
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultViewLocator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Func<Type, Type> ViewModelToViewFunc { get; set; }

        private IViewFor AttemptViewResolutionFor(Type viewModelType)
        {
            if (viewModelType is null)
            {
                return null;
            }

            var proposedViewType = this.ViewModelToViewFunc?.Invoke(viewModelType);

            if (proposedViewType != null)
            {
                return this.serviceProvider.GetService(proposedViewType) as IViewFor;
            }

            proposedViewType = typeof(IViewFor<>).MakeGenericType(viewModelType);
            var view = this.serviceProvider.GetService(proposedViewType) as IViewFor;

            return view;
        }

        public virtual IViewFor ResolveView<T>(T viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var view = this.AttemptViewResolutionFor(viewModel.GetType());

            if (view == null)
            {
                view = this.AttemptViewResolutionFor(typeof(T));
            }

            return view;
        }

        public virtual IViewFor ResolveView(object viewModel, Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            return this.AttemptViewResolutionFor(viewModelType);
        }
    }
}
