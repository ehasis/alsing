using System;
using System.Collections;
using System.ComponentModel;

namespace Alsing.Serialization.Extensions
{
    public static class Extensions
    {
        public static bool IsList(this object item)
        {
            return item is IList;
        }

        public static bool IsDictionary(this object item)
        {
            return item is IDictionary;
        }

        public static bool IsArray(this object item)
        {
            return item.GetType().IsArray;
        }

        public static bool IsValueObject(this object item)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            return tc.CanConvertFrom(typeof(string)) || item is Type;
        }
    }
}
