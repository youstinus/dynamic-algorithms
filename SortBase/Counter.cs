namespace SortBase
{
    public static class Counter
    {
        private static long _count;

        public static void Add(long count = 1)
        {
            _count += count;
        }

        public static long Get()
        {
            return _count;
        }

        public static void Reset()
        {
            _count = 0;
        }
    }
}
