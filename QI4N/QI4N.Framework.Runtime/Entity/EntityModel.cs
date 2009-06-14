namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class EntityModel : AbstractCompositeModel
    {
        public static EntityModel NewModel(Type compositeType, IList<Type> mixins)
        {
            var stateModel = new EntityStateModel();
            var mixinsModel = new EntityMixinsModel();

            var compositeMethodsModel = new CompositeMethodsModel(compositeType, mixinsModel);
            return new EntityModel(stateModel, compositeMethodsModel, mixinsModel, compositeType);
        }

        public EntityModel(AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel, MixinsModel mixinsModel, Type compositeType) : base(stateModel, compositeMethodsModel, mixinsModel, compositeType)
        {
        }

    }
}