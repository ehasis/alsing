using System;
namespace Alsing.Lists
{
    interface ICheckSet<T>
    {
        void Check(T item);
        bool IsChecked(T item);
        void UnCheck(T item);
        bool this[T item] { get; set; }
    }
}
