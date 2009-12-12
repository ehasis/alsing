using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alsing.Core;

namespace Alsing.Core
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this ICustomAttributeProvider self)
        {
            return self
                .GetCustomAttributes(typeof(T), true)
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

        public static string GetTypeName(this Type type)
        {
            if (type.IsGenericType)
            {

                var argNames = type
                    .GetGenericArguments()
                    .Select(argType => GetTypeName(argType))
                    .ToArray();

                string args = string.Join(",", argNames);

                string typeName = type.Name;
                int index = typeName.IndexOf("`");
                typeName = typeName.Substring(0, index);

                return string.Format("{0}[of {1}]", typeName, args);
            }
            return type.Name;
        }

        public static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            Type currentType = type;
            while (currentType != null)
            {
                FieldInfo[] fields =
                    currentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
                foreach (FieldInfo fieldInfo in fields)
                {
                    yield return fieldInfo;
                }
                currentType = currentType.BaseType;
            }
        }
    }
}
