using System.Collections;
using System.Collections.Generic;

namespace Alsing.Core
{
    public static class Range
    {
        public static IEnumerable<T> From<T>(T end)
        {
            return new GenericRange<T>(end);
        }

        public static IEnumerable<T> From<T>(T start, T end)
        {
            return new GenericRange<T>(start, end);
        }
    }

    internal class GenericRange<T> : IEnumerable<T>
    {
        private readonly T end;
        private readonly T start;

        public GenericRange(T end)
        {
            this.end = end;
        }

        public GenericRange(T start, T end)
        {
            this.start = start;
            this.end = end;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Numeric<T> i = start;
            if (i <= end)
                while (i <= end) yield return i++;
            else
                while (i >= end) yield return i--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}