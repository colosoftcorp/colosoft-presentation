using System;
using System.Collections.Generic;

namespace Colosoft.Presentation
{
    public interface IResourcesRepository
    {
        string Name { get; }

        string ThemeName { get; }

        IEnumerable<string> Schemes { get; }

        object Get(Uri location);
    }
}
