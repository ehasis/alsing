namespace QI4N.Framework.Runtime
{
    using System;
    using System.Linq;
    using System.Reflection;

    using JavaProxy;

    using Reflection;

    public interface PropertyModel
    {
        AbstractProperty NewInstance(object value);

        string QualifiedName { get; }

        AbstractProperty NewBuilderInstance();

        MethodInfo Accessor { get; }

        AbstractProperty NewInitialInstance();
    }

    //Slow reflection code, but only used when setting up the composite models
    public static class PropertyModelFactory
    {
        public static PropertyModel NewInstance(MethodInfo accessor)
        {
            Type f = (from g in accessor.ReturnType.GetAllInterfaces()
                      where g.IsGenericType && g.GetGenericTypeDefinition() == typeof(Property<>)
                      select g).FirstOrDefault();

            Type propertyContentType = f.GetGenericArguments()[0];
            Type template = typeof(PropertyModel<>);
            Type generic = template.MakeGenericType(propertyContentType);
            var propertyModelInstance = Activator.CreateInstance(generic, new object[]
                                                                              {
                                                                                      accessor
                                                                              }) as PropertyModel;

            return propertyModelInstance;
        }

        public static PropertyModel NewInstance(PropertyInfo accessor)
        {
            Type propertyContentType = accessor.PropertyType;
            Type template = typeof(PropertyModel<>);
            Type generic = template.MakeGenericType(propertyContentType);
            var propertyModelInstance = Activator.CreateInstance(generic, new object[]
                                                                              {
                                                                                      accessor
                                                                              }) as PropertyModel;

            return propertyModelInstance;
        }
    }

    public class PropertyModel<T> : PropertyModel
    {
        private readonly MethodInfo accessor;

        public PropertyModel(MethodInfo accessor)
        {
            this.accessor = accessor;
        }

        public MethodInfo Accessor
        {
            get
            {
                return this.accessor;
            }
        }

        public string QualifiedName
        {
            get
            {
                return this.accessor.Name;
            }
        }

        public MethodInfo GetAccessor()
        {
            return this.accessor;
        }

        public AbstractProperty NewBuilderInstance()
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
            AbstractProperty wrapper = this.WrapInstance(instance);

            return wrapper;
        }

        public AbstractProperty NewInitialInstance()
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
            AbstractProperty wrapper = this.WrapInstance(instance);

            return wrapper;
        }

        public AbstractProperty NewInstance(object value)
        {
            var instance = new PropertyInstance<T>(null, (T)value, this);
            AbstractProperty wrapper = this.WrapInstance(instance);

            return wrapper;
        }

        private AbstractProperty WrapInstance(PropertyInstance<T> instance)
        {
            Type type = this.accessor.ReturnType;
            var handler = new PropertyHandler(instance);
            var proxy = Proxy.NewProxyInstance(type, handler) as AbstractProperty;

            return proxy;
        }
    }
}