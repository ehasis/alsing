namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public sealed class ValueModel : AbstractCompositeModel
    {
        public ValueModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }

        public static ValueModel NewModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, PropertyDeclarations propertyDeclarations, List<Type> assemblyConcerns, List<Type> sideEffects, List<Type> mixins)
        {
            var constraintsModel = new ValueConstraintsModel(compositeType);
            bool immutable = metaInfo.Get(typeof(ImmutableAttribute)) != null;
            var propertiesModel = new ValuePropertiesModel(constraintsModel, propertyDeclarations, immutable);
            var stateModel = new ValueStateModel(propertiesModel);
            var mixinsModel = new MixinsModel(compositeType, mixins);

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations(assemblyConcerns, concerns);
            ConcernsDeclaration.ConcernDeclarations(compositeType, concerns);
            var concernsModel = new ConcernsDeclaration(concerns);

            var sideEffectsModel = new SideEffectsDeclaration(compositeType, sideEffects);
            var compositeMethodsModel = new CompositeMethodsModel(compositeType, constraintsModel, concernsModel, sideEffectsModel, mixinsModel);
            stateModel.AddStateFor(compositeMethodsModel.Properties, compositeType);

            return new ValueModel(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel);
        }


        public CompositeInstance NewValueInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = this.mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new ValueInstance(this, moduleInstance, mixins, stateHolder);

            ((MixinsModel)this.mixinsModel).NewMixins(compositeInstance, uses, stateHolder, mixins);

            return compositeInstance;
        }
    }
}