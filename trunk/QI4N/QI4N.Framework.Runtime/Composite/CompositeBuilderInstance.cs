namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Proxy;

    public class CompositeBuilderInstance<T> : CompositeBuilder<T>
    {
        protected IDictionary<MethodInfo, AbstractAssociation> associationValues;

        protected Type compositeInterface;

        protected CompositeModel compositeModel;

        protected ModuleInstance moduleInstance;

        protected IDictionary<MethodInfo, AbstractProperty> propertyValues;

        protected CompositeInstance prototypeInstance;

        protected StateHolder state;

        protected UsesInstance uses;

        public CompositeBuilderInstance(ModuleInstance moduleInstance, CompositeModel model, UsesInstance uses)
                : this(moduleInstance, model)
        {
            this.uses = uses;
        }

        public CompositeBuilderInstance(ModuleInstance moduleInstance, CompositeModel model)
        {
            this.moduleInstance = moduleInstance;
            this.compositeModel = model;
        }

        public CompositeBuilderInstance()
        {
            this.compositeInterface = typeof(T);
        }

        public CompositeBuilderInstance(Type compositeInterface)
        {
            this.compositeInterface = compositeInterface;
        }

        public IDictionary<MethodInfo, AbstractAssociation> Associations
        {
            get
            {
                if (this.associationValues == null)
                {
                    this.associationValues = new Dictionary<MethodInfo, AbstractAssociation>();
                }

                return this.associationValues;
            }
        }

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

        protected StateHolder State
        {
            get
            {
                if (this.state == null)
                {
                    this.state = this.compositeModel.NewBuilderState();
                }
                return this.state;
            }
        }

        protected UsesInstance Uses
        {
            get
            {
                if (this.uses == null)
                {
                    this.uses = new UsesInstance();
                }

                return this.uses;
            }
        }


        public T NewInstance()
        {
            StateHolder instanceState;

            //if (this.state == null)
            //{
            //    instanceState = this.compositeModel.NewInitialState();
            //}
            //else
            //{
            //    instanceState = this.compositeModel.NewState(this.state);
            //}

            //this.compositeModel.State.CheckConstraints(instanceState);

            //CompositeInstance compositeInstance = compositeModel.NewCompositeInstance( moduleInstance, this.uses ?? UsesInstance.NO_USES, instanceState );

            //return (T)compositeInstance;

            Type compositeType = this.GetMatchingComposite();

            var instance = ProxyInstanceBuilder.NewProxyInstance<T>(compositeType);
            this.ConfigureInstance(instance);

            return instance;
        }

        //public T NewInstanceJava()
        //{
        //    // Calculate total set of Properties for this Composite
        //    var properties = new Dictionary<MethodInfo, AbstractProperty>();
        //    foreach (PropertyContext propertyContext in this.compositeModel.GetPropertyContexts())
        //    {
        //        object value;
        //        MethodInfo accessor = propertyContext.PropertyBinding.GetPropertyResolution().GetPropertyModel().GetAccessor();
        //        if (this.propertyValues != null && this.propertyValues.ContainsKey(accessor))
        //        {
        //            value = this.propertyValues[accessor].Value;
        //        }
        //        else
        //        {
        //            value = null;// propertyContext.GetPropertyBinding().GetDefaultValue();
        //        }

        //        AbstractProperty property = propertyContext.NewInstance(this.moduleInstance, value, accessor.ReturnType);
        //        PropertyBinding binding = propertyContext.PropertyBinding;
        //        PropertyResolution propertyResolution = binding.GetPropertyResolution();
        //        PropertyModel propertyModel = propertyResolution.GetPropertyModel();
        //        properties.Add(propertyModel.GetAccessor(), property);
        //    }

        //    CompositeInstance compositeInstance = this.compositeModel.NewCompositeInstance(this.moduleInstance,
        //                                                                            this.uses,
        //                                                                            new CompositeBuilderState(properties));
        //    return (T)compositeInstance.Proxy;
        //}

        public T Prototype()
        {
            // Instantiate proxy for given composite interface

            var handler = new StateInvocationHandler(this);

            object instance = ProxyInstanceBuilder.NewProxyInstance(this.compositeInterface, handler);
            return (T)instance;
        }

        public K PrototypeFor<K>()
        {
            // Instantiate proxy for given interface

            var handler = new StateInvocationHandler(this);

            object instance = ProxyInstanceBuilder.NewProxyInstance(typeof(K), handler);
            return (K)instance;
        }

        public void use(params object[] usedObjects)
        {
            this.Uses.Use(usedObjects);
        }

        public void Use(params object[] item)
        {
            throw new NotImplementedException();
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


        private void ConfigureInstance(T compositeInstance)
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

        private Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where this.compositeInterface.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
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

        public class StateInvocationHandler : InvocationHandler
        {
            private readonly CompositeBuilderInstance<T> owner;

            public StateInvocationHandler(CompositeBuilderInstance<T> owner)
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
    }

    public class UsesInstance
    {
        public static readonly UsesInstance NO_USES = new UsesInstance();

        internal void Use(object[] usedObjects)
        {
            throw new NotImplementedException();
        }
    }

    public interface PropertyResolution
    {
        PropertyModel GetPropertyModel();
    }

    public interface PropertyModel
    {
        MethodInfo GetAccessor();
    }
}