namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class TypeExtensions
    {

        public static object GetDefaultValue(this Type self)
        {
            Array a = Array.CreateInstance(self, 1);
            var defaultValue = a.GetValue(0);
            return defaultValue;
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public static FieldInfo[] GetAllFields(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic;

            FieldInfo[] ownFields = type.GetFields(flags).ToArray();

            return ownFields;
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

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public static IEnumerable<Type> GetAllInterfaces(this Type targetType)
        {
            var types = new List<Type>();
            if (targetType.IsInterface)
            {
                types.Add(targetType);
            }

            types.AddRange(targetType.GetInterfaces());
            return types;
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic;

            MethodInfo[] ownMethods = type.GetMethods(flags);

            return ownMethods;
        }

        //public static Type[] GetAppliesToTypes(this Type mixinType)
        //{
        //    IEnumerable<Type> appliesTo = from attribs in mixinType.GetCustomAttributes(typeof(AppliesToAttribute), true).Cast<AppliesToAttribute>()
        //                                  from type in attribs.AppliesToTypes
        //                                  select type;

        //    return appliesTo.ToArray();
        //}

        public static T GetAttribute<T>(this ICustomAttributeProvider self) where T : Attribute
        {
            var attrib = self.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
            return attrib;
        }

        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider self) where T : Attribute
        {
            IEnumerable<T> attribs = self.GetCustomAttributes(typeof(T), true).Cast<T>();
            return attribs;
        }

        public static PropertyInfo GetInterfaceProperty(this Type interfaceType, string propertyName)
        {
            PropertyInfo propertyInfo = (
                                                from i in interfaceType.GetAllInterfaces()
                                                select i.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public)
                                        ).FirstOrDefault();

            return propertyInfo;
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
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
                    .Select<ParameterInfo, Type>(SelectParameterType)
                    .ToArray();

            MethodBuilder methodBuilder = typeBuilder
                    .DefineMethod(methodName, methodAttributes, CallingConventions.Standard, method.ReturnType, parameters);

            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();

                string[] genericNames = genericArguments
                    .Select(a => a.Name)
                    .ToArray();

                var genericParameters = methodBuilder.DefineGenericParameters(genericNames);
            }

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

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
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

        public static bool HasAttribute<T>(this ICustomAttributeProvider self) where T : Attribute
        {
            return self.GetCustomAttributes(typeof(T), true).Any();
        }

        public static bool HasAttribute(this ICustomAttributeProvider self, Type attributeType)
        {
            return self.GetCustomAttributes(attributeType, true).Any();
        }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public static object NewInstance(this Type self)
        {
            return Activator.CreateInstance(self, null);
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        private static Type SelectParameterType(ParameterInfo param)
        {
            return param.ParameterType;
        }


        public static MethodInfo ToDefinition(this MethodInfo self)
        {
            if (self.IsGenericMethod)
                return self.GetGenericMethodDefinition();

            return self;
        }
    }
}