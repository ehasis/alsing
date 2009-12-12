using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing
{
    public interface ISet<T>
    {
        void Add(T item);
        void Remove(T item);
        bool Contains(T item);
        int Count { get; }
    }
}
