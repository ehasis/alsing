namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    using Runtime;

    public class DefaultCompositeInstance : CompositeInstance
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
            throw new NotImplementedException();
        }

        public string ToURI()
        {
            return "hello";
        }
    }
}