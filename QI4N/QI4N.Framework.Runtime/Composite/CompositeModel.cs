namespace QI4N.Framework.Runtime
{
    using System;

    public class CompositeModel : AbstractCompositeModel
    {
        public CompositeModel(CompositeMethodsModel compositeMethodsModel, MixinsModel mixinsModel, Type compositeType)
                : base(compositeMethodsModel, mixinsModel, compositeType)
        {
        }

        public Type CompositeType
        {
            get
            {
                return this.compositeType;
            }
        }

        public AbstractStateModel State
        {
            get
            {
                return this.stateModel;
            }
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = this.mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);

            this.mixinsModel.NewMixins(compositeInstance, uses, stateHolder, mixins);

            return compositeInstance;
        }
    }
}