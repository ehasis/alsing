namespace QI4N.Framework.Runtime
{
    using System;

    public class ValueInstance : AbstractCompositeInstance
    {
        public ValueInstance(ValueModel valueModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state)
            : base(valueModel, moduleInstance, mixins, state)
        {
        }

        public static ValueInstance GetValueInstance(ValueComposite composite)
        {
            return (ValueInstance)JavaProxy.Proxy.GetInvocationHandler(composite);
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || !JavaProxy.Proxy.IsProxyClass(o.GetType()))
            {
                return false;
            }

            try
            {
                var that = (ValueInstance)JavaProxy.Proxy.GetInvocationHandler(o);
                return this.State.Equals(that.State);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.State.GetHashCode();
        }
    }
}