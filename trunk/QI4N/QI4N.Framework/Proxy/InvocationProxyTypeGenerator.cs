namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;

    internal class InvocationProxyTypeBuilder
    {
        private AssemblyBuilder assemblyBuilder;

        private Type compositeType;

        private FieldBuilder defaultHandlerFieldBuilder;

        private Type[] interfaces;

        private TypeBuilder typeBuilder;

        private Type[] additionalTypes;

        private static readonly object syncRoot = new object();

        private static IDictionary<Type, Type> typeCache = new Dictionary<Type, Type>();

        public Type BuildProxyType(Type compositeType,Type[] additionalTypes)
        {
            lock (syncRoot)
            {
                Type cachedProxyType;
                if (typeCache.TryGetValue(compositeType, out cachedProxyType))
                {
                    return cachedProxyType;
                }
                this.compositeType = compositeType;
                this.additionalTypes = additionalTypes;

                this.CreateInterfaceList();

                this.CreateAssemblyBuilder();
                this.CreateTypeBuilder();

                this.CreateDefaultHandlerField();

                foreach (Type type in this.interfaces)
                {
                    foreach (MethodInfo method in type.GetAllMethods())
                    {
                        this.CreateMethod(method);
                    }
                }

                this.CreateCtor();

                Type proxyType = this.typeBuilder.CreateType();

                typeCache.Add(compositeType,proxyType);

                return proxyType;                
            }
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

        private void CreateCtor()
        {
            ConstructorBuilder ctorBuilder = this.typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[]
                                                                                                                                          {
                                                                                                                                                  typeof(InvocationHandler)
                                                                                                                                          });
            ctorBuilder.DefineParameter(1, ParameterAttributes.None, "handler");
            ILGenerator generator = ctorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg, 0);
            generator.Emit(OpCodes.Ldarg, 1);
            generator.Emit(OpCodes.Stfld, this.defaultHandlerFieldBuilder);
            generator.Emit(OpCodes.Ret);
        }

        private void CreateDefaultHandlerField()
        {
            this.defaultHandlerFieldBuilder = this.typeBuilder.DefineField("defaultHandler", typeof(InvocationHandler), FieldAttributes.Public);
        }

        private void CreateInterfaceList()
        {
            Type[] allFromComposite = this.compositeType.GetAllInterfaces().ToArray();
            var allFromAdditional = from t in additionalTypes
                                      from tt in t.GetAllInterfaces()
                                      select tt;

            var all = allFromComposite.Union(allFromComposite).Distinct().ToArray();

            this.interfaces = all;
        }

        private void CreateMethod(MethodInfo method)
        {
            MethodBuilder methodBuilder = this.typeBuilder.GetMethodOverrideBuilder(method);
            methodBuilder.SetCustomAttribute(typeof(DebuggerStepThroughAttribute).GetConstructor(new Type[]
                                                                                                     {
                                                                                                     }), new byte[]
                                                                                                             {
                                                                                                             });
            methodBuilder.SetCustomAttribute(typeof(DebuggerHiddenAttribute).GetConstructor(new Type[]
                                                                                                {
                                                                                                }), new byte[]
                                                                                                        {
                                                                                                        });

            ILGenerator generator = methodBuilder.GetILGenerator();

            FieldBuilder fieldBuilder = this.defaultHandlerFieldBuilder;

            CreateInvocationHandlerMethod(method, generator, fieldBuilder);
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
    }
}