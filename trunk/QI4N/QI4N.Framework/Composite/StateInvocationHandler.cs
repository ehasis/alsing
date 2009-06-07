namespace QI4N.Framework.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

    public class StateInvocationHandler : InvocationHandler
    {
        private readonly CompositeContext context;

        private readonly ModuleInstance moduleInstance;

        private readonly IDictionary<MethodInfo, AbstractProperty> properties;

        public StateInvocationHandler(CompositeContext context, ModuleInstance moduleInstance, IDictionary<MethodInfo, AbstractProperty> properties)
        {
            this.context = context;
            this.moduleInstance = moduleInstance;
            this.properties = properties;
        }

        public object Invoke(object proxy, MethodInfo method, object[] objects)
        {
            if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
            {
                AbstractProperty propertyInstance;

                if (!this.properties.TryGetValue(method, out propertyInstance))
                {
                    propertyInstance = ProxyInstanceBuilder.NewProxyInstance(method.ReturnType) as AbstractProperty;
                    //PropertyContext propertyContext = this.context.GetMethodDescriptor(method).GetCompositeMethodContext().GetPropertyContext();
                    //propertyInstance = propertyContext.NewInstance(this.moduleInstance, null, method.ReturnType);
                    this.properties.Add(method, propertyInstance);
                }
                return propertyInstance;
            }

            throw new NotSupportedException("Method does not represent state: " + method.Name);
        }
    }
}