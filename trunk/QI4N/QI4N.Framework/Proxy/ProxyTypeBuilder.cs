namespace QI4N.Framework.Proxy
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

        private Type proxyType;

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
                this.CreateMethods(type);
            }

            this.CreateDefaultCtor();
            this.CreateProxyType();

            return this.proxyType;
        }

        private static IEnumerable<MethodInfo> GetMethods(Type type)
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
            // TODO: init mixins here
        }

        private void CreateInterfaceList()
        {
            Type[] all = this.compositeType.GetAllInterfaces().ToArray();
            var interfaceList = new List<Type>();
            interfaceList.AddRange(all);

            this.interfaces = interfaceList.ToArray();
        }

        private void CreateMethod(MethodInfo method)
        {
            MethodBuilder methodBuilder = this.GetMethodBuilder(method);

            ILGenerator generator = methodBuilder.GetILGenerator();


            var fieldBuilder = (from fb in fieldBuilders
                                from i in fb.FieldType.GetInterfaces()
                                where i == method.DeclaringType
                                select fb).FirstOrDefault();

            if (fieldBuilder != null)
            {
                this.CreateDelegatedMixinMethod(method,generator, fieldBuilder);
            }
            else
            {
                this.CreateUnMappedMethod(method, generator);
            }


            
        }

        private MethodBuilder GetMethodBuilder(MethodInfo method)
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

            MethodBuilder methodBuilder = this
                    .typeBuilder
                    .DefineMethod(methodName, methodAttributes, CallingConventions.Standard, method.ReturnType, parameters);

            this.typeBuilder.DefineMethodOverride(methodBuilder, method);
            return methodBuilder;
        }

        private void CreateUnMappedMethod(MethodInfo method, ILGenerator generator)
        {
            if (method.ReturnType != typeof(void))
            {
                LocalBuilder local = generator.DeclareLocal(method.ReturnType);
                local.SetLocalSymInfo("FakeReturn");
                generator.Emit(OpCodes.Ldloc, local);
            }
            generator.Emit(OpCodes.Ret);
        }

        private void CreateDelegatedMixinMethod(MethodInfo method, ILGenerator generator,FieldBuilder fieldBuilder )
        {
            //delegate calls to fieldbuilder 
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld,fieldBuilder);

            int paramCount = method.GetParameters().Length;

            for (int i = 0; i < paramCount;i++ )
            {
                generator.Emit(OpCodes.Ldarg,i+2);
            }
            generator.Emit(OpCodes.Callvirt,method);

            generator.Emit(OpCodes.Ret);
        }

        private void CreateMethods(Type type)
        {
            IEnumerable<MethodInfo> methods = GetMethods(type);

            foreach (MethodInfo method in methods)
            {
                this.CreateMethod(method);
            }
        }

        private void CreateMixinFields()
        {

            foreach(Type interfaceType in this.interfaceToMixinMap.Keys)
            {
                foreach(Type mixinType in this.interfaceToMixinMap[interfaceType])
                {
                    Type fieldType = mixinType;

                    string mixinFieldName = string.Format("_{0}", fieldType.GetTypeName());
                    mixinFieldName = mixinFieldName.Replace("`", "");

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
                foreach(Type mixinType in mixins)
                {
                    Type genericType = mixinType;

                    //handle generic type mixins
                    if (mixinType.IsGenericTypeDefinition)
                    {
                        var generics = interfaceType.GetGenericArguments();
                        genericType = mixinType.MakeGenericType(generics);
                    }

                    genericMixins.Add(genericType);
                }

                this.interfaceToMixinMap.Add(interfaceType, genericMixins.ToArray());
            }
        }

        private void CreateProxyType()
        {
            this.proxyType = this.typeBuilder.CreateType();
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