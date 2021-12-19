using System.Diagnostics;

namespace Testing
{
    class Timer
    {
        private readonly Stopwatch _stopwatch;

        public Timer()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public string Stop()
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.Elapsed;
            var time = $"{elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds}";
            Reset();
            return time;
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }
    }
}
