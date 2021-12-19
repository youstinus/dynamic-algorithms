using System.Diagnostics;

namespace _1Recurrence
{
    public class Counter
    {
        private long _count;
        private readonly Stopwatch _stopwatch;

        public Counter()
        {
            _stopwatch = new Stopwatch();
            _count = 0;
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    
        public long GetTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        public long GetTicks()
        {
            return _stopwatch.ElapsedTicks;
        }

        public void Add(long count = 1)
        {
            _count += count;
        }

        public long GetCount()
        {
            return _count;
        }

        public void Reset()
        {
            _count = 0;
            _stopwatch.Reset();
        }
    }
}
