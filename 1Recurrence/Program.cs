using System;
using System.Collections.Generic;

namespace _1Recurrence
{
    internal class Program
    {
        private static List<int> _s = new List<int>() { 10, 15, 6, 7, 2, 15, 1, 6, 5, 1, 6, 13, 11, 18, 6, 7, 4, 2, 13, 12, 7, 2, 9, 8, 15, 18, 8, 9, 4, 3, 5, 7, 9, 2, 15 };
        private static List<int> _p = new List<int>() { 12, 5, 16, 17, 12, 5, 11, 6, 8, 1, 6, 3, 1, 8, 6, 7, 4, 2, 3, 2, 7, 2, 9, 8, 1, 8, 9, 4, 3, 5, 7, 9, 2, 15, 7 };
        private static readonly Counter Counter = new Counter();
        private static int[,] _matrix;

        private static void Main(string[] args)
        {
            Console.WriteLine(" (c) 2019 Name Surname");

            Console.WriteLine(" {0,5} {1,5} {2,5} {3,10} {4,15} {5,10} {6,10} {7,15} {8,10}", "Nr", "N", "W", "R result", "R Time(ticks)", "R Counter", "D result", "D Time(ticks)", "D Counter");
            Console.WriteLine(" =============================================================================================");
            for (var i = 0; i < 12; i++)
            {
                var n = i + 20; //rnd.Next(i + 20);
                var w = 400;
                //InitializeLists(n); // initializes random lists
                Counter.Reset();
                Counter.Start();
                var g = G(n, w);
                Counter.Stop();
                var rTime = Counter.GetTicks();
                var rCounter = Counter.GetCount();

                Counter.Reset();
                Counter.Start();
                var g1 = G1(n, w);
                Counter.Stop();
                Console.WriteLine(" {0, 5} {1, 5} {2, 5} {3, 10} {4, 15} {5, 10} {6,10} {7,15} {8,10}", i + 1, n, w, g, rTime, rCounter, g1, Counter.GetTicks(), Counter.GetCount());
            }

            Console.WriteLine(" -=Done=-");
            Console.ReadKey();
        }

        private readonly int[,] _ar = new int[20, 20];

        private static int G(int k, int r)
        {
            Counter.Add(2);
            if (k == 0 || r == 0)
                return 0;

            Counter.Add();
            if (_s[k] > r)
                return G(k - 1, r);

            Counter.Add(2);
            var first = G(k - 1, r);
            var second = G(k - 1, r - _s[k]) + _p[k];
            return Math.Max(first, second);
        }

        private static int G1(int k, int r)
        {
            _matrix = new int[k+1, r+1];

            for (var i = 1; i <= k; i++)
            {
                for (var j = 1; j <= r; j++)
                {
                    if (_s[i] > j)
                    {
                        _matrix[i, j] = _matrix[i - 1, j];
                    }
                    else
                    {
                        _matrix[i, j] = Math.Max(_matrix[i - 1, j], _p[i] + _matrix[i - 1, j - _s[i]]);
                    }
                }
            }

            Counter.Add((k+1)*(r+1)*3);
            return _matrix[k, r];
        }

        private static void InitializeLists(int n)
        {
            var rnd = new Random(15216521);
            _s = new List<int>();
            _p = new List<int>();
            for (var i = 0; i <= n; i++)
            {
                _s.Add(rnd.Next(n));
                _p.Add(rnd.Next(n));
            }
        }
    }
}
