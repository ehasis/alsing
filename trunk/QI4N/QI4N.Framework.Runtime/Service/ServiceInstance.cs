namespace QI4N.Framework.Runtime
{
    public sealed class ServiceInstance : AbstractCompositeInstance
    {
        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public ServiceInstance(ServiceModel compositeModel, ModuleInstance moduleInstance, object[] mixins)
                : base(compositeModel, moduleInstance, mixins, null)
        {
        }
    }
}