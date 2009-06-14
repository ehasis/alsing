namespace QI4N.Framework.Runtime
{
    public class ServiceInstance : DefaultCompositeInstance
    {
        public ServiceInstance(CompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state) : base(compositeModel, moduleInstance, mixins, state)
        {
        }
    }
}