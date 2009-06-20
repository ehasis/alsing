namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using JavaProxy;

    public abstract class AbstractCompositeModel
    {
        protected CompositeMethodsModel compositeMethodsModel;

        protected Type compositeType;

        protected AbstractMixinsModel mixinsModel;

        protected Type proxyType;

        protected AbstractStateModel stateModel;

        private MetaInfo metaInfo;

        private readonly Visibility visibility;

        protected AbstractCompositeModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
        {
            this.compositeType = compositeType;
            this.visibility = visibility;
            this.metaInfo = metaInfo;
            this.stateModel = stateModel;

            // Create proxy class
            this.proxyType = CreateProxyType(compositeType);

            this.mixinsModel = mixinsModel;

            this.compositeMethodsModel = compositeMethodsModel;
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

        public Visibility Visibility
        {
            get
            {
                return this.visibility;
            }
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public object Invoke(MixinsInstance mixins, CompositeInstance compositeInstance, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
        {
            return this.compositeMethodsModel.Invoke(mixins, proxy, method, args, moduleInstance);
        }

        public StateHolder NewBuilderState()
        {
            return this.stateModel.NewBuilderState();
        }

        public StateHolder NewInitialState()
        {
            return this.stateModel.NewInitialState();
        }

        public Composite NewProxy(InvocationHandler invocationHandler)
        {
            // TODO: linqify
            var instance = Activator.CreateInstance(this.proxyType, invocationHandler) as Composite;

            return instance;
        }

        public StateHolder NewState(StateHolder state)
        {
            return this.stateModel.NewState(state);
        }

        private static Type CreateProxyType(Type compositeType)
        {
            return Proxy.BuildProxyType(compositeType);
        }

        //    public abstract CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance usesInstance, StateHolder instanceState);
    }
}