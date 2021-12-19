using System;

namespace SortBase
{
    public class Numbers
    {
        public double First { get; set; }
        public int Second { get; set; }

        public Numbers(double first, int second)
        {
            First = first;
            Second = second;
        }

        public Numbers(byte[] first, byte[] second)
        {
            First = BitConverter.ToDouble(first, 0);
            Second = BitConverter.ToInt32(second, 0);
        }

        public static bool operator >(Numbers a, Numbers b)
        {
            if (a == null)//1
                return b != null;

            if (b == null)//1
                return false;

            if (a.Second == b.Second)//1
                return b.First - a.First < 0.00000001;//4

            return a.Second > b.Second;//3
        }

        public static bool operator <(Numbers a, Numbers b)//10
        {
            if (b == null)
                return a != null;

            if (a == null)
                return false;

            if (a.Second == b.Second)
                return a.First - b.First < 0.00000001;

            return a.Second < b.Second;
        }

        public override string ToString()
        {
            return $" {First:F2}-{Second} ";
        }
    }
}
