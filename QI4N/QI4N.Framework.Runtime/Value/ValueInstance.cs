namespace QI4N.Framework.Runtime
{
    using System;

    public class ValueInstance : DefaultCompositeInstance
    {
        public ValueInstance(CompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state) : base(compositeModel, moduleInstance, mixins, state)
        {
        }

        public static ValueInstance GetValueInstance(ValueComposite composite)
        {
            return (ValueInstance)Reflection.Proxy.GetInvocationHandler(composite);
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || !Reflection.Proxy.IsProxyClass(o.GetType()))
            {
                return false;
            }

            try
            {
                var that = (ValueInstance)Reflection.Proxy.GetInvocationHandler(o);
                return State.Equals(that.State);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return State.GetHashCode();
        }
    }
}