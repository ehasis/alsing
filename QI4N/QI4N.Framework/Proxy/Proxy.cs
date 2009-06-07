namespace QI4N.Framework.Proxy
{
    using System;

    using Activation;

    public class Proxy
    {
        public T NewProxyInstance<T>(Type type)
        {
            ObjectActivator<T> proxyActivator = GetActivator<T>(type);

            T instance = proxyActivator.Invoke();

            return instance;
        }

        public T NewProxyInstance<T>()
        {
            return this.NewProxyInstance<T>(typeof(T));
        }

        public object NewProxyInstance(Type type)
        {
            return this.NewProxyInstance<object>(type);
        }

        private static ObjectActivator<T> GetActivator<T>(Type type)
        {
            var proxyBuilder = new ProxyTypeBuilder();

            Type proxyType = proxyBuilder.BuildProxyType(type);
            ObjectActivator<T> activator = ObjectActivator.GetActivator<T>(proxyType);

            return activator;
        }
    }
}