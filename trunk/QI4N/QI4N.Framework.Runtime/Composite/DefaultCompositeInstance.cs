using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QI4N.Framework.Runtime;

namespace QI4N.Framework
{
    public class DefaultCompositeInstance : CompositeInstance
    {
        public DefaultCompositeInstance(CompositeModel model, ModuleInstance instance, object[] mixins, StateHolder state)
        {
            this.Proxy = model.NewProxy(this);
        }

        public object[] Mixins { get; set; }
       
        public Composite Proxy { get; set; }

        public CompositeModel Model { get; set; }

        public string ToURI()
        {
            return "hello";
        }

        public object Invoke(object proxy, System.Reflection.MethodInfo method, object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
