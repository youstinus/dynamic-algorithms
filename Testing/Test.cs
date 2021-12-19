using System;
using System.IO;
using System.Linq;
using SortBase;

namespace Testing
{
    public class Test
    {
        private readonly int[] _counts19 = { 50000000, 100000000, 150000000, 200000000, 250000000, 300000000, 350000000, 400000000, 450000000, 500000000, 550000000 };
        private readonly int[] _counts18 = { 20000000, 40000000, 60000000, 80000000, 100000000, 120000000, 140000000, 160000000, 180000000, 200000000 };
        private readonly int[] _counts17 = { 10000000, 20000000, 30000000, 40000000, 50000000, 60000000, 70000000, 80000000, 90000000, 100000000 };
        private readonly int[] _counts16 = { 5000000, 10000000, 15000000, 20000000, 25000000, 30000000, 35000000, 40000000, 45000000, 50000000, 55000000};
        private readonly int[] _counts15 = { 2000000, 4000000, 6000000, 8000000, 10000000, 12000000, 14000000, 16000000, 18000000, 20000000};
        private readonly int[] _counts14 = { 1000000, 2000000, 3000000, 4000000, 5000000, 6000000, 7000000, 8000000, 9000000, 10000000, 11000000};
        private readonly int[] _counts13 = { 500000, 1000000, 1500000, 2000000, 2500000, 3000000, 3500000, 4000000, 4500000, 5000000, 5500000, 6000000};
        private readonly int[] _counts12 = { 200000, 400000, 600000, 800000, 1000000, 1200000, 1400000, 1600000, 1800000, 2000000, 2200000, 2400000};
        private readonly int[] _counts11 = { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000, 1100000, 1200000};
        private readonly int[] _counts10 = {50000, 100000, 150000, 200000, 250000, 300000, 350000, 400000, 450000, 500000, 550000, 600000, 650000, 700000};
        private readonly int[] _counts9 = {10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000, 110000, 120000, 130000, 140000, 150000};
        private readonly int[] _counts8 = {5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000};
        private readonly int[] _counts7 = {3000, 6000, 9000, 12000, 15000, 18000, 21000, 24000, 27000, 30000, 33000, 36000, 39000};
        private readonly int[] _counts6 = {2000, 4000, 6000, 8000, 10000, 12000, 14000, 16000, 18000, 20000, 22000, 24000, 26000};
        private readonly int[] _counts5 = {1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000, 15000};
        private readonly int[] _counts4 = {500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500, 9000};
        private readonly int[] _counts3 = {200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800, 3000, 3200, 3400, 3600, 3800, 4000};
        private readonly int[] _counts2 = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000};
        private readonly int[] _counts1 = {20, 40, 60, 80, 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 360, 380, 400};
        private int[] _counts;

        private readonly int _seed;
        private readonly Timer _timer;

        private StreamWriter _output = new StreamWriter(File.Open("output.txt", FileMode.Append, FileAccess.Write));

        public Test()
        {
            _seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            _timer = new Timer();
        }

        public void CloseFile()
        {
            _output.Close();
        }

        public void StartTests()
        {
            Console.WriteLine("\n 1. TestMergeSortArrayOp\n 2. TestMergeSortListOp\n 3. TestMergeSortArrayD\n 4. TestMergeSortListD\n 5. TestInsertionSortArrayOp\n 6. TestInsertionSortListOp\n 7. TestInsertionSortArrayD\n 8. TestInsertionSortListD\n 9. TestHashTableSearchOp\n 10. TestHashTableSearchD");
            var which = Console.ReadLine();

            Console.WriteLine("\n Data counts:\n 1. Up to 400\n 2. Up to 2000\n 3. Up to 4000\n 4. Up to 9000\n 5. Up to 15000\n 6. Up to 26000\n 7. Up to 39000\n 8. Up to 60000\n 9. Up to 150000\n10. Up to 700000\n11. Up to 1200000\n12. Up to 2400000\n13. Up to 6000000\n14. Up to 11000000\n15. Up to 20000000\n16. Up to 55000000\n17. Up to 100000000\n18. Up to 200000000\n19. Up to 550000000");
            var counters = Console.ReadLine();

            try
            {
                switch (counters)
                {
                    case "1":
                        _counts = _counts1;
                        break;
                    case "2":
                        _counts = _counts2;
                        break;
                    case "3":
                        _counts = _counts3;
                        break;
                    case "4":
                        _counts = _counts4;
                        break;
                    case "5":
                        _counts = _counts5;
                        break;
                    case "6":
                        _counts = _counts6;
                        break;
                    case "7":
                        _counts = _counts7;
                        break;
                    case "8":
                        _counts = _counts8;
                        break;
                    case "9":
                        _counts = _counts9;
                        break;
                    case "10":
                        _counts = _counts10;
                        break;
                    case "11":
                        _counts = _counts11;
                        break;
                    case "12":
                        _counts = _counts12;
                        break;
                    case "13":
                        _counts = _counts13;
                        break;
                    case "14":
                        _counts = _counts14;
                        break;
                    case "15":
                        _counts = _counts15;
                        break;
                    case "16":
                        _counts = _counts16;
                        break;
                    case "17":
                        _counts = _counts17;
                        break;
                    case "18":
                        _counts = _counts18;
                        break;
                    case "19":
                        _counts = _counts19;
                        break;
                    default:
                        throw new InvalidOperationException("Ending tests");
                }

                TestHashTableSearchOp();
                
                StartTests();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private void TestHashTableSearchOp()
        {
            var keys = Helper.GetStringList(_counts.Last());
            var values = Helper.GetStringList(_counts.Last());

            var title = "\n HASHTABLE OP";
            var headers = $" {"N",10}{"TIME",16}{"OPERATIONS",22}\n";
            Console.WriteLine(title);
            Console.WriteLine(headers);
            _output.WriteLine(title);
            _output.WriteLine(headers);

            foreach (var count in _counts)
            {
                var table = new _3HashSearch.HashTable(count);
                Counter.Reset();
                _timer.Start();
                for (var i = 0; i < count; i++)
                {
                    table.Add(keys[i], values[i]);
                }
                var timeString = _timer.Stop();

                var output = $" {count,10}{timeString,16}{Counter.Get(),22}";
                var output2 = $"{count};{timeString};{Counter.Get()}";
                Console.WriteLine(output);
                _output.WriteLine(output2);

                Counter.Reset();
                _timer.Start();
                for (var i = 0; i < count; i++)
                {
                    var result = table.Get(keys[i]);
                    if(result == null)
                        Console.Write("#");
                }
                timeString = _timer.Stop();

                output = $" {count,10}{timeString,16}{Counter.Get(),22}";
                output2 = $"{count};{timeString};{Counter.Get()}";
                Console.WriteLine(output);
                _output.WriteLine(output2);

                Counter.Reset();
            }

            var ending = new string('-', 20);
            Console.WriteLine(ending);
            _output.WriteLine(ending);
        }        
    }
}
