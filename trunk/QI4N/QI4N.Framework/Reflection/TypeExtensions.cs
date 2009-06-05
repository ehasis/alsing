namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAllInterfaces(this Type interfaceType)
        {
            yield return interfaceType;
            foreach (Type type in interfaceType.GetInterfaces())
            {
                yield return type;
            }
        }

        public static PropertyInfo GetInterfaceProperty(this Type interfaceType, string propertyName)
        {
            PropertyInfo propertyInfo = (
                                                from i in interfaceType.GetAllInterfaces()
                                                select i.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public)
                                        ).FirstOrDefault();

            return propertyInfo;
        }

        public static string GetTypeName(this Type type)
        {
            if (type.IsGenericType)
            {

                var argNames = (from argType in type.GetGenericArguments()
                                select GetTypeName(argType)).ToArray();

                string args = string.Join(",", argNames);

                string typeName = type.Name;
                int index = typeName.IndexOf("`");
                typeName = typeName.Substring(0, index);

                return string.Format("{0}[of {1}]", typeName, args);
            }
            return type.Name;
        }
    }
}