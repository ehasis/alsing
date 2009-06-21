namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ValueModel : AbstractCompositeModel
    {
        public ValueModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }

        public static ValueModel NewModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, PropertyDeclarations propertyDeclarations, List<Type> assemblyConcerns, List<Type> sideEffects, List<Type> mixins)
        {
            var constraintsModel = new ConstraintsModel(compositeType);
            bool immutable = metaInfo.Get(typeof(ImmutableAttribute)) != null;
            var propertiesModel = new PropertiesModel(constraintsModel, propertyDeclarations, immutable);
            var stateModel = new StateModel(propertiesModel);
            var mixinsModel = new MixinsModel(compositeType, mixins);

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations(assemblyConcerns, concerns);
            ConcernsDeclaration.ConcernDeclarations(compositeType, concerns);
            var concernsModel = new ConcernsDeclaration(concerns);

            var sideEffectsModel = new SideEffectsDeclaration(compositeType, sideEffects);
            var compositeMethodsModel = new CompositeMethodsModel(compositeType, constraintsModel, concernsModel, sideEffectsModel, mixinsModel);
            stateModel.AddStateFor(compositeMethodsModel.Methods, compositeType);

            return new ValueModel(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel);

        }

    }
}