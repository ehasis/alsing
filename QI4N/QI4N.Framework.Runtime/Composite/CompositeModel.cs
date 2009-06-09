namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Proxy;

    public class CompositeModel : AbstractCompositeModel
    {
        protected AbstractStateModel stateModel;

        protected Type compositeType;

        protected IDictionary<MethodInfo, AbstractProperty> propertyValues;

        protected IDictionary<MethodInfo, AbstractProperty> Properties
        {
            get
            {
                if (this.propertyValues == null)
                {
                    this.propertyValues = new Dictionary<MethodInfo, AbstractProperty>();
                }

                return this.propertyValues;
            }
        }

        public CompositeModel(Type compositeType)
        {
            this.stateModel = new AbstractStateModel();
            this.compositeType = compositeType;

            var realCompositeType = this.GetMatchingComposite();

            var builder = new InvocationProxyTypeBuilder();

            proxyType = builder.BuildProxyType(realCompositeType);
        }

        public AbstractStateModel State
        {
            get
            {
                return this.stateModel;
            }
        }

        public StateHolder NewBuilderState()
        {
            return this.stateModel.NewBuilderState();
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = null;
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);
            return compositeInstance;
        }

        private Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where this.compositeType.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
        }

        public StateHolder NewInitialState()
        {
            return this.stateModel.NewInitialState();
        }

        public StateHolder NewState(StateHolder state)
        {
            return this.stateModel.NewState(state);
        }


        public class StateInvocationHandler : InvocationHandler
        {
            private readonly CompositeModel owner;

            public StateInvocationHandler(CompositeModel owner)
            {
                this.owner = owner;
            }

            public object Invoke(object proxy, MethodInfo method, object[] objects)
            {
                if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
                {
                    AbstractProperty propertyInstance;

                    if (!this.owner.Properties.TryGetValue(method, out propertyInstance))
                    {
                        propertyInstance = ProxyInstanceBuilder.NewProxyInstance(method.ReturnType) as AbstractProperty;
                        //PropertyContext propertyContext = this.context.GetMethodDescriptor(method).GetCompositeMethodContext().GetPropertyContext();
                        //propertyInstance = propertyContext.NewInstance(this.moduleInstance, null, method.ReturnType);
                        this.owner.Properties.Add(method, propertyInstance);
                    }
                    return propertyInstance;
                }

                throw new NotSupportedException("Method does not represent state: " + method.Name);
            }
        }

        public Composite NewProxy(InvocationHandler invocationHandler)
        {
            //var instance = ProxyInstanceBuilder.NewProxyInstance<Composite>(proxyType);
            //return instance;
            var instance = Activator.CreateInstance(this.proxyType) as Composite;
            FieldInfo defaultHandlerField = proxyType.GetField("defaultHandler");
            defaultHandlerField.SetValue(instance, invocationHandler);

            return instance;            
        }
    }

    public abstract class AbstractCompositeModel
    {
        protected Type proxyType;
    }
}