using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Lists
{
    public class Set<T> : ISet<T>
    {
        private HashSet<T> internalSet = new HashSet<T>();

        public void Add(T item)
        {
            internalSet.Add(item);
        }

        public void Remove(T item)
        {
            internalSet.Remove(item);
        }

        public bool Contains(T item)
        {
            return internalSet.Contains(item);
        }

        public int Count
        {
            get 
            {
                return internalSet.Count;
            }
        }
    }
}
