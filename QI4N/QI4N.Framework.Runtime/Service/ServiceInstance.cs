namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    public sealed class ServiceInstance : AbstractCompositeInstance
    {
        [DebuggerStepThrough]
        //[DebuggerHidden]
        public ServiceInstance(ServiceModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state)
                : base(compositeModel, moduleInstance, mixins, state)
        {
        }
    }
}