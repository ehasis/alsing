using System;
using System.Linq;
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

            var allFields = from f in type.GetFields(BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance)
                            where f.Name.ToLowerInvariant() == fieldName.ToLowerInvariant()
                            select f;

            FieldInfo field = allFields.FirstOrDefault();
            if (field != null)
                return field;

            return GetFieldInfo(type.BaseType, fieldName);
        }
    }
}
