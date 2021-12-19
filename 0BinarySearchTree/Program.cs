using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0BinarySearchTree
{
    internal class Program
    {
        private static readonly Random Random = new Random();

        private static void Main()
        {
            const int n = 500000;
            var names = new string[n + 1];
            var strTree = new BinarySearchTree();
            var stopWatch = new Stopwatch();
            for (var i = 0; i < n; i++)
            {
                var s = RandomName(32);
                names[i] = s;
                strTree.Insert(s);
            }

            names[n] = RandomName(32);
            stopWatch.Start();
            var count = SequentialLoop(names, strTree);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for sequential loop: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Contains: {0,6:N0} Total: {1,6:N0}", count, names.Length);
            stopWatch.Reset();
            stopWatch.Start();
            count = ParallelTaskLoop(names, strTree);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for parallel loop: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Contains: {0,6:N0} Total: {1,6:N0}", count, names.Length);
            Console.ReadKey();
        }

        private static int SequentialLoop(IEnumerable<string> names, BinarySearchTree strTree)
        {
            return names.Count(strTree.Contains);
        }

        // https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx
        private static int ParallelTaskLoop(IReadOnlyList<string> names, BinarySearchTree strTree)
        {
            const int countCpu = 8;
            var tasks = new Task<int>[countCpu];
            for (var j = 0; j < countCpu; j++)
                tasks[j] = Task<int>.Factory.StartNew(
                    p =>
                    {
                        var count = 0;
                        for (var i = (int)p; i < names.Count; i += countCpu)
                            if (strTree.Contains(names[i]))
                                count++;
                        return count;
                    }, j);
            var total = 0;
            for (var i = 0; i < countCpu; i++) total += tasks[i].Result;
            return total;
        }

        static string RandomName(int size)
        {
            var builder = new StringBuilder();
            var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() +
                                                               65)));
            builder.Append(ch);
            for (var i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() +
                                                               97)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}

