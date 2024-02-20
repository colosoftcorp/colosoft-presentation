using System;
using System.ComponentModel;

namespace Colosoft.Presentation.Input
{
    [TypeConverter(typeof(ModifierKeysConverter))]
    [Flags]
    public enum ModifierKeys
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
    }
}