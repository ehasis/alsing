namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Internal;

    using Proxy;

    using Reflection;

    public class CompositeBuilderImpl<T> : CompositeBuilder<T>
    {
        protected IDictionary<MethodInfo, AbstractAssociation> associationValues;

        protected Type compositeInterface = typeof(T);

        protected CompositeContext context;

        protected ModuleInstance moduleInstance;

        protected IDictionary<MethodInfo, AbstractProperty> propertyValues;

        protected HashSet<Object> uses;

        public CompositeBuilderImpl(ModuleInstance moduleInstance, CompositeContext context)
        {
            this.moduleInstance = moduleInstance;
            this.context = context;

            this.compositeInterface = context.GetCompositeBinding().GetCompositeResolution().GetCompositeModel().GetCompositeType();
        }

        public CompositeBuilderImpl()
        {
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

        protected HashSet<object> Uses
        {
            get
            {
                if (this.uses == null)
                {
                    this.uses = new HashSet<object>();
                }

                return this.uses;
            }
        }


        public T NewInstance()
        {
            Type compositeType = GetMatchingComposite();

            var instance = ProxyInstanceBuilder.NewProxyInstance<T>(compositeType);
            ConfigureInstance(instance);

            return instance;
        }

        public T NewInstanceJava()
        {
            // Calculate total set of Properties for this Composite
            var properties = new Dictionary<MethodInfo, AbstractProperty>();
            foreach (PropertyContext propertyContext in this.context.GetPropertyContexts())
            {
                object value;
                MethodInfo accessor = propertyContext.GetPropertyBinding().GetPropertyResolution().GetPropertyModel().GetAccessor();
                if (this.propertyValues != null && this.propertyValues.ContainsKey(accessor))
                {
                    value = this.propertyValues[accessor].Get();
                }
                else
                {
                    value = null;// propertyContext.GetPropertyBinding().GetDefaultValue();
                }

                AbstractProperty property = propertyContext.NewInstance(this.moduleInstance, value, accessor.ReturnType);
                PropertyBinding binding = propertyContext.GetPropertyBinding();
                PropertyResolution propertyResolution = binding.GetPropertyResolution();
                PropertyModel propertyModel = propertyResolution.GetPropertyModel();
                properties.Add(propertyModel.GetAccessor(), property);
            }

            CompositeInstance compositeInstance = this.context.NewCompositeInstance(this.moduleInstance,
                                                                                    this.uses,
                                                                                    new CompositeBuilderState(properties));
            return (T)compositeInstance.GetProxy();
        }

        public K StateFor<K>()
        {
            // Instantiate proxy for given interface

            var handler = new StateInvocationHandler(this.context, this.moduleInstance, this.Properties);

            object instance = ProxyInstanceBuilder.NewProxyInstance(typeof(K), handler);
            return (K)instance;
        }

        public T StateOfComposite()
        {
            // Instantiate proxy for given composite interface

            var handler = new StateInvocationHandler(this.context, this.moduleInstance, this.Properties);

            object instance = ProxyInstanceBuilder.NewProxyInstance(typeof(T), handler);
            return (T)instance;
        }

        public void use(params object[] usedObjects)
        {
            HashSet<object> useSet = this.Uses;

            foreach (object usedObject in usedObjects)
            {
                useSet.Add(usedObject);
            }
        }


        private void ConfigureInstance(T compositeInstance)
        {
            compositeInstance
                    .GetType()
                    .GetFields()
                    .Select(f => f.GetValue(compositeInstance))
                    .ToList()
                    .ForEach(mixinInstance => ConfigureMixinInstance(mixinInstance, compositeInstance));



            foreach(MethodInfo accessor in Properties.Keys)
            {
                var state = Properties[accessor];
                var value = state.Get();
                var property = accessor.Invoke(compositeInstance,null) as AbstractProperty;
                property.Set(value);
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
                        InjectState(mixinInstance, field, fieldAttribute);
                    }
                }
            }
        }

        private static Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where typeof(T).IsAssignableFrom(composite) &&
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
    }

    public interface CompositeInstance
    {
        object GetProxy();
    }

    public interface PropertyResolution
    {
        PropertyModel GetPropertyModel();
    }

    public interface PropertyModel
    {
        MethodInfo GetAccessor();
    }

    public interface PropertyContext
    {
        PropertyBinding GetPropertyBinding();

        AbstractProperty NewInstance(ModuleInstance moduleInstance, object value, Type type);
    }

    public interface PropertyBinding
    {
        PropertyResolution GetPropertyResolution();

        object GetDefaultValue();
    }
}