namespace QI4N.Framework.JavaProxy
{
    using System;
    using System.Reflection;

    using Reflection;

    public static class Proxy
    {
        public static Type BuildProxyType(Type compositeType)
        {
            var builder = new InvocationProxyTypeBuilder();
            Type type = builder.BuildProxyType(compositeType);
            return type;
        }

        public static InvocationHandler GetInvocationHandler(object proxy)
        {
            FieldInfo defaultHandlerField = proxy.GetType().GetField("defaultHandler");
            var handler = defaultHandlerField.GetValue(proxy) as InvocationHandler;
            return handler;
        }

        public static bool IsProxyClass(Type type)
        {
            FieldInfo defaultHandlerField = type.GetField("defaultHandler");
            return defaultHandlerField != null;
        }

        public static object NewProxyInstance(Type type, InvocationHandler handler)
        {
            Type proxyType = BuildProxyType(type);
            object instance = Activator.CreateInstance(proxyType, new object[]
                                                                      {
                                                                              handler
                                                                      });
            return instance;
        }
    }
}