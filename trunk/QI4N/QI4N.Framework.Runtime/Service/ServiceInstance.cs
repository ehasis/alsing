namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    public sealed class ServiceInstance : AbstractCompositeInstance
    {
        [DebuggerStepThrough]
        //[DebuggerHidden]
        public ServiceInstance(ServiceModel compositeModel, ModuleInstance moduleInstance, object[] mixins)
                : base(compositeModel, moduleInstance, mixins, null)
        {
        }
    }
}