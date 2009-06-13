namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;

    using Reflection;

    public class ProxyTypeBuilder
    {
        private readonly IList<FieldBuilder> fieldBuilders = new List<FieldBuilder>();

        private readonly IDictionary<Type, Type[]> interfaceToMixinMap = new Dictionary<Type, Type[]>();

        private AssemblyBuilder assemblyBuilder;

        private Type compositeType;

        private Type[] interfaces;

        private TypeBuilder typeBuilder;


        public Type BuildProxyType(Type compositeType)
        {
            this.compositeType = compositeType;

            this.CreateInterfaceList();

            this.CreateAssemblyBuilder();
            this.CreateTypeBuilder();

            this.CreateMixinList();
            this.CreateMixinFields();

            foreach (Type type in this.interfaces)
            {
                foreach (MethodInfo method in type.GetAllMethods())
                {
                    this.CreateMethod(method);
                }
            }

            this.CreateDefaultCtor();

            Type proxyType = this.typeBuilder.CreateType();

            return proxyType;
        }

        private static void CreateDelegatedMixinMethod(MethodInfo method, ILGenerator generator, FieldBuilder fieldBuilder)
        {
            //delegate calls to fieldbuilder 
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, fieldBuilder);

            ParameterInfo[] paramArray = method.GetParameters();
            int paramCount = paramArray.Length;

            for (int i = 0; i < paramCount; i++)
            {
                generator.Emit(OpCodes.Ldarg, i + 1);
            }
            generator.Emit(OpCodes.Callvirt, method);

            generator.Emit(OpCodes.Ret);
        }

        private static void CreateInvocationHandlerMethod(MethodInfo method, ILGenerator generator, FieldBuilder fieldBuilder)
        {
            MethodInfo invokeMethod = typeof(InvocationHandler).GetMethod("Invoke");
            MethodInfo getFromCacheMethod = typeof(MethodInfoCache).GetMethod("GetMethod");

            int methodId = MethodInfoCache.AddMethod(method);

            ParameterInfo[] paramInfos = method.GetParameters();
            IEnumerable<Type> paramTypes = paramInfos.Select(p => p.ParameterType);
            int paramCount = method.GetParameters().Length;

            // Build parameter object array
            LocalBuilder paramArray = generator.DeclareLocal(typeof(object[]));
            generator.Emit(OpCodes.Ldc_I4_S, paramCount);
            generator.Emit(OpCodes.Newarr, typeof(object));
            generator.Emit(OpCodes.Stloc, paramArray);
            //---

            int j = 0;

            //Fill parameter object array
            foreach (Type parameterType in paramTypes)
            {
                //load arr
                generator.Emit(OpCodes.Ldloc, paramArray);
                //load index
                generator.Emit(OpCodes.Ldc_I4, j);
                //load arg
                generator.Emit(OpCodes.Ldarg, j + 1);
                //box if needed
                if (parameterType.IsByRef)
                {
                    generator.Emit(OpCodes.Ldind_Ref);
                    Type t = parameterType.GetElementType();
                    if (t.IsValueType)
                    {
                        generator.Emit(OpCodes.Box, t);
                    }
                }
                else if (parameterType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, parameterType);
                }
                generator.Emit(OpCodes.Stelem_Ref);
                j++;
            }

            //This .
            generator.Emit(OpCodes.Ldarg_0);
            //Mixin .
            generator.Emit(OpCodes.Ldfld, fieldBuilder);
            // param 1 = this
            generator.Emit(OpCodes.Ldarg_0);
            // param 2 = methodinfo

            generator.Emit(OpCodes.Ldc_I4, methodId);
            generator.Emit(OpCodes.Call, getFromCacheMethod);

            // param 3 = parameter array
            generator.Emit(OpCodes.Ldloc, paramArray);

            //call this.mixin.invoke(this,methodinfo,paramArray);
            generator.Emit(OpCodes.Callvirt, invokeMethod);

            if (method.ReturnType == typeof(void))
            {
                generator.Emit(OpCodes.Pop);
            }
            else if (method.ReturnType.IsValueType)
            {
                generator.Emit(OpCodes.Unbox, method.ReturnType);
                generator.Emit(OpCodes.Ldobj, method.ReturnType);
            }

            generator.Emit(OpCodes.Ret);
        }

        private static void CreateNoOpMethod(MethodInfo method, ILGenerator generator)
        {
            if (method.ReturnType != typeof(void))
            {
                LocalBuilder local = generator.DeclareLocal(method.ReturnType);
                local.SetLocalSymInfo("FakeReturn");
                generator.Emit(OpCodes.Ldloc, local);
            }
            generator.Emit(OpCodes.Ret);
        }


        private void CreateAssemblyBuilder()
        {
            AppDomain domain = Thread.GetDomain();
            var assemblyName = new AssemblyName
                                   {
                                           Name = Guid.NewGuid().ToString(),
                                           Version = new Version(1, 0, 0, 0)
                                   };

            this.assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        private void CreateDefaultCtor()
        {
            ConstructorBuilder ctorBuilder = this.typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[]
                                                                                                                                          {
                                                                                                                                          });
            ILGenerator generator = ctorBuilder.GetILGenerator();

            foreach (FieldBuilder field in this.fieldBuilders)
            {
                ConstructorInfo ctor = field.FieldType.GetConstructor(new Type[]
                                                                          {
                                                                          });
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Newobj, ctor);
                generator.Emit(OpCodes.Stfld, field);
            }

            generator.Emit(OpCodes.Ret);
        }

        private void CreateInterfaceList()
        {
            Type[] all = this.compositeType.GetAllInterfaces().ToArray();
            this.interfaces = all;
        }

        private void CreateMethod(MethodInfo method)
        {
            MethodBuilder methodBuilder = this.typeBuilder.GetMethodOverrideBuilder(method);

            ILGenerator generator = methodBuilder.GetILGenerator();

            FieldBuilder fieldBuilder = this.GetDelegatingFieldBuilder(method);

            //If possible, delegate call to mixin implementation
            if (fieldBuilder != null)
            {
                CreateDelegatedMixinMethod(method, generator, fieldBuilder);
                return;
            }

            fieldBuilder = this.GetInterceptorFieldBuilder(method);

            //If possible, delegate call to mixin interceptor
            if (fieldBuilder != null)
            {
                CreateInvocationHandlerMethod(method, generator, fieldBuilder);
                return;
            }

            //else just No Op it
            CreateNoOpMethod(method, generator);
        }

        private void CreateMixinFields()
        {
            foreach (Type interfaceType in this.interfaceToMixinMap.Keys)
            {
                foreach (Type mixinType in this.interfaceToMixinMap[interfaceType])
                {
                    Type fieldType = mixinType;

                    string mixinFieldName = string.Format("field {0}", fieldType.GetTypeName());

                    //TODO: ensure unique name

                    FieldBuilder mixinFieldBuilder = this.typeBuilder.DefineField(mixinFieldName, fieldType, FieldAttributes.Public);

                    this.fieldBuilders.Add(mixinFieldBuilder);
                }
            }
        }

        private void CreateMixinList()
        {
            foreach (Type interfaceType in this.interfaces)
            {
                IEnumerable<Type> mixins = from attribute in interfaceType.GetCustomAttributes(typeof(MixinsAttribute), true).Cast<MixinsAttribute>()
                                           from mixinType in attribute.MixinTypes
                                           select mixinType;

                var genericMixins = new List<Type>();
                foreach (Type mixinType in mixins)
                {
                    Type genericType = mixinType;

                    //handle generic type mixins
                    if (mixinType.IsGenericTypeDefinition)
                    {
                        Type[] generics = interfaceType.GetGenericArguments();
                        genericType = mixinType.MakeGenericType(generics);
                    }

                    genericMixins.Add(genericType);
                }

                this.interfaceToMixinMap.Add(interfaceType, genericMixins.ToArray());
            }
        }


        private void CreateTypeBuilder()
        {
            const string moduleName = "Alsing.Proxy";
            const string nameSpace = "Alsing.Proxy";
            string typeName = string.Format("{0}.{1}", nameSpace, this.compositeType.GetTypeName());

            ModuleBuilder moduleBuilder = this.assemblyBuilder.DefineDynamicModule(moduleName, true);
            const TypeAttributes typeAttributes = TypeAttributes.Class | TypeAttributes.Public;

            this.typeBuilder = moduleBuilder.DefineType(typeName, typeAttributes, typeof(object), this.interfaces);
        }

        private FieldBuilder GetDelegatingFieldBuilder(MethodInfo method)
        {
            FieldBuilder fieldBuilder = (from fb in this.fieldBuilders
                                         from i in fb.FieldType.GetInterfaces()
                                         where i == method.DeclaringType
                                         select fb).FirstOrDefault();

            return fieldBuilder;
        }

        private FieldBuilder GetInterceptorFieldBuilder(MethodInfo method)
        {
            FieldBuilder fieldBuilder = (from fb in this.fieldBuilders
                                         where typeof(InvocationHandler).IsAssignableFrom(fb.FieldType)
                                         from a in fb.FieldType.GetCustomAttributes(typeof(AppliesToAttribute), true).Cast<AppliesToAttribute>()
                                         from t in a.AppliesToTypes
                                         let f = Activator.CreateInstance(t, null) as AppliesToFilter
                                         where f.AppliesTo(method, fb.FieldType, this.compositeType, null)
                                         select fb).FirstOrDefault();
            return fieldBuilder;
        }
    }
}