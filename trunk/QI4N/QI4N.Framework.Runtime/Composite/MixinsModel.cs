namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class MixinsModel
    {
        protected readonly IDictionary<MethodInfo, MixinModel> methodImplementation = new Dictionary<MethodInfo, MixinModel>();

        protected readonly IList<Type> mixinImplementationTypes = new List<Type>();

        protected readonly IList<MixinModel> mixinModels = new List<MixinModel>();

        protected readonly HashSet<Type> mixinTypes = new HashSet<Type>();

        //    private readonly IDictionary<Type, Type> mixinToImplementationLookup = new Dictionary<Type, Type>();


        public MixinsModel()
        {
            this.mixinImplementationTypes.Add(typeof(CompositeMixin));
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
                //             mixinToImplementationLookup.Add(mixinType,mixinImplementationType);
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
                    IEnumerable<Type> fieldTypes = implementationType
                            .GetAllFields()
                            .Where(f => f.HasAttribute(typeof(ThisAttribute)))
                            .Select(f => f.FieldType);

                    thisTypes.AddRange(fieldTypes);
                }

                foreach (Type type in thisTypes)
                {
                    this.AddMixinType(type);
                }
            } while (oldCount != this.mixinImplementationTypes.Count);
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
            if (!this.methodImplementation.ContainsKey(method))
            {
                MixinModel mixinModel = this.TryGetExistingMixinModel(method);

                if (mixinModel == null)
                {
                    mixinModel = this.GetNewMixinModel(method);

                    this.mixinModels.Add(mixinModel);
                }

                this.methodImplementation.Add(method, mixinModel);
                return mixinModel;
            }

            return this.methodImplementation[method];
        }

        public int IndexOfMixin(Type mixinImplementationType)
        {
            return this.mixinImplementationTypes.IndexOf(mixinImplementationType);
        }

        public object[] NewMixinHolder()
        {
            return new object[this.mixinImplementationTypes.Count];
        }

        public void NewMixins(CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder, object[] mixins)
        {
            int i = 0;
            foreach (Type mixinImplementationType in this.mixinImplementationTypes)
            {
                object mixin = Activator.CreateInstance(mixinImplementationType, null);
                mixins[i++] = mixin;
            }

            foreach (object mixin in mixins)
            {
                ConfigureMixin(mixin, compositeInstance, uses, stateHolder);
            }
        }

        private static void ConfigureMixin(object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            FieldInfo[] mixinFields = mixin.GetType().GetAllFields().ToArray();

            foreach (FieldInfo mixinField in mixinFields)
            {
                ConfigureMixinField(mixinField, mixin, compositeInstance, uses, stateHolder);
            }
        }

        private static void ConfigureMixinField(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            MixinFieldInjectState(mixinField, mixin, compositeInstance, uses, stateHolder);

            MixinFieldInjectThis(mixinField, mixin, compositeInstance, uses, stateHolder);

            MixinFieldInjectUse(mixinField, mixin, compositeInstance, uses, stateHolder);
        }

        private static void MixinFieldInjectState(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            bool isState = mixinField.GetCustomAttributes(typeof(StateAttribute), true).Any();

            if (isState)
            {
                if (typeof(AbstractProperty).IsAssignableFrom(mixinField.FieldType))
                {
                    //// TODO: create property
                    //object propertyInstance = null;
                    //mixinField.SetValue(mixin, propertyInstance);
                }
                else if (typeof(AbstractAssociation).IsAssignableFrom(mixinField.FieldType))
                {
                    //// TODO: create association
                    //object associationInstance = null;
                    //mixinField.SetValue(mixin, associationInstance);
                }
                else if (typeof(StateHolder).IsAssignableFrom(mixinField.FieldType))
                {
                    mixinField.SetValue(mixin, stateHolder);
                }
            }
        }

        private static void MixinFieldInjectThis(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            bool isThis = mixinField.GetCustomAttributes(typeof(ThisAttribute), true).Any();

            if (isThis)
            {
                ////normal "This"
                //if (mixinField.FieldType.IsAssignableFrom(compositeInstance.GetType()))
                //{
                mixinField.SetValue(mixin, compositeInstance.Proxy);
                //}
                ////private mixin
                //else
                //{
                //    //gg
                //}
            }
        }

        private static void MixinFieldInjectUse(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            bool isUse = mixinField.GetCustomAttributes(typeof(UsesAttribute), true).Any();
            if (isUse)
            {
                object obj = uses.UseForType(mixinField.FieldType);
                mixinField.SetValue(mixin, obj);
            }
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

        private MixinModel GetNewMixinModel(MethodInfo method)
        {
            Type mixinType = this.FindTypedImplementation(method);

            if (mixinType != null)
            {
                return this.ImplementMethodWithType(method, mixinType);
            }

            mixinType = this.FindGenericImplementation(method);

            if (mixinType != null)
            {
                return this.ImplementMethodWithType(method, mixinType);
            }

            throw new Exception("No implementation found for method " + method.Name);
        }

        private MixinModel ImplementMethodWithType(MethodInfo method, Type mixinType)
        {
            var mixinModel = new MixinModel(mixinType);
            return mixinModel;
        }

        private MixinModel TryGetExistingMixinModel(MethodInfo method)
        {
            MixinModel mixinModel = null;
            foreach (MixinModel existingModel in this.mixinModels)
            {
                if (existingModel.MixinType == method.DeclaringType)
                {
                    mixinModel = existingModel;
                    break;
                }
            }
            return mixinModel;
        }

        public IEnumerable<Type> GetMixinTypes()
        {
            return this.mixinTypes;
        }
    }
}