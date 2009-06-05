namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    using Proxy;

    public delegate T ProxyActivator<T>();

    public static class ProxyActivator
    {
        public static ProxyActivator<T> GetActivator<T>(Type compositeType)
        {
            Type ob = typeof(ProxyInstanceBuilder<>);
            Type cob = ob.MakeGenericType(compositeType);
            object cobi = Activator.CreateInstance(cob);
            MethodInfo cobm = cob.GetMethod("NewInstance");
            ProxyActivator<T> activator = () => (T)cobm.Invoke(cobi, null);
            return activator;
        }
    }
}