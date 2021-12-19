using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _3HashSearch
{
    class Program
    {
        private static readonly Random Rnd = new Random();
        private static readonly Counter Cnt = new Counter();
        private static int _n = 8388608; // 1048576

        public static void Main(string[] args)
        {
            Console.WriteLine("\n File name or leave blank to generate:");

            var keys = new List<string>();
            var values = new List<string>();

            keys = GetStringList(_n);
            values = GetStringList(_n);

            StartHashTable(keys, values);

            Console.WriteLine("\n --=< Done >=--");
            keys.Clear();
            values.Clear();
            GC.Collect();
            Console.ReadKey();
        }

        private static void StartHashTable(List<string> keys, List<string> values)
        {
            var table = new HashTable(_n * 4);
            Console.WriteLine(" Creating table");

            for (var i = 0; i < _n; i++)
            {
                table.Add(keys[i], values[i]);
            }
            
            Console.WriteLine(" {0,5} {1,10} {2,12} {3,12} {4,12} {5,12}", "Nr", "N", "ST Time (ms)", "ST Counter", "MT Time (ms)", "MT Counter");
            Console.WriteLine(" ====================================================================");
            var k = 1;
            for (var i = 256; i <= _n; i *= 2)
            {
                Cnt.Reset();
                Cnt.Start();
                SearchTask(1, table, keys, i);
                Cnt.Stop();

                var duration1 = Cnt.GetTime();
                var count1 = Cnt.GetCount();

                Cnt.Reset();
                Cnt.Start();
                MultiThreadSearch(table, keys, i);
                Cnt.Stop();
         
                var duration2 = Cnt.GetTime();
                var count2 = Cnt.GetCount();

                Console.WriteLine(" {0, 5} {1, 10} {2, 12} {3, 12} {4, 12} {5, 12}", k++, i, duration1, count1, duration2, count2);
            }
        }

        private static void MultiThreadSearch(HashTable table, List<string> keys, int count)
        {
            const int threads = 8;

            var tasks = new Task[threads];

            for (var j = 0; j < threads; j++)
            {
                tasks[j] = Task.Factory.StartNew(
                    () =>
                    {
                        SearchTask(threads, table, keys, count);
                    });
            }
            Cnt.Add(3 + threads * 3);
            Task.WaitAll(tasks);
        }

        private static void SearchTask(int threads, HashTable table, List<string> keys, int count)
        {
            Cnt.Add();
            for (var i = 0; i < count; i += threads)
            {
                table.Get(keys[i]);
            }
            Cnt.Add(count / threads);
        }
        
        public static List<string> GetStringList(int count)
        {
            var strings = new List<string>();

            for (var i = 0; i < count; i++)
            {
                strings.Add(GetUniqueString2(12));
            }

            return strings;
        }

        public static string GetUniqueString(int size)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string GetUniqueString2(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[size];

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Rnd.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public static void Shuffle(IList<string> list)
        {
            var rng = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static int Hash2(string value)
        {
            var _sha = SHA256.Create();
            var hashed = _sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Math.Abs(BitConverter.ToInt32(hashed, 0));
        }
    }
}
