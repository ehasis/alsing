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
            Type runtimeCompositeType = this.GetMatchingComposite();

            var instance = ProxyInstanceBuilder.NewProxyInstance(runtimeCompositeType) as Composite;
            this.ConfigureInstance(instance);

            object[] mixins = null;
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);

            return compositeInstance;
        }

        private static void InjectThis(object mixinInstance, FieldInfo field, object compositeInstance)
        {
            if (field.FieldType.IsAssignableFrom(compositeInstance.GetType()))
            {
                field.SetValue(mixinInstance, compositeInstance);
            }
            else
            {
                object privateMixinInstance = ProxyInstanceBuilder.NewProxyInstance(field.FieldType);

                field.SetValue(mixinInstance, privateMixinInstance);
            }
        }


        private void ConfigureInstance(object compositeInstance)
        {
            compositeInstance
                    .GetType()
                    .GetFields()
                    .Select(f => f.GetValue(compositeInstance))
                    .ToList()
                    .ForEach(mixinInstance => this.ConfigureMixinInstance(mixinInstance, compositeInstance));

            foreach (MethodInfo accessor in this.Properties.Keys)
            {
                AbstractProperty state = this.Properties[accessor];
                object value = state.Value;
                var property = accessor.Invoke(compositeInstance, null) as AbstractProperty;
                if (property != null)
                {
                    property.Value = value;
                }
            }
        }

        private void ConfigureMixinInstance(object mixinInstance, object compositeInstance)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            FieldInfo[] fields = mixinInstance
                    .GetType()
                    .GetFields(flags);

            foreach (FieldInfo field in fields)
            {
                IEnumerable<InjectionScopeAttribute> fieldAttributes = field.GetCustomAttributes(typeof(InjectionScopeAttribute), true).Cast<InjectionScopeAttribute>();

                foreach (InjectionScopeAttribute fieldAttribute in fieldAttributes)
                {
                    if (fieldAttribute is ThisAttribute)
                    {
                        InjectThis(mixinInstance, field, compositeInstance);
                    }
                    if (fieldAttribute is StateAttribute)
                    {
                        this.InjectState(mixinInstance, field, fieldAttribute);
                    }
                }
            }
        }



        private void InjectState(object mixinInstance, FieldInfo field, InjectionScopeAttribute fieldAttribute)
        {
            Type mixinInterface = mixinInstance.GetType().GetInterfaces().First();
            if (typeof(AbstractProperty).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                var propertyInstance = ProxyInstanceBuilder.NewProxyInstance(field.FieldType) as AbstractProperty;
                field.SetValue(mixinInstance, propertyInstance);
            }
            else if (typeof(AbstractAssociation).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                var associationInstance = ProxyInstanceBuilder.NewProxyInstance(field.FieldType) as AbstractAssociation;

                field.SetValue(mixinInstance, associationInstance);
            }
            else if (typeof(StateHolder).IsAssignableFrom(field.FieldType))
            {
                //TODO: fix
                var state = new DefaultEntityStateHolder();
                field.SetValue(mixinInstance, state);
            }
            else
            {
                throw new Exception(string.Format("[State] can not be applied to field type '{0}'", field.FieldType.Name));
            }
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
            var instance = Activator.CreateInstance(proxyClass, invocationHandler) as Composite;
            return instance;

            //return Composite.class.cast( proxyClass.getConstructor( InvocationHandler.class ).newInstance( invocationHandler ) );
        }
    }

    public abstract class AbstractCompositeModel
    {
        protected readonly Type proxyClass;
    }
}