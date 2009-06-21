namespace QI4N.Framework.Runtime
{
    using System;

    public interface CompositeInstance : InvocationHandler
    {
        object[] Mixins { get; set; }

        Composite Proxy { get; set; }

        AbstractCompositeModel CompositeModel { get; }

        string ToURI();

        object NewProxy(Type type);
    }
}