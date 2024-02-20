using System.Collections.Generic;

namespace Colosoft.Presentation.Themes
{
    public interface IDataTemplateSalectorLoader
    {
        IEnumerable<IDataTemplateSelector> GetSelectors();
    }
}
