using System;

namespace Colosoft.Presentation.Themes
{
    public interface IDataTemplateSelector
    {
        string FullName { get; }

        Uri SelectTemplate(object item);
    }
}
