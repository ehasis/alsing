namespace QI4N.Framework.Runtime
{
    public sealed class ServiceInstance : AbstractCompositeInstance
    {
        public ServiceInstance(ServiceModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state)
                : base(compositeModel, moduleInstance, mixins, state)
        {
        }
    }
}