namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public abstract class AbstractCompositeInstance : CompositeInstance, MixinsInstance
    {
        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        protected AbstractCompositeInstance(AbstractCompositeModel compositeModel, ModuleInstance moduleInstance, object[] mixins, StateHolder state)
        {
            this.CompositeModel = compositeModel;
            this.ModuleInstance = moduleInstance;
            this.Mixins = mixins;
            this.State = state;
            this.Proxy = compositeModel.NewProxy(this);
        }

        public AbstractCompositeModel CompositeModel { get; set; }

        public object[] Mixins { get; set; }

        public ModuleInstance ModuleInstance { get; set; }

        public Composite Proxy { get; set; }

        public StateHolder State { get; set; }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.CompositeModel.Invoke(this, this, proxy, method, args, this.ModuleInstance);
        }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object Invoke(object composite, object[] args, CompositeMethodInstance methodInstance)
        {
            object mixin = methodInstance.GetMixin(this.Mixins);
            return methodInstance.Invoke(composite, args, mixin);
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object InvokeObject(object proxy, object[] args, MethodInfo method)
        {
            return method.Invoke(this, args);
        }

        public object NewProxy(Type mixinType)
        {
            return this.CompositeModel.NewProxy(this, mixinType);
        }

        public string ToURI()
        {
            return "hello";
        }
    }
}