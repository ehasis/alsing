namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class EntityModel : AbstractCompositeModel
    {
        public EntityModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel) : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }

        public static EntityModel NewModel(Type compositeType, IList<Type> mixins)
        {
            //var stateModel = new EntityStateModel();
            //var mixinsModel = new EntityMixinsModel();
            //var concernsDeclaration = new ConcernsDeclaration();
            //var sideEffectsDeclaration = new SideEffectsDeclaration();

            //var compositeMethodsModel = new CompositeMethodsModel(compositeType,concernsDeclaration,sideEffectsDeclaration, mixinsModel);
            //return new EntityModel(stateModel, compositeMethodsModel, mixinsModel, compositeType);

            return null;
        }
    }
}