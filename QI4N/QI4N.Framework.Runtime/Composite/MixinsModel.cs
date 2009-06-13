namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class MixinsModel
    {
        private readonly IDictionary<MethodInfo, MixinModel> methodImplementation = new Dictionary<MethodInfo, MixinModel>();

        private readonly IList<Type> mixinImplementationTypes = new List<Type>();

        private readonly IList<MixinModel> mixinModels = new List<MixinModel>();

        private readonly HashSet<Type> mixinTypes = new HashSet<Type>();

        private readonly IDictionary<Type, Type> mixinToImplementationLookup = new Dictionary<Type, Type>();

        public void AddMixinType(Type mixinType)
        {
            this.mixinTypes.Add(mixinType);

            foreach (Type mixinImplementationType in mixinType.GetMixinTypes())
            {
                this.mixinImplementationTypes.Add(mixinImplementationType);
                mixinToImplementationLookup.Add(mixinType,mixinImplementationType);
            }
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
            if (!this.methodImplementation.ContainsKey(method))
            {                
                MixinModel mixinModel = null;

                mixinModel = this.TryGetExistingMixinModel(method);

                if (mixinModel == null)
                {
                    mixinModel = GetNewMixinModel(method);

                    this.mixinModels.Add(mixinModel);
                }

                this.methodImplementation.Add(method, mixinModel);
                return mixinModel;
            }

            return this.methodImplementation[method];
        }

        public int IndexOfMixin(Type mixinImplementationType)
        {
            return mixinImplementationTypes.IndexOf(mixinImplementationType);
        }

        private MixinModel GetNewMixinModel(MethodInfo method)
        {
            Type delegatingType = this.GetDelegatingType(method);

            if (delegatingType != null)
            {
                var mixinModel = new MixinModel
                                     {
                                             MixinsModel = this,
                                             MixinType = delegatingType
                                     };

                return mixinModel;
            }

            Type invocationType = this.GetInterceptingType(method);

            if (invocationType != null)
            {
                var mixinModel = new MixinModel
                {
                    MixinsModel = this,
                    MixinType = invocationType
                };

                return mixinModel;
            }

            return null;
        }

        private Type GetInterceptingType(MethodInfo method)
        {
            Type mixinImplementationType = (from fb in this.mixinImplementationTypes
                                         where typeof(InvocationHandler).IsAssignableFrom(fb)
                                         from a in fb.GetCustomAttributes(typeof(AppliesToAttribute), true).Cast<AppliesToAttribute>()
                                         from t in a.AppliesToTypes
                                         let f = Activator.CreateInstance(t, null) as AppliesToFilter
                                         where f.AppliesTo(method, fb, null, null)
                                         select fb).FirstOrDefault();

            return mixinImplementationType;
        }

        private Type GetDelegatingType(MethodInfo method)
        {
            Type mixinImplementationType = (from m in this.mixinImplementationTypes
                                            from i in m.GetInterfaces()
                                            where i == method.DeclaringType
                                            select m).FirstOrDefault();

            return mixinImplementationType;
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
            MixinFieldInjectState(mixinField, mixin,compositeInstance,uses, stateHolder);

            MixinFieldInjectThis(mixinField, mixin, compositeInstance, uses, stateHolder);
        }

        private static void MixinFieldInjectState(FieldInfo mixinField, object mixin,CompositeInstance compositeInstance,UsesInstance uses, StateHolder stateHolder)
        {
            bool isState = mixinField.GetCustomAttributes(typeof(StateAttribute), true).Any();

            if (isState)
            {
                if (typeof(AbstractProperty).IsAssignableFrom(mixinField.FieldType))
                {
                    // TODO: create property
                    object propertyInstance = null;
                    mixinField.SetValue(mixin, propertyInstance);
                }
                else if (typeof(AbstractAssociation).IsAssignableFrom(mixinField.FieldType))
                {
                    // TODO: create association
                    object associationInstance = null;
                    mixinField.SetValue(mixin, associationInstance);
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
                mixinField.SetValue(mixin, compositeInstance);
            }
        }
    }
}