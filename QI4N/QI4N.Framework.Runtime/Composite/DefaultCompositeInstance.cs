namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class DefaultCompositeInstance : CompositeInstance, MixinsInstance
    {
        public DefaultCompositeInstance(CompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state)
        {
            this.CompositeModel = compositeModel;
            this.ModuleInstance = moduleInstance;
            this.Mixins = mixins;
            this.State = state;
            this.Proxy = compositeModel.NewProxy(this);
        }

        public CompositeModel CompositeModel { get; set; }

        public object[] Mixins { get; set; }

        public ModuleInstance ModuleInstance { get; set; }

        public Composite Proxy { get; set; }

        public StateHolder State { get; set; }

        [DebuggerStepThrough]
        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.CompositeModel.Invoke(this, this, proxy, method, args, this.ModuleInstance);
        }

        [DebuggerStepThrough]
        public object Invoke(object composite, object[] args, CompositeMethodInstance methodInstance)
        {
            object mixin = methodInstance.GetMixin(this.Mixins);
            return methodInstance.Invoke(composite, args, mixin);
        }

        public object InvokeObject(object proxy, object[] args, MethodInfo method)
        {
            //this is a method that does not belong to a mixin
            //e.g. equals, tostring, gethashcode
            return this.Invoke(this, method, args);
        }

        public string ToURI()
        {
            return "hello";
        }
    }
}