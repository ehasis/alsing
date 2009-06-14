namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class CompositeModel : AbstractCompositeModel
    {
        protected CompositeModel(AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel, MixinsModel mixinsModel, Type compositeType)
                : base(stateModel, compositeMethodsModel, mixinsModel, compositeType)
        {
        }

        public static CompositeModel NewModel(Type compositeType, IList<Type> mixins)
        {
            var stateModel = new CompositeStateModel();
            var mixinsModel = new MixinsModel();

            var compositeMethodsModel = new CompositeMethodsModel(compositeType, mixinsModel);

            stateModel.AddStateFor(compositeMethodsModel.Methods, compositeType);

            return new CompositeModel(stateModel, compositeMethodsModel, mixinsModel, compositeType);
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