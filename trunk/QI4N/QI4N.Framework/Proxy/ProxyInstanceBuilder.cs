namespace QI4N.Framework.Proxy
{
    using System;
    using System.Reflection;

    using Activation;

    public static class ProxyInstanceBuilder
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

        public static object NewProxyInstance(Type type, InvocationHandler handler)
        {
            var proxyBuilder = new InvocationProxyTypeBuilder();

            Type proxyType = proxyBuilder.BuildProxyType(type);
            ObjectActivator<object> activator = ObjectActivator.GetActivator<object>(proxyType);

            var instance = activator();
            FieldInfo defaultHandlerField = proxyType.GetField("defaultHandler");
            defaultHandlerField.SetValue(instance,handler);

            return instance;
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