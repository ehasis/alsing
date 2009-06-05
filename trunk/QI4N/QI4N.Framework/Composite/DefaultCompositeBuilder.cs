namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class DefaultCompositeBuilder<T> : CompositeBuilder<T>
    {
        private static ProxyActivator<T> activator;

        public T NewInstance()
        {
            Type compositeType = GetMatchingComposite();
            T instance = this.CreateInstance(compositeType);
            return instance;
        }

        public K StateFor<K>()
        {
            return default(K);
        }


        private static ProxyActivator<T> GetCompositeActivator(Type compositeType)
        {
            //TODO: fix thread safety, build invocation
            if (activator == null)
            {
                activator = ProxyActivator.GetActivator<T>(compositeType);
            }

            return activator;
        }

        private static Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where typeof(T).IsAssignableFrom(composite)
                                                   select composite;

            return matchingComposites.Single();
        }

        private void ConfigureInstance(T compositeInstance, Type compositeType)
        {
            compositeInstance
                    .GetType()
                    .GetFields()
                    .Select(f => f.GetValue(compositeInstance))
                    .ToList()
                    .ForEach(mixinInstance => this.ConfigureMixinInstance(mixinInstance, compositeType,compositeInstance));
        }

        private void ConfigureMixinInstance(object mixinInstance, Type compositeType,object compositeInstance)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            FieldInfo[] fields = mixinInstance
                    .GetType()
                    .GetFields(flags);

            Type mixinInterface = mixinInstance.GetType().GetInterfaces().First();

            foreach (FieldInfo field in fields)
            {
                var fieldAttributes = field.GetCustomAttributes(typeof(InjectionScopeAttribute), true).Cast<InjectionScopeAttribute>();

                foreach (var fieldAttribute in fieldAttributes)
                {
                    if (fieldAttribute is ThisAttribute)
                    {
                        field.SetValue(mixinInstance, compositeInstance);
                    }
                    if (fieldAttribute is StateAttribute)
                    {
                        if (typeof(Property).IsAssignableFrom(field.FieldType))
                        {
                            var stateAttribute = fieldAttribute as StateAttribute;
                            PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                            //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);

                            ProxyActivator<object> factivator = ProxyActivator.GetActivator<object>(field.FieldType);
                            object fieldValue = factivator.Invoke();

                            field.SetValue(mixinInstance, fieldValue);
                        }
                        else if (typeof(AbstractAssociation).IsAssignableFrom(field.FieldType))
                        {
                            var stateAttribute = fieldAttribute as StateAttribute;
                            PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                            //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);

                            ProxyActivator<object> factivator = ProxyActivator.GetActivator<object>(field.FieldType);
                            object fieldValue = factivator.Invoke();

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
                }
            }
        }

        private T CreateInstance(Type compositeType)
        {
            ProxyActivator<T> proxyActivator = GetCompositeActivator(compositeType);
            T instance = proxyActivator.Invoke();
            this.ConfigureInstance(instance, compositeType);
            return instance;
        }
    }
}