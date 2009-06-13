using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Reflection
{
    using System.Reflection;

    using Activation;

    public static class Proxy
    {
        public static object NewProxyInstance(Type type, InvocationHandler handler)
        {
            var proxyBuilder = new InvocationProxyTypeBuilder();

            Type proxyType = proxyBuilder.BuildProxyType(type);
            ObjectActivator<object> activator = ObjectActivator.GetActivator<object>(proxyType);

            var instance = activator();
            FieldInfo defaultHandlerField = proxyType.GetField("defaultHandler");
            defaultHandlerField.SetValue(instance, handler);

            return instance;
        }

        public static InvocationHandler GetInvocationHandler(Composite proxy)
        {
            FieldInfo defaultHandlerField = proxy.GetType().GetField("defaultHandler");
            var handler = defaultHandlerField.GetValue(proxy) as InvocationHandler;
            return handler;
        }
    }
}
