using System;

namespace Colosoft.Presentation.Menu
{
    public class MenuItemCommand : Input.Command
    {
        private readonly MenuItem menuItem;

        public MenuItemCommand(MenuItem menuItem)
            : base(
                  GetText(menuItem),
                  GetName(menuItem),
                  typeof(MenuItem),
                  CreateCommandDescription(menuItem))
        {
            this.menuItem = menuItem;
        }

        private static string GetText(MenuItem menuItem)
        {
            if (menuItem?.ToolTipDescription != null)
            {
                return menuItem.ToolTipDescription.Format(System.Globalization.CultureInfo.CurrentUICulture);
            }
            else if (menuItem?.Label != null)
            {
                return menuItem.Label.Format(System.Globalization.CultureInfo.CurrentUICulture);
            }
            else
            {
                return menuItem?.Name ?? string.Empty;
            }
        }

        private static string GetName(MenuItem menuItem)
        {
            if (menuItem?.Label != null)
            {
                return menuItem.Label.Format(System.Globalization.CultureInfo.CurrentUICulture);
            }
            else
            {
                return menuItem?.Name ?? string.Empty;
            }
        }

        private static Input.CommandDescription CreateCommandDescription(MenuItem menuItem)
        {
            IMessageFormattable text;

            if (menuItem?.Label != null)
            {
                text = menuItem.Label;
            }
            else
            {
                text = menuItem?.Name.GetFormatter() ?? MessageFormattable.Empty;
            }

            IMessageFormattable description;

            if (menuItem?.ToolTipDescription != null)
            {
                description = menuItem.ToolTipDescription;
            }
            else
            {
                description = menuItem?.Name.GetFormatter() ?? MessageFormattable.Empty;
            }

            return new Input.CommandDescription(text, description, menuItem?.Gestures);
        }

        protected override bool OnCanExecute(Input.CommandContext commandContext)
        {
            if (commandContext is null)
            {
                throw new ArgumentNullException(nameof(commandContext));
            }

            if (this.menuItem.Command == null)
            {
                return false;
            }

            return this.menuItem.Command.CanExecute(commandContext.CommandParameter);
        }

        protected override void OnExecute(Input.CommandContext commandContext)
        {
            if (commandContext is null)
            {
                throw new ArgumentNullException(nameof(commandContext));
            }

            this.menuItem.Command.Execute(commandContext.CommandParameter);
        }
    }
}
