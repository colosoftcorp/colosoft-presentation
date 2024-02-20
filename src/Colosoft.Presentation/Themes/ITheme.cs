using System;
using System.Collections.Generic;

namespace Colosoft.Presentation.Themes
{
    public interface ITheme : System.ComponentModel.ISupportInitialize
    {
        string Name { get; }

        IMessageFormattable Description { get; }

        object GetResourceObject(Uri location);

        IEnumerable<IDataTemplateSelector> GetDataTemplateSelectors();
    }
}
