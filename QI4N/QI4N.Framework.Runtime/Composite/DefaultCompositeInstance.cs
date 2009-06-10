namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    using Runtime;

    public class DefaultCompositeInstance : CompositeInstance, MixinsInstance
    {
        public DefaultCompositeInstance(CompositeModel model, ModuleInstance instance, object[] mixins, StateHolder state)
        {
            this.Model = model;
            this.Instance = instance;
            this.Mixins = mixins;
            this.State = state;
            this.Proxy = model.NewProxy(this);
        }

        public ModuleInstance Instance { get; set; }

        public object[] Mixins { get; set; }

        public CompositeModel Model { get; set; }

        public Composite Proxy { get; set; }

        public StateHolder State { get; set; }

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.Model.Invoke(this, this, proxy, method, args, this.Instance);
        }

        public object Invoke(object composite, object[] args, CompositeMethodInstance methodInstance)
        {
            throw new NotImplementedException();
        }

        public object InvokeObject(object proxy, object[] args, MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public string ToURI()
        {
            return "hello";
        }
    }
}