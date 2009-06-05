namespace QI4N.Framework.Proxy
{
    using System;

    using Activation;

    public class ProxyInstanceBuilder<T>
    {
        private static readonly object syncRoot = new object();

        private static ObjectActivator<T> proxyActivator;

        public T NewInstance()
        {
            EnsureActivatorExists();

            T instance = proxyActivator.Invoke();

            return instance;
        }

        private static ObjectActivator<T> BuildActivator()
        {
            var proxyBuilder = new ProxyTypeBuilder();

            Type proxyType = proxyBuilder.BuildProxyType(typeof(T));
            ObjectActivator<T> activator = ObjectActivator.GetActivator<T>(proxyType);

            return activator;
        }

        private static void EnsureActivatorExists()
        {
            lock (syncRoot)
            {
                if (proxyActivator == null)
                {
                    proxyActivator = BuildActivator();
                }
            }
        }
    }
}