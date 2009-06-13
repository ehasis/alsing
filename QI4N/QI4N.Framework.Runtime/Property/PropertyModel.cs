namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Proxy;

    public class PropertyModel
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

        public AbstractProperty NewInstance<T>(object value)
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
            AbstractProperty wrapper = this.WrapInstance(instance);

            return wrapper;
        }

        private AbstractProperty WrapInstance<T>(PropertyInstance<T> instance)
        {
            Type type = this.accessor.ReturnType;
            var handler = new PropertyHandler(instance);
            var proxy = ProxyInstanceBuilder.NewProxyInstance(type, handler) as AbstractProperty;

            return proxy;
        }
    }
}