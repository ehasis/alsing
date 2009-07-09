namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public abstract class AbstractMixinsModel
    {
        public IDictionary<MethodInfo, int> MethodIndex = new Dictionary<MethodInfo, int>();

        protected readonly IDictionary<MethodInfo, MixinModel> methodImplementation = new Dictionary<MethodInfo, MixinModel>();

        protected readonly IList<Type> mixinImplementationTypes = new List<Type>();

        protected readonly IList<MixinModel> mixinModels = new List<MixinModel>();

        protected readonly HashSet<Type> mixinTypes = new HashSet<Type>();


        protected AbstractMixinsModel(Type type, IEnumerable<Type> assemblyMixins)
        {
            this.mixinImplementationTypes.Add(typeof(CompositeMixin));

            foreach (Type assemblyMixin in assemblyMixins)
                this.mixinImplementationTypes.Add(assemblyMixin);
        }

        public void AddMixinType(Type mixinType)
        {
            if (this.mixinTypes.Contains(mixinType))
            {
                return;
            }

            this.mixinTypes.Add(mixinType);

            foreach (Type mixinImplementationType in mixinType.GetMixinTypes())
            {
                this.mixinImplementationTypes.Add(mixinImplementationType);
            }
        }

        public void AddThisTypes()
        {
            int oldCount;

            //recurse through mixin implementations and try to find This injections
            do
            {
                oldCount = this.mixinImplementationTypes.Count;
                var thisTypes = new List<Type>();
                foreach (Type implementationType in this.mixinImplementationTypes)
                {
                    IEnumerable<Type> fieldTypes = implementationType.GetAllFields()
                            .Where(f => f.HasAttribute<ThisAttribute>())
                            .Select(f => f.FieldType);

                    thisTypes.AddRange(fieldTypes);
                }

                foreach (Type type in thisTypes)
                {
                    foreach (Type childType in type.GetAllInterfaces())
                    {
                        this.AddMixinType(childType);
                    }
                }
            } while (oldCount != this.mixinImplementationTypes.Count);
        }

        public IEnumerable<Type> GetMixinTypes()
        {
            return this.mixinTypes;
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
            if (!this.methodImplementation.ContainsKey(method))
            {
                Type mixinType = this.FindTypedImplementation(method);
                if (mixinType != null)
                {
                    return this.ImplementMethodWithType(method, mixinType);
                }

                // Check generic implementations
                mixinType = this.FindGenericImplementation(method);
                if (mixinType != null)
                {
                    return this.ImplementMethodWithType(method, mixinType);
                }

                throw new Exception("No implementation found for method " + method.Name);
            }

            return this.methodImplementation[method];
        }

        public int IndexOfMixin(Type mixinImplementationType)
        {
            return this.mixinImplementationTypes.IndexOf(mixinImplementationType);
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public MixinModel MixinFor(MethodInfo method)
        {
            int integer = this.MethodIndex[method];
            return this.mixinModels[integer];
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public FragmentInvocationHandler NewInvocationHandler(MethodInfo method)
        {
            return this.MixinFor(method).NewInvocationHandler(method.DeclaringType);
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object[] NewMixinHolder()
        {
            return new object[this.mixinModels.Count];
        }


        private Type FindGenericImplementation(MethodInfo method)
        {
            Type mixinImplementationType = (from mit in this.mixinImplementationTypes
                                            where typeof(InvocationHandler).IsAssignableFrom(mit)
                                            from appattr in mit.GetCustomAttributes(typeof(AppliesToAttribute), true).Cast<AppliesToAttribute>()
                                            from appt in appattr.AppliesToTypes
                                            let atf = Activator.CreateInstance(appt, null) as AppliesToFilter
                                            where atf.AppliesTo(method, mit, null, null)
                                            select mit).FirstOrDefault();

            return mixinImplementationType;
        }

        private Type FindTypedImplementation(MethodInfo method)
        {
            Type mixinImplementationType = (from m in this.mixinImplementationTypes
                                            from i in m.GetInterfaces()
                                            where i == method.DeclaringType
                                            select m).FirstOrDefault();

            return mixinImplementationType;
        }

        private MixinModel ImplementMethodWithType(MethodInfo method, Type mixinType)
        {
            MixinModel foundMixinModel = null;

            foreach (MixinModel mixinModel in this.mixinModels)
            {
                if (mixinModel.MixinType.Equals(mixinType))
                {
                    foundMixinModel = mixinModel;
                    break;
                }
            }

            if (foundMixinModel == null)
            {
                foundMixinModel = new MixinModel(mixinType);
                this.mixinModels.Add(foundMixinModel);
            }

            this.methodImplementation.Add(method, foundMixinModel);
            this.MethodIndex.Add(method, this.mixinModels.IndexOf(foundMixinModel));

            return foundMixinModel;
        }
    }
}