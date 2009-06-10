namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Proxy;

    using Runtime;

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

            Type realCompositeType = this.GetMatchingComposite();

            var builder = new InvocationProxyTypeBuilder();

            this.proxyType = builder.BuildProxyType(realCompositeType);
        }

        public object Invoke(object[] mixins, CompositeInstance compositeInstance, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
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

        private Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where this.compositeType.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
        }
    }

    public class CompositeMethodsModel
    {
        public object Invoke(object[] mixins, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
        {
            throw new NotImplementedException();
        }
    }

    public class AbstractMixinsModel
    {
    }
}