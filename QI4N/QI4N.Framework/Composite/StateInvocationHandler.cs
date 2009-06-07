namespace QI4N.Framework.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

    [AppliesToEverything]
    public class StateInvocationHandler : InvocationHandler
    {
        private readonly CompositeContext context;

        private readonly ModuleInstance moduleInstance;

        private readonly IDictionary<MethodInfo, Property> properties;

        public StateInvocationHandler(CompositeContext context, ModuleInstance moduleInstance, IDictionary<MethodInfo, Property> properties)
        {
            this.context = context;
            this.moduleInstance = moduleInstance;
            this.properties = properties;
        }

        public object Invoke(object proxy, MethodInfo method, object[] objects)
        {
            if (typeof(Property).IsAssignableFrom(method.ReturnType))
            {
                Property propertyInstance;

                if (!this.properties.TryGetValue(method, out propertyInstance))
                {
                    propertyInstance = ProxyInstanceBuilder.NewProxyInstance(method.ReturnType) as Property;
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