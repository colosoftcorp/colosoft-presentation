using System;
using System.Collections.Generic;

namespace Colosoft.Presentation.Menu
{
    public class MenuControlDataPositionComparer : IComparer<IMenuControlData>
    {
        public static readonly MenuControlDataPositionComparer Instance = new MenuControlDataPositionComparer();

        public int Compare(IMenuControlData x, IMenuControlData y)
        {
            if (x == null)
            {
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }

            return MenuPositionComparer.Instance.Compare(x.Position, y.Position);
        }
    }
}
