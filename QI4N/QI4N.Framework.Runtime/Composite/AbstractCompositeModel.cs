namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Proxy;

    public abstract class AbstractCompositeModel
    {
        protected CompositeMethodsModel compositeMethodsModel;

        protected Type compositeType;

        protected AbstractMixinsModel mixinsModel;

        protected Type proxyType;

        protected AbstractStateModel stateModel;

        protected AbstractCompositeModel(CompositeMethodsModel compositeMethodsModel, Type compositeType)
        {
            this.stateModel = new AbstractStateModel();
            this.compositeType = compositeType;
            this.compositeMethodsModel = compositeMethodsModel;

            var builder = new InvocationProxyTypeBuilder();

            this.proxyType = builder.BuildProxyType(compositeType);
        }

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
            var instance = Activator.CreateInstance(this.proxyType, invocationHandler) as Composite;

            return instance;
        }

        public StateHolder NewState(StateHolder state)
        {
            return this.stateModel.NewState(state);
        }
    }
}