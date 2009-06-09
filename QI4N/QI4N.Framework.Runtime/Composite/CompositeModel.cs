namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

    public class CompositeModel
    {
            public Composite NewProxy( InvocationHandler invocationHandler)
            {
                //// Instantiate proxy for given composite interface
                //try
                //{
                //    var composite = ProxyInstanceBuilder.NewProxyInstance()
                //}
                //catch (Exception e)
                //{
                //    throw new ConstructionException(e);
                //}
                throw new NotImplementedException();
            }

        public CompositeBinding GetCompositeBinding()
        {
            throw new NotImplementedException();
        }

        public MethodDescriptor GetMethodDescriptor(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PropertyContext> GetPropertyContexts()
        {
            throw new NotImplementedException();
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            throw new NotImplementedException();
        }


        public StateHolder NewInitialState()
        {
            throw new NotImplementedException();
        }

        public StateHolder NewState(StateHolder state)
        {
            throw new NotImplementedException();
        }

        public StateHolder NewBuilderState()
        {
            throw new NotImplementedException();
        }

        public AbstractStateModel State
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class AbstractStateModel
    {
        internal void CheckConstraints(StateHolder instanceState)
        {
            throw new NotImplementedException();
        }
    }

    public interface MethodDescriptor
    {
        CompositeMethodContext GetCompositeMethodContext();
    }

    public interface CompositeMethodContext
    {
        PropertyContext GetPropertyContext();
    }

    public interface CompositeBinding
    {
        CompositeResolution GetCompositeResolution();
    }

    public interface CompositeResolution
    {
        CompositeModel GetCompositeModel();
    }

}