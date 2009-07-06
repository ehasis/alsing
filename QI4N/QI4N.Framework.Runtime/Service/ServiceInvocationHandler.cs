namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class ServiceInvocationHandler : InvocationHandler
    {
        private readonly ServiceReferenceInstance owner;

        public ServiceInvocationHandler(ServiceReferenceInstance instance)
        {
            this.owner = instance;
        }

        [DebuggerStepThrough]
        //[DebuggerHidden]
        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            if (method.Name == "ToString")
            {
                return this.owner.ServiceModel.ToString();
            }
            CompositeInstance instance = this.owner.GetInstance();

            return instance.Invoke(proxy, method, args);
        }
    }
}