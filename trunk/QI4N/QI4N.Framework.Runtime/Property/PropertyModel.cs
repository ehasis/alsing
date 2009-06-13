namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Reflection;

    public interface PropertyModel
    {
        AbstractProperty NewInstance(object value);


        string QualifiedName { get; }

        AbstractProperty NewBuilderInstance();

        MethodInfo Accessor { get; }

        AbstractProperty NewInitialInstance();
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