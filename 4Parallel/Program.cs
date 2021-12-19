using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4Parallel
{
    internal class Program
    {
        private static List<int> _s = new List<int>() { 10, 15, 6, 7, 2, 15, 1, 6, 5, 1, 6, 13, 11, 18, 6, 7, 4, 2, 13, 12, 7, 2, 9, 8, 15, 18, 8, 9, 4, 3, 5, 7, 9, 2, 15 };
        private static List<int> _p = new List<int>() { 12, 5, 16, 17, 12, 5, 11, 6, 8, 1, 6, 3, 1, 8, 6, 7, 4, 2, 3, 2, 7, 2, 9, 8, 1, 8, 9, 4, 3, 5, 7, 9, 2, 15, 7 };
        private static readonly Counter Counter = new Counter();

        private static void Main()
        {
            Console.WriteLine(" (c) 2019 Name Surname");

            Console.WriteLine(" {0,5} {1,5} {2,5} {3,10} {4,10} {5,10}", "Nr", "N", "W", "Result", "Time (ms)", "Counter");
            Console.WriteLine(" ==================================================");
            for (var i = 0; i < 12; i++) // 12
            {
                var n = i + 20;
                var w = 400;
                //InitializeLists(n);
                Counter.Reset();
                Counter.Start();
                var g = G(n, w);
                Counter.Stop();
                Console.WriteLine(" {0, 5} {1, 5} {2, 5} {3, 10} {4, 10} {5, 10}", i + 1, n, w, g, Counter.GetTime(), Counter.GetCount());
            }

            Console.WriteLine(" -=Done=-");
            Console.ReadKey();
        }

        private static int G(int k, int r)
        {
            var tasks = new Task[2];
            Counter.Add(2);
            if (r == 0 || k == 0)
                return 0;

            Counter.Add();
            if (_s[k] > r)
                return G(k - 1, r);

            Counter.Add(2);

            var first = Task<int>.Factory.StartNew(() => G1(k - 1, r));
            tasks[0] = first;
            var second = Task<int>.Factory.StartNew(() => G1(k - 1, r - _s[k]) + _p[k]);
            tasks[1] = second;
            Task.WaitAll(tasks);
            return Math.Max(first.Result, second.Result);
        }

        private static int G1(int k, int r)
        {
            Counter.Add(2);
            if (r == 0 || k == 0)
                return 0;

            Counter.Add();
            if (_s[k] > r)
                return G1(k - 1, r);

            Counter.Add(2);
            var first = G1(k - 1, r);
            var second = G1(k - 1, r - _s[k]) + _p[k];
            return Math.Max(first, second);
        }

        private static void InitializeLists(int n)
        {
            var rnd = new Random(15216521);
            _s = new List<int>();
            _p = new List<int>();
            for (var i = 0; i <= n; i++)
            {
                var num = rnd.Next(20);
                _s.Add(num);
                num = rnd.Next(20);
                _p.Add(num);
            }
        }
    }
}
