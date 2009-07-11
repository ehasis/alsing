namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    internal class MixinsModel : AbstractMixinsModel
    {
        public MixinsModel(Type compositeType, IList<Type> assemblyMixins)
                : base(compositeType, assemblyMixins)
        {
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public void NewMixins(CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder, object[] mixins)
        {
            int i = 0;
            foreach (MixinModel mixinModel in this.mixinModels)
            {
                object mixin = mixinModel.NewInstance(compositeInstance, stateHolder, uses);
                mixins[i++] = mixin;
            }
        }


        //private static void MixinFieldInjectThis(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        //{
        //    bool isThis = mixinField.GetCustomAttributes(typeof(ThisAttribute), true).Any();

        //    if (isThis)
        //    {

        //        if (mixinField.FieldType.IsAssignableFrom(compositeInstance.GetType()))
        //        {
        //            mixinField.SetValue(mixin, compositeInstance);
        //        }
        //        else
        //        {
        //            mixinField.SetValue(mixin, compositeInstance.NewProxy(mixinField.FieldType));
        //        }
        //    }
        //}

        //private static void MixinFieldInjectUse(FieldInfo mixinField, object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        //{
        //    bool isUse = mixinField.GetCustomAttributes(typeof(UsesAttribute), true).Any();
        //    if (isUse)
        //    {
        //        object obj = uses.UseForType(mixinField.FieldType);
        //        mixinField.SetValue(mixin, obj);
        //    }
        //}
    }
}