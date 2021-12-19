using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortBase;

namespace Testing
{
    public static class Helper
    {
        private static Random _random = new Random();

        public static List<string> GetStringList(int count)
        {
            var strings = new List<string>();

            for (var i = 0; i < count; i++)
            {
                strings.Add(GetUniqueString2(12));
                //strings.Add(GetUniqueString(12));
            }

            return strings;
        }
        private static string GetUniqueString2(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[size];

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public static Numbers[] GenerateNumbersArray(int n, int seed)
        {
            var data = new Numbers[n];
            var rand = new Random(seed);

            for (var i = 0; i < n; i++)
                data[i] = new Numbers(rand.NextDouble(), rand.Next());

            return data;
        }
    }
}
