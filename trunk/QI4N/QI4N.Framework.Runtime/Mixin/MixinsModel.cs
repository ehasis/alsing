namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    internal class MixinsModel : AbstractMixinsModel
    {
        public MixinsModel(Type compositeType, IList<Type> assemblyMixins)
                : base(compositeType, assemblyMixins)
        {
        }

        public void NewMixins(CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder, object[] mixins)
        {
            int i = 0;
            foreach (MixinModel mixinModel in this.mixinModels)
            {
                object mixin = Activator.CreateInstance(mixinModel.MixinType, null);
                mixins[i++] = mixin;
            }

            foreach (object mixin in mixins)
            {
                ConfigureMixin(mixin, compositeInstance, uses, stateHolder);
            }
        }

        private static void ConfigureMixin(object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            IEnumerable<FieldInfo> mixinFields = TypeExtensions.GetAllFields(mixin.GetType());

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
                if (typeof(Property).IsAssignableFrom(mixinField.FieldType))
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
                mixinField.SetValue(mixin, compositeInstance.NewProxy(mixinField.FieldType));
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
    }
}