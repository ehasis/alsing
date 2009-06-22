namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class ServiceReferenceInstance<T> : ServiceReference<T>
    {
        public ServiceModel ServiceModel;

        public class ServiceInvocationHandler : InvocationHandler
        {
            private readonly ServiceReferenceInstance<T> owner;

            public ServiceInvocationHandler(ServiceReferenceInstance<T> instance)
            {
                this.owner = instance;
            }

            public object Invoke(object proxy, MethodInfo method, object[] args)
            {
                if (method.Name == "ToString")
                {
                    return this.owner.ServiceModel.ToString();
                }
                CompositeInstance instance = owner.GetInstance();

                return instance.Invoke(proxy, method, args);
            }
        }

        public CompositeInstance GetInstance()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ServiceReference<T>
    {
    }
}