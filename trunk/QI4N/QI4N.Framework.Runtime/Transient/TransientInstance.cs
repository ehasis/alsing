namespace QI4N.Framework.Runtime
{
    public sealed class TransientInstance : AbstractCompositeInstance
    {
        public TransientInstance(AbstractCompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state) : base(compositeModel, moduleInstance, mixins, state)
        {
        }
    }
}