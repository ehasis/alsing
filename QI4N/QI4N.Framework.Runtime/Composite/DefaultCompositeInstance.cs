namespace QI4N.Framework.Runtime
{
    public class DefaultCompositeInstance : AbstractCompositeInstance
    {
        public DefaultCompositeInstance(AbstractCompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state) : base(compositeModel, moduleInstance, mixins, state)
        {
        }

        public override string ToString()
        {
            return "I am the Composite Instance";
        }
    }
}