namespace QI4N.Framework.Proxy
{
    using System;

    using Activation;

    public class ProxyInstanceBuilder
    {
        public T NewInstance<T>(Type type)
        {
            var proxyActivator = GetActivator<T>(type);

            var instance = proxyActivator.Invoke();

            return instance;
        }

        public T NewInstance<T>()
        {
            var proxyActivator = GetActivator<T>(typeof(T));

            var instance = proxyActivator.Invoke();

            return instance;
        }

        public object NewInstance(Type type)
        {
            var proxyActivator = GetActivator<object>(type);

            var instance = proxyActivator.Invoke();

            return instance;
        }

        private static ObjectActivator<T> GetActivator<T>(Type type)
        {
            var proxyBuilder = new ProxyTypeBuilder();

            Type proxyType = proxyBuilder.BuildProxyType(type);
            var activator = ObjectActivator.GetActivator<T>(proxyType);

            return activator;
        }
    }
}