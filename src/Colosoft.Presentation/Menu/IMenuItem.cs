using System;

namespace Colosoft.Presentation.Menu
{
    public interface IMenuItem : ICommandData, ICommandParameterContainer
    {
        string Name { get; }

        IMessageFormattable Label { get; set; }

        Uri LargeImage { get; set; }

        Uri SmallImage { get; set; }

        IMessageFormattable ToolTipTitle { get; set; }

        IMessageFormattable ToolTipDescription { get; set; }

        string Gestures { get; set; }

        IMenuPosition Position { get; set; }

        Uri Path { get; set; }
    }
}
