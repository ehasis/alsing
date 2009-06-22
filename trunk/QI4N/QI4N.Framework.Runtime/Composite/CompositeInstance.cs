namespace QI4N.Framework.Runtime
{
    using System;

    public interface CompositeInstance : InvocationHandler
    {
        Composite Proxy { get; set; }

        object NewProxy(Type type);

        ModuleInstance ModuleInstance { get; set; }
    }
}