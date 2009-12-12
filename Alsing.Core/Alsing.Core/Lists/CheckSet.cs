using System.Collections.Generic;
using System;

namespace Alsing.Collections
{
    public class CheckSet<T> : Alsing.Lists.ICheckSet<T>
    {
        private readonly Dictionary<T, bool> lookup = new Dictionary<T, bool>();

        public bool IsChecked(T item)
        {
            bool res;
            lookup.TryGetValue(item, out res);
            return res;
        }

        public bool this[T item]
        {
            get
            {
                return this.IsChecked(item);
            }
            set
            {
                this.Check(item);
            }
        }

        public void Check(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            lookup[item] = true;
        }

        public void UnCheck(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            lookup[item] = false;
        }
    }
}