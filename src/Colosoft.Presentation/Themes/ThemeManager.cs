using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.Themes
{
    public class ThemeManager : NotificationObject, IThemeManager
    {
        private readonly List<ITheme> themes;
        private ITheme currentTheme;
        private List<IDataTemplateSelector> selectors;

        public ThemeManager(
            IThemeManagerSettings settings,
            IEnumerable<ITheme> themes)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.themes = new List<ITheme>();
            this.Settings = settings;

            if (themes != null)
            {
                foreach (var i in themes)
                {
                    if (!this.themes.Exists(f => f.Name == i.Name))
                    {
                        this.themes.Add(i);
                    }
                }
            }
        }

        public IThemeManagerSettings Settings { get; }

        public ITheme CurrentTheme
        {
            get
            {
                if (this.currentTheme == null)
                {
                    if (this.themes.Count == 0)
                    {
                        throw new InvalidOperationException(ResourceMessageFormatter.Create(
                            () => Properties.Resources.ThemeManager_DefaultThemeNotFound).Format());
                    }

                    var settings = this.Settings;

                    if (!string.IsNullOrEmpty(settings?.DefaultThemeName))
                    {
                        this.currentTheme = this.themes.FirstOrDefault(f => f.Name == settings.DefaultThemeName);
                    }

                    if (this.currentTheme == null)
                    {
                        this.currentTheme = this.themes[0];
                    }
                }

                return this.currentTheme;
            }
        }

        public ICollection<ITheme> Themes => this.themes;

        private ICollection<IDataTemplateSelector> Selectors
        {
            get
            {
                if (this.selectors == null)
                {
                    this.selectors = new List<IDataTemplateSelector>(this.CurrentTheme.GetDataTemplateSelectors());
                }

                return this.selectors;
            }
        }

        public async Task Change(ITheme theme, CancellationToken cancellationToken)
        {
            if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            this.currentTheme = theme;
            this.selectors = null;

            if (this.Settings != null)
            {
                this.Settings.DefaultThemeName = this.currentTheme.Name;
                await this.Settings.Save(cancellationToken);
            }
        }

        public IDataTemplateSelector GetDataTemplateSelector(string fullName)
        {
            return this.Selectors.FirstOrDefault(f => f.FullName == fullName);
        }

        public IDataTemplateSelector FindDataTemplateSelector(object item)
        {
            foreach (var selector in this.Selectors)
            {
                var uri = selector.SelectTemplate(item);
                if (uri != null)
                {
                    object result = this.CurrentTheme.GetResourceObject(uri);
                    if (result != null)
                    {
                        return selector;
                    }
                }
            }

            return null;
        }

        public object SelectDataTemplate(string selectorFullName, object item)
        {
            if (selectorFullName is null)
            {
                throw new ArgumentNullException(nameof(selectorFullName));
            }

            var selector = this.GetDataTemplateSelector(selectorFullName);

            if (selector == null)
            {
                throw new InvalidOperationException(ResourceMessageFormatter.Create(
                    () => Properties.Resources.ThemeManager_DataTemplateSelectorNotFound, selectorFullName).Format());
            }

            var uri = selector.SelectTemplate(item);

            if (uri == null)
            {
                return null;
            }

            return this.CurrentTheme.GetResourceObject(uri);
        }
    }
}
