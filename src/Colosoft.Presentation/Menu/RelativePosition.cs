using System;

namespace Colosoft.Presentation.Menu
{
    public class RelativePosition : IMenuPosition
    {
        public Uri From { get; }

        public int Index { get; }

        public RelativePosition(Uri from, int index = 0)
        {
            this.From = from ?? throw new ArgumentNullException(nameof(from));
            this.Index = index;
        }

        public int CompareTo(IMenuPosition other)
        {
            if (other is RelativePosition relativePosition)
            {
                if (relativePosition.Index > this.Index)
                {
                    return 1;
                }
                else if (relativePosition.Index < this.Index)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            return -1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is RelativePosition relativePosition)
            {
                return relativePosition.From == this.From && relativePosition.Index == this.Index;
            }

            return false;
        }

        public override int GetHashCode() =>
            this.From.GetHashCode() ^ this.Index.GetHashCode();

        public static bool operator ==(RelativePosition left, RelativePosition right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(RelativePosition left, RelativePosition right)
        {
            return !(left == right);
        }

        public static bool operator <(RelativePosition left, RelativePosition right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(RelativePosition left, RelativePosition right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(RelativePosition left, RelativePosition right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(RelativePosition left, RelativePosition right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
