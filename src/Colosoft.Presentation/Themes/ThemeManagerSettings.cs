using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Themes
{
    public class ThemeManagerSettings : IThemeManagerSettings
    {
        public ThemeManagerSettings(string defaultThemeName)
        {
            this.DefaultThemeName = defaultThemeName ?? throw new System.ArgumentNullException(nameof(defaultThemeName));
        }

        public string DefaultThemeName { get; set; }

        public virtual Task Save(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
