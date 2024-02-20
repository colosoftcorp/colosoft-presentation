namespace Colosoft.Presentation.PresentationData
{
    public class CommandDataConfigurator : ICommandDataConfigurator
    {
        private readonly Input.IRoutedCommandFactory routedCommandFactory;

        public CommandDataConfigurator(Input.IRoutedCommandFactory routedCommandFactory)
        {
            this.routedCommandFactory = routedCommandFactory;
        }

        public void Configure(ICommandData commandData)
        {
            if (commandData is ControlData controlData && controlData.RoutedCommand == null)
            {
                var command = this.routedCommandFactory.Create(
                    controlData.Name,
                    controlData.Command,
                    new Input.CommandDescription(
                        controlData.Label == null ? controlData.Name.GetFormatter() : controlData.Label,
                        controlData.ToolTipDescription == null ? controlData.Name.GetFormatter() : controlData.ToolTipDescription,
                        controlData.Gestures,
                        controlData.ToolTipDescription,
                        controlData.Gestures));

                controlData.RoutedCommand = command;
            }
            else if (commandData is Menu.IMenuControlData menuControlData && menuControlData.RoutedCommand == null)
            {
                var command = this.routedCommandFactory.Create(
                    menuControlData.Name,
                    menuControlData.Command,
                    new Input.CommandDescription(
                        menuControlData.Label == null ? menuControlData.Name.GetFormatter() : menuControlData.Label,
                        menuControlData.ToolTipDescription == null ? menuControlData.Name.GetFormatter() : menuControlData.ToolTipDescription,
                        menuControlData.Gestures,
                        menuControlData.ToolTipDescription,
                        menuControlData.Gestures));

                menuControlData.RoutedCommand = command;
            }
        }
    }
}
