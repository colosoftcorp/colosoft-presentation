using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Colosoft.Presentation.Menu
{
    public interface IMenuControlData : INotifyCollectionChanged, IEnumerable<IMenuControlData>, IConfigurableCommandData, ICommandParameterContainer
    {
        string Name { get; }

        IMessageFormattable Label { get; }

        IMessageFormattable ToolTipTitle { get; }

        IMessageFormattable ToolTipDescription { get; }

        Uri LargeImage { get; }

        Uri SmallImage { get; }

        string Gestures { get; }

        System.Collections.ObjectModel.ObservableCollection<IMenuControlData> Children { get; }

        int Count { get; }

        IMenuPosition Position { get; }
    }
}
