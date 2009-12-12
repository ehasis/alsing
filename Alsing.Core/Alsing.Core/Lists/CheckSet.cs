using System.Collections.Generic;

namespace Alsing.Collections
{
    public class CheckSet<T>
    {
        private readonly Dictionary<T, bool> lookup = new Dictionary<T, bool>();

        public bool IsChecked(T item)
        {
            bool res;
            lookup.TryGetValue(item, out res);
            return res;
        }

        public void Check(T item)
        {
            lookup[item] = true;
        }

        public void UnCheck(T item)
        {
            lookup[item] = false;
        }
    }
}