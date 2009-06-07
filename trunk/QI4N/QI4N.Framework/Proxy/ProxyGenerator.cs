namespace QI4N.Framework.Proxy
{
    using System;

    using Activation;

    using Internal;

    public static class ProxyGenerator
    {
        public static T NewProxyInstance<T>(Type type)
        {
            ObjectActivator<T> proxyActivator = GetActivator<T>(type);

            T instance = proxyActivator.Invoke();

            return instance;
        }

        public static T NewProxyInstance<T>()
        {
            return NewProxyInstance<T>(typeof(T));
        }

        public static object NewProxyInstance(Type type)
        {
            return NewProxyInstance<object>(type);
        }

        internal static object NewProxyInstance(Type[] interfaces, StateInvocationHandler handler)
        {
            throw new NotImplementedException();
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