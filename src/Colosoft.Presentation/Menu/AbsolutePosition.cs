namespace Colosoft.Presentation.Menu
{
    public struct AbsolutePosition : IMenuPosition, System.IEquatable<AbsolutePosition>
    {
        public int Index { get; set; }

        public AbsolutePosition(int index)
        {
            this.Index = index;
        }

        public int CompareTo(IMenuPosition other)
        {
            if (other is RelativePosition relativePosition)
            {
                if (relativePosition.Index < this.Index)
                {
                    return 1;
                }
                else if (relativePosition.Index > this.Index)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else if (other is AbsolutePosition absolutePosition)
            {
                if (absolutePosition.Index < this.Index)
                {
                    return 1;
                }
                else if (absolutePosition.Index > this.Index)
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
            if (obj is AbsolutePosition absolutePosition)
            {
                return absolutePosition.Index == this.Index;
            }

            return false;
        }

        public bool Equals(AbsolutePosition other) =>
            other.Index == this.Index;

        public override int GetHashCode() =>
            this.Index.GetHashCode();

        public static bool operator ==(AbsolutePosition left, AbsolutePosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AbsolutePosition left, AbsolutePosition right)
        {
            return !(left == right);
        }

        public static bool operator <(AbsolutePosition left, AbsolutePosition right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(AbsolutePosition left, AbsolutePosition right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(AbsolutePosition left, AbsolutePosition right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(AbsolutePosition left, AbsolutePosition right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
