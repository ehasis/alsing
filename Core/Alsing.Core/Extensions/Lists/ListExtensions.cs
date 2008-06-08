using System.Collections.Generic;

namespace Alsing
{
    public struct ListEntry<T>
    {
        public readonly int Index;
        public readonly T Item;
        public readonly bool IsFirst;
        public readonly bool IsLast;

        public ListEntry(T item, int index, bool isFirst, bool isLast)
        {
            Item = item;
            Index = index;
            IsFirst = isFirst;
            IsLast = isLast;
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
            for (int i = 0; i < list.Count; i++)
                yield return new ListEntry<T>(list[i], i, i == 0, i == list.Count - 1);
        }
    }
}
