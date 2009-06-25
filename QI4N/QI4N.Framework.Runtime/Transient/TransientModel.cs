namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    using JavaProxy;

    public sealed class TransientModel : AbstractCompositeModel
    {
        public TransientModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }

        public static TransientModel NewModel(Type compositeType,
                                              Visibility visibility,
                                              MetaInfo metaInfo,
                                              PropertyDeclarations propertyDeclarations,
                                              IEnumerable<Type> assemblyConcerns,
                                              IEnumerable<Type> sideEffects, IList<Type> mixins)
        {
            var constraintsModel = new TransientConstraintsModel(compositeType);
            bool immutable = metaInfo.Get(typeof(ImmutableAttribute)) != null;
            var propertiesModel = new TransientPropertiesModel(constraintsModel, propertyDeclarations, immutable);
            var stateModel = new TransientStateModel(propertiesModel);
            var mixinsModel = new MixinsModel(compositeType, mixins);

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations(assemblyConcerns, concerns);
            ConcernsDeclaration.ConcernDeclarations(compositeType, concerns);
            var concernsModel = new ConcernsDeclaration(concerns);

            var sideEffectsModel = new SideEffectsDeclaration(compositeType, sideEffects);
            var compositeMethodsModel = new CompositeMethodsModel(compositeType, constraintsModel, concernsModel, sideEffectsModel, mixinsModel);
            stateModel.AddStateFor(compositeMethodsModel.Properties, compositeType);

            return new TransientModel(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel);
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = this.mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new TransientInstance(this, moduleInstance, mixins, stateHolder);

            ((MixinsModel)this.mixinsModel).NewMixins(compositeInstance, uses, stateHolder, mixins);

            return compositeInstance;
        }

        public static void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}