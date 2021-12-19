using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _3HashSearch
{
    public class Methods
    {
        private readonly Counter _counter = new Counter();

        public void MultiThreadSearch(int n, HashTable table, List<string> keys)
        {
            const int threads = 4;
            var portions = n / threads;

            var tasks = new List<Task>();

            for (var j = 0; j < threads; j++)
            {
                Console.WriteLine($"number is {j}");
                var numb = j;
                var t = new Task(() => SearchTask(portions, numb, n, table, keys));
                tasks.Add(t);
                t.Start();
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Done all tasks in {_counter.GetCount()} steps for {n} items");
        }

        public static void FakMultiThreading()
        {
            Thread.Sleep(2000);
            for (var i = 0; i < 10; i++)
            {
                Console.Write("f");
            }
        }

        public void SearchTask(int portions, int j, int n, HashTable table, List<string> keys)
        {
            Thread.Sleep(1000);

            for (var i = portions * j; i < Math.Min(portions * (j + 1), n); i++)
            {
                _counter.Add();
                SortBase.Counter.Add();
                var found = table.Get(keys[i]);
                if (string.IsNullOrWhiteSpace(found))
                    Console.WriteLine($"not found i - {i}. j - {j}. key - {keys[i]}");
            }

            Console.WriteLine($"finished task {j} {n} {portions}");
        }
    }
}
