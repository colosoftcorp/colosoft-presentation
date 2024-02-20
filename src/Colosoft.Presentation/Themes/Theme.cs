using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation.Themes
{
    public abstract class Theme : ITheme
    {
        private readonly IEnumerable<IResourcesRepository> resourcesRepositories;

        protected Theme(IEnumerable<IResourcesRepository> resourcesRepositories)
        {
            this.resourcesRepositories = resourcesRepositories;
        }

        public abstract string Name { get; }

        public virtual IMessageFormattable Description => MessageFormattable.Empty;

        public virtual void BeginInit()
        {
            // ignore
        }

        public virtual void EndInit()
        {
            // ignore
        }

        public virtual IEnumerable<IDataTemplateSelector> GetDataTemplateSelectors() => Enumerable.Empty<IDataTemplateSelector>();

        public object GetResourceObject(Uri location)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            foreach (var resource in this.resourcesRepositories)
            {
                if (resource.ThemeName == this.Name && resource.Schemes.Contains(location.Scheme))
                {
                    var result = resource.Get(location);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
