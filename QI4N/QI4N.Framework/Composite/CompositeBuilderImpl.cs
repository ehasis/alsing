namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Internal;

    public class CompositeBuilderImpl<T> : CompositeBuilder<T>
    {
        protected IDictionary<MethodInfo, AbstractAssociation> associationValues;

        protected Type compositeInterface = typeof(T);

        protected CompositeContext context;

        protected ModuleInstance moduleInstance;

        protected IDictionary<MethodInfo, Property> propertyValues;

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

        protected IDictionary<MethodInfo,Property> Properties
        {
            get
            {
                if (propertyValues == null)
                    propertyValues = new Dictionary<MethodInfo, Property>();

                return propertyValues;
            }
        }

        public IDictionary<MethodInfo,AbstractAssociation> Associations
        {
            get
            {
                if (associationValues == null)
                    associationValues = new Dictionary<MethodInfo, AbstractAssociation>();

                return associationValues;
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

            var builder = new Proxy.Proxy();
            var instance = builder.NewProxyInstance<T>(compositeType);
            ConfigureInstance(instance);
            return instance;
        }

        public T NewInstanceJava()
        {
            // Calculate total set of Properties for this Composite
            var properties = new Dictionary<MethodInfo, Property>();
            foreach (PropertyContext propertyContext in context.GetPropertyContexts())
            {
                object value;
                MethodInfo accessor = propertyContext.GetPropertyBinding().GetPropertyResolution().GetPropertyModel().GetAccessor();
                if (propertyValues != null && propertyValues.ContainsKey(accessor))
                {
                    value = propertyValues[accessor].GetValue();
                }
                else
                {
                    value = propertyContext.GetPropertyBinding().GetDefaultValue();
                }

                Property property = propertyContext.NewInstance(moduleInstance, value, accessor.ReturnType);
                PropertyBinding binding = propertyContext.GetPropertyBinding();
                PropertyResolution propertyResolution = binding.GetPropertyResolution();
                PropertyModel propertyModel = propertyResolution.GetPropertyModel();
                properties.Add(propertyModel.GetAccessor(), property);
            }

            CompositeInstance compositeInstance = context.NewCompositeInstance(moduleInstance,
                                                                                uses,
                                                                                new CompositeBuilderState(properties));
            return (T) compositeInstance.GetProxy();
        }

        public K StateFor<K>()
        {
            return (K)(object)this.StateOfComposite();
        }

        public T StateOfComposite()
        {
            //if (Equals(this.stateFor, default(T)))
            //{
            //    Type compositeType = GetMatchingComposite();
            //    var builder = new Proxy.Proxy();
            //    var instance = builder.NewProxyInstance<T>(compositeType);
            //    ConfigureInstance(instance);
            //    this.stateFor = instance;
            //}

            //return this.stateFor;

            throw new NotImplementedException();
        }

        public void use(params object[] usedObjects)
        {
            HashSet<object> useSet = this.Uses;

            foreach (object usedObject in usedObjects)
            {
                useSet.Add(usedObject);
            }
        }


        private static void ConfigureInstance(T compositeInstance)
        {
            compositeInstance
                    .GetType()
                    .GetFields()
                    .Select(f => f.GetValue(compositeInstance))
                    .ToList()
                    .ForEach(mixinInstance => ConfigureMixinInstance(mixinInstance, compositeInstance));
        }

        private static void ConfigureMixinInstance(object mixinInstance, object compositeInstance)
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

        private static void InjectState(object mixinInstance, FieldInfo field, InjectionScopeAttribute fieldAttribute)
        {
            Type mixinInterface = mixinInstance.GetType().GetInterfaces().First();
            if (typeof(Property).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);
                var builder = new Proxy.Proxy();
                object fieldValue = builder.NewProxyInstance(field.FieldType);

                field.SetValue(mixinInstance, fieldValue);
            }
            else if (typeof(AbstractAssociation).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);

                var builder = new Proxy.Proxy();
                object fieldValue = builder.NewProxyInstance(field.FieldType);

                field.SetValue(mixinInstance, fieldValue);
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
                var builder = new Proxy.Proxy();
                object privateMixinInstance = builder.NewProxyInstance(field.FieldType);

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

    public class CompositeBuilderState
    {
        public CompositeBuilderState(IDictionary<MethodInfo, Property> properties)
        {
            throw new NotImplementedException();
        }
    }

    public interface PropertyContext
    {
        PropertyBinding GetPropertyBinding();

        Property NewInstance(ModuleInstance moduleInstance, object value, Type type);
    }

    public interface PropertyBinding
    {
        PropertyResolution GetPropertyResolution();

        object GetDefaultValue();
    }
}