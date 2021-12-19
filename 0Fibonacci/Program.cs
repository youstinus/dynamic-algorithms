using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _0Fibonacci
{
    class CustomData
    {
        public int TNum;
        public int TResult;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var n = 45;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var fibnum = F1(n);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for sequential Fibonacci: {0,6:N0} ",
                stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Fibonacci( {0,4:N0} ) = {1,9:N0}", n, fibnum);
            stopWatch.Reset();
            stopWatch.Start();
            fibnum = F2(n);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for parallel Fibonacci: {0,6:N0} ",
                stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Fibonacci( {0,4:N0} ) = {1,9:N0}", n, fibnum);
            Console.ReadKey();
        }
        static int F1(int n)
        {
            if (n > 1) return F1(n - 1) + F1(n - 2);
            else return 1;
        }
        static int F2(int n)
        {
            var fibnum = 0;
            if (n < 6) fibnum = F1(n);
            else
            {
                //fibnum = F1(n - 3) + 3 * F1(n - 4) + 3 * F1(n - 5) + F1(n - 6);
                var countCPU = 4;
                var tasks = new Task[countCPU];
                for (var j = 0; j < countCPU; j++)
                    tasks[j] = Task.Factory.StartNew(
                        (Object p) =>
                        {
                            var data = p as CustomData; if (data == null) return;
                            data.TResult = F1(n - data.TNum - 3);
                        },
                        new CustomData() { TNum = j });
                Task.WaitAll(tasks);
                fibnum = (tasks[0].AsyncState as CustomData).TResult
                         + 3 * (tasks[1].AsyncState as CustomData).TResult
                         + 3 * (tasks[2].AsyncState as CustomData).TResult
                         + (tasks[3].AsyncState as CustomData).TResult;
            }
            return fibnum;
        }
    }
}
