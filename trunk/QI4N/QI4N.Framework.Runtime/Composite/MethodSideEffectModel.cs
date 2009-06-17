namespace QI4N.Framework.Runtime
{
    using System;

    public class MethodSideEffectModel
    {
        public MethodSideEffectModel(Type clazz)
        {
            throw new NotImplementedException();
        }

        public bool IsGeneric
        {
            get
            {
                return false;
            }
        }

        public object NewInstance(ModuleInstance instance, SideEffectInvocationHandlerResult result, ProxyReferenceInvocationHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}