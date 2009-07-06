namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Bootstrap;

    public sealed class ServiceModel : AbstractCompositeModel
    {
        private readonly string identity;

        private readonly bool instantiateOnStartup;

        private readonly string moduleName;

        private ServiceModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel, string moduleName, string identity, bool instantiateOnStartup)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
            this.moduleName = moduleName;
            this.identity = identity;
            this.instantiateOnStartup = instantiateOnStartup;
        }

        public string Identity
        {
            get
            {
                return this.identity;
            }
        }

        public bool InstantiateOnStartup
        {
            get
            {
                return this.instantiateOnStartup;
            }
        }

        public string ModuleName
        {
            get
            {
                return this.moduleName;
            }
        }


        public static ServiceModel NewModel(Type compositeType,
                                            Visibility visibility,
                                            MetaInfo metaInfo,
                                            IEnumerable<Type> assemblyConcerns,
                                            IEnumerable<Type> sideEffects,
                                            IList<Type> mixins,
                                            String moduleName,
                                            String identity,
                                            bool instantiateOnStartup)
        {
            PropertyDeclarations propertyDeclarations = new MetaInfoDeclaration();
            var constraintsModel = new ConstraintsModel(compositeType);
            bool immutable = metaInfo.Get<ImmutableAttribute>() != null;
            var propertiesModel = new ServicePropertiesModel(constraintsModel, propertyDeclarations, immutable);
            var stateModel = new ServiceStateModel(propertiesModel);
            var mixinsModel = new MixinsModel(compositeType, mixins);

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations(assemblyConcerns, concerns);
            ConcernsDeclaration.ConcernDeclarations(compositeType, concerns);
            var concernsModel = new ConcernsDeclaration(concerns);
            var sideEffectsModel = new SideEffectsDeclaration(compositeType, sideEffects);
            var compositeMethodsModel = new CompositeMethodsModel(compositeType, constraintsModel, concernsModel, sideEffectsModel, mixinsModel);
            stateModel.AddStateFor(compositeMethodsModel.Properties, compositeType);

            return new ServiceModel(
                    compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel, moduleName, identity, instantiateOnStartup);
        }

        public bool IsServiceFor(Type serviceType, Visibility visibility)
        {
            // Check visibility
            if (visibility != Visibility)
            {
                return false;
            }


            // Plain class check

            return serviceType.IsAssignableFrom(this.compositeType);
        }

        [DebuggerStepThrough]
        //[DebuggerHidden]
        public ServiceInstance NewInstance(ModuleInstance module)
        {
            StateHolder stateHolder = null;//
            var mixins = mixinsModel.NewMixinHolder();
            var serviceInstance = new ServiceInstance( this, module, mixins, stateHolder );

            var uses = new UsesInstance();
            uses.Use( this );

            // Instantiate all mixins
            ((MixinsModel)mixinsModel).NewMixins(serviceInstance,
                                                     uses,
                                                     stateHolder,
                                                     mixins );

            return serviceInstance;
        }
    }
}