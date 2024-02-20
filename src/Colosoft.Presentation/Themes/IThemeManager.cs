using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Themes
{
    public interface IThemeManager
    {
        IThemeManagerSettings Settings { get; }

        ITheme CurrentTheme { get; }

        ICollection<ITheme> Themes { get; }

        Task Change(ITheme theme, CancellationToken cancellationToken);

        IDataTemplateSelector GetDataTemplateSelector(string fullName);

        IDataTemplateSelector FindDataTemplateSelector(object item);

        object SelectDataTemplate(string selectorFullName, object item);
    }
}
