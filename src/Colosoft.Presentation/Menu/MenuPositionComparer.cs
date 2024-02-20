using System.Collections.Generic;

namespace Colosoft.Presentation.Menu
{
    public class MenuPositionComparer : IComparer<IMenuPosition>
    {
        public static readonly MenuPositionComparer Instance = new MenuPositionComparer();

        public int Compare(IMenuPosition x, IMenuPosition y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.CompareTo(y);
        }
    }
}
