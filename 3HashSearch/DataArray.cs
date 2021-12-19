using System;

namespace _3HashSearch
{
    public interface IDataArray
    {
        int Length { get; }
        Tuple<string, string> this[int index] { get; set; }
        void InsertionSortArray();
        bool CheckSorted();
        void Print(int n);
        void PrintFromTo(int from, int to);
    }

    public class DataArray : IDataArray
    {
        private readonly Tuple<string, string>[] _data;

        public int Length { get; }

        public Tuple<string, string> this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        public DataArray(int n, int seed)
        {
            _data = new Tuple<string, string>[n];
            var rand = new Random(seed);
            Length = n;

            for (var i = 0; i < Length; i++)
            {
                _data[i] = Tuple.Create("s", "a");
            }
        }

        public DataArray(Tuple<string, string>[] numbers)
        {
            _data = numbers;
            Length = _data.Length;
        }

        public void InsertionSortArray()
        {
            for (var i = 1; i < Length; i++)
            {
                var j = i;
                var tmp = this[i];

                while (j > 0 && string.Compare(this[j - 1].Item2, tmp.Item2, StringComparison.Ordinal) > 0)//10
                {
                    this[j] = this[j - 1];
                    j--;
                }

                this[j] = tmp;
            }
        }

        public bool CheckSorted()
        {
            for (var i = 1; i < Length; i++)
                if (string.Compare(this[i - 1].Item2, this[i].Item2, StringComparison.Ordinal) > 0)
                    return false;

            return true;
        }

        public void Print(int n)
        {
            if (n < 1)
                return;

            var last = Math.Min(n, Length);

            for (var i = 0; i < last; i++)
                Console.Write("\n{0}", this[i]);

            Console.WriteLine();
        }

        public void PrintFromTo(int from, int to)
        {
            var last = Math.Min(to, Length);

            for (var i = 0; i < last; i++)
                if (i >= from)
                    Console.Write("\n{0}", this[i]);

            Console.WriteLine();
        }

        public int GetChainCount()
        {
            return Length;
        }

        public string PrintOutChain()
        {
            var nodeChain = "";
            for (var i = 0; i < Length; i++)
            {
                nodeChain += $" >>> {this[i].Item1}={GetKeyHash(this[i].Item1)}";
            }

            return nodeChain;
        }

        public int GetKeyHash(string key)
        {
            return Math.Abs(key.GetHashCode());
        }
    }
}
