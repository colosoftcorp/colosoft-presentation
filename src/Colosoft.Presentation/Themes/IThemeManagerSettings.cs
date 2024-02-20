using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Themes
{
    public interface IThemeManagerSettings
    {
        string DefaultThemeName { get; set; }

        Task Save(CancellationToken cancellationToken);
    }
}