using System;
using System.Reflection;

namespace Alsing
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this MethodBase method)
        {
            return method
                .GetCustomAttributes(typeof (T), true)
                .HasContent();
        }

        public static bool HasAttribute<T>(this Type type)
        {
            return type
                .GetCustomAttributes(typeof (T), true)
                .HasContent();
        }
    }
}
