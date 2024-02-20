using System;

namespace Colosoft.Presentation
{
    internal static class GlobalEventManager
    {
#pragma warning disable CA1801 // Review unused parameters
        internal static void AddOwner(RoutedEvent routedEvent, Type ownerType)
        {
            // ignore
        }

        internal static int GetNextAvailableGlobalIndex(object value)
        {
            return 0;
        }
#pragma warning restore CA1801 // Review unused parameters
    }
}