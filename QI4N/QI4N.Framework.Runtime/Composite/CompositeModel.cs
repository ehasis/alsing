namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeModel : AbstractCompositeModel
    {
        protected IDictionary<MethodInfo, AbstractProperty> propertyValues;

        public CompositeModel(CompositeMethodsModel compositeMethodsModel,MixinsModel mixinsModel , Type compositeType)
                : base(compositeMethodsModel,mixinsModel, compositeType)
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

        protected IDictionary<MethodInfo, AbstractProperty> Properties
        {
            get
            {
                if (this.propertyValues == null)
                {
                    this.propertyValues = new Dictionary<MethodInfo, AbstractProperty>();
                }

                return this.propertyValues;
            }
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);

            mixinsModel.NewMixins(compositeInstance, uses, stateHolder, mixins);

            return compositeInstance;
        }
    }
}