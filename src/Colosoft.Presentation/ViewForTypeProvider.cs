using System;
using System.Collections.Generic;

namespace Colosoft.Presentation
{
    public class ViewForTypeProvider : IViewForTypeProvider
    {
        private readonly IEnumerable<IViewForTypeRepository> repositories;

        public ViewForTypeProvider(IEnumerable<IViewForTypeRepository> repositories)
        {
            this.repositories = repositories;
        }

        public Type Get(Type viewModelType)
        {
            foreach (var repository in this.repositories)
            {
                var type = repository.Get(viewModelType);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
