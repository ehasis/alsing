using System.Linq;

namespace Alsing
{
    public static class GeneralExtensions
    {
        public static bool IsAnyOf<T>(this T item, params T[] items)
        {
            return items.Contains(item);
        }
    }
}