using System.Collections.Generic;
using System;
using System.Linq;
namespace Alsing.Core
{
    public struct ListEntry<T>
    {
        public readonly int Index;
        public readonly T Item;
        public readonly bool IsFirst;
        public readonly bool IsLast;
        public readonly TimeSpan AverageIterationInterval;
        public readonly DateTime EstimatedCompletion;

        public ListEntry(T item, int index, bool isFirst, bool isLast,TimeSpan averageIterationInterval,DateTime estimatedCompletion)
        {
            Item = item;
            Index = index;
            IsFirst = isFirst;
            IsLast = isLast;
            AverageIterationInterval = averageIterationInterval;
            EstimatedCompletion = estimatedCompletion;
        }
    }

    public static class ListExtensions
    {
        public static bool HasContent<T>(this IList<T> list)
        {
            return list.Count > 0;
        }

        public static bool HasContent<T>(this T[] array)
        {
            return array.Length > 0;
        }

        public static IEnumerable<ListEntry<T>> GetEntries<T>(this IList<T> list)
        {
            DateTime start = DateTime.Now;
            for (int i = 0; i < list.Count; i++)
            {
                TimeSpan totalTime = DateTime.Now - start;
                TimeSpan averageTime = new TimeSpan( totalTime.Ticks / (i + 1));
                TimeSpan totalSpan = new TimeSpan(averageTime.Ticks * list.Count);
                DateTime estimatedCompletion = start + totalSpan;

                yield return new ListEntry<T>(list[i], i, i == 0, i == list.Count - 1,averageTime,estimatedCompletion);
            }
        }
    }
}
