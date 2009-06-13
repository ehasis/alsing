namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Reflection;

    public interface PropertyModel
    {
        AbstractProperty NewInstance(object value);


    }

    public class PropertyModel<T> : PropertyModel
    {
        private readonly MethodInfo accessor;

        public PropertyModel(MethodInfo accessor)
        {
            this.accessor = accessor;
        }

        public MethodInfo GetAccessor()
        {
            return this.accessor;
        }

        public AbstractProperty NewInstance(object value)
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
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