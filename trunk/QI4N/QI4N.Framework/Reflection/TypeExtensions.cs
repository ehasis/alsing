namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class TypeExtensions
    {
        public static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic;

            FieldInfo[] ownFields = type.GetFields(flags).ToArray();

            foreach (FieldInfo field in ownFields)
            {
                yield return field;
            }
        }

        public static IEnumerable<MethodInfo> GetAllInterfaceMethods(this Type self)
        {
            foreach (Type type in self.GetAllInterfaces())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    yield return methodInfo;
                }
            }
        }

        public static IEnumerable<Type> GetAllInterfaces(this Type targetType)
        {
            if (targetType.IsInterface)
            {
                yield return targetType;
            }

            foreach (Type type in targetType.GetInterfaces())
            {
                yield return type;
            }
        }

        public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic;

            MethodInfo[] ownMethods = type.GetMethods(flags)
                    .ToArray();

            foreach (MethodInfo method in ownMethods)
            {
                yield return method;
            }
        }

        public static Type[] GetAppliesToTypes(this Type mixinType)
        {
            IEnumerable<Type> appliesTo = from attribs in mixinType.GetCustomAttributes(typeof(AppliesToAttribute), true).Cast<AppliesToAttribute>()
                                          from type in attribs.AppliesToTypes
                                          select type;

            return appliesTo.ToArray();
        }

        public static T GetAttribute<T>(this Type self) where T : Attribute
        {
            var attrib = self.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
            return attrib;
        }

        public static IEnumerable<T> GetAttributes<T>(this Type self) where T : Attribute
        {
            IEnumerable<T> attribs = self.GetCustomAttributes(typeof(T), true).Cast<T>();

            foreach (T attrib in attribs)
            {
                yield return attrib;
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

        public static MethodBuilder GetMethodOverrideBuilder(this TypeBuilder typeBuilder, MethodInfo method)
        {
            const MethodAttributes methodAttributes = MethodAttributes.NewSlot |
                                                      MethodAttributes.Private |
                                                      MethodAttributes.Virtual |
                                                      MethodAttributes.Final |
                                                      MethodAttributes.HideBySig;

            string methodName = string.Format("{1} in {0}", method.DeclaringType.Name, method.Name);
            Type[] parameters = method
                    .GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray();

            MethodBuilder methodBuilder = typeBuilder
                    .DefineMethod(methodName, methodAttributes, CallingConventions.Standard, method.ReturnType, parameters);

            typeBuilder.DefineMethodOverride(methodBuilder, method);
            return methodBuilder;
        }

        public static Type[] GetMixinTypes(this Type mixinType)
        {
            IEnumerable<Type> appliesTo = from attribs in mixinType.GetCustomAttributes(typeof(MixinsAttribute), true).Cast<MixinsAttribute>()
                                          from type in attribs.MixinTypes
                                          select type;

            return appliesTo.ToArray();
        }

        public static string GetTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                string[] argNames = (from argType in type.GetGenericArguments()
                                     select GetTypeName(argType)).ToArray();

                string args = string.Join(",", argNames);

                string typeName = type.Name;
                int index = typeName.IndexOf("`");
                typeName = typeName.Substring(0, index);

                return string.Format("{0}[of {1}]", typeName, args);
            }
            return type.Name;
        }

        public static bool HasAttribute(this FieldInfo self, Type attributeType)
        {
            return self.GetCustomAttributes(attributeType, true).Any();
        }

        public static bool HasAttribute(this Type self, Type attributeType)
        {
            return self.GetCustomAttributes(attributeType, true).Any();
        }

        public static bool HasAttribute(this MethodInfo self, Type attributeType)
        {
            return self.GetCustomAttributes(attributeType, true).Any();
        }

        public static object NewInstance(this Type self)
        {
            return Activator.CreateInstance(self, null);
        }
    }
}