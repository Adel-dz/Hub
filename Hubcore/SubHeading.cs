using System;
using System.Text;
using static System.Diagnostics.Debug;


namespace DGD.HubCore
{
    public class SubHeading : IComparable<SubHeading>, IEquatable<SubHeading>
    {
        public const ulong MaxValue = 9999999999;
        public const ulong MinValue = 100000000;

        public static readonly SubHeading MinSubHeading = new SubHeading(MinValue);
        public static readonly SubHeading MaxSubHeading = new SubHeading(MaxValue);

        public SubHeading(ulong value)
        {
            Assert(value <= MaxValue);
            Assert(value >= MinValue);

            Value = value;
        }


        public ulong Value { get; }


        public static SubHeading Parse(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            var sb = new StringBuilder(str);

            for (int i = sb.Length - 1; i >= 0; --i)
                if (sb[i] == '.')
                    sb.Remove(i , 1);

            ulong value;
            if (!ulong.TryParse(sb.ToString() , out value))
                return null;

            return value <= MaxValue && value >= MinValue ? new SubHeading(value) : null;
        }

        public override string ToString()
        {
            const int STR_LEN = 12;

            var sb = new StringBuilder(Value.ToString("0000000000") , STR_LEN);
            sb.Insert(8 , '.');
            sb.Insert(6 , '.');
            sb.Insert(4 , '.');

            return sb.ToString();
        }
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as SubHeading);

        public int CompareTo(SubHeading other) => other == null ? 1 : Value.CompareTo(other.Value);
        public bool Equals(SubHeading other) => other != null && other.Value == Value;
    }
}
