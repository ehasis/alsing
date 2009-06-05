namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Proxy;

    public class DefaultCompositeBuilder<T> : CompositeBuilder<T>
    {

        public T NewInstance()
        {
            Type compositeType = GetMatchingComposite();

            var builder = new ProxyInstanceBuilder();
            var instance = builder.NewInstance<T>(compositeType);
            ConfigureInstance(instance,compositeType);
            return instance;
        }

        public K StateFor<K>()
        {
            return default(K);
        }


        private static Type GetMatchingComposite()
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where typeof(T).IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
        }

        private static void ConfigureInstance(T compositeInstance, Type compositeType)
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

            Type mixinInterface = mixinInstance.GetType().GetInterfaces().First();

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
                        InjectState(mixinInstance, field, fieldAttribute, mixinInterface);
                    }
                }
            }
        }

        private static void InjectState(object mixinInstance, FieldInfo field, InjectionScopeAttribute fieldAttribute, Type mixinInterface)
        {
            if (typeof(Property).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);
                var builder = new ProxyInstanceBuilder();
                object fieldValue = builder.NewInstance(field.FieldType);

                field.SetValue(mixinInstance, fieldValue);
            }
            else if (typeof(AbstractAssociation).IsAssignableFrom(field.FieldType))
            {
                var stateAttribute = fieldAttribute as StateAttribute;
                PropertyInfo property = stateAttribute.GetProperty(field, mixinInterface);

                //object[] propertyAttributes = property.GetCustomAttributes(typeof(InjectionScopeAttribute), true);

                var builder = new ProxyInstanceBuilder();
                object fieldValue = builder.NewInstance(field.FieldType);
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
                var builder = new ProxyInstanceBuilder();
                object privateMixinInstance = builder.NewInstance(field.FieldType);

                field.SetValue(mixinInstance, privateMixinInstance);
            }
        }


    }
}