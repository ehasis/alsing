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

        public static FieldInfo GetAnyField(this Type type,string fieldName)
        {
            return GetFieldInfo(type, fieldName);
        }

        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            if (type == null)
                return null;

            FieldInfo field = type.GetField(fieldName,
                                            BindingFlags.Public |
                                            BindingFlags.NonPublic |
                                            BindingFlags.Instance);

            return field ?? GetFieldInfo(type.BaseType, fieldName);
        }
    }
}
