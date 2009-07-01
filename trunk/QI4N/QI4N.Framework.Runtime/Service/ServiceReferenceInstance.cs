namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ServiceReferenceInstance : ServiceReference
    {
        public ServiceModel ServiceModel;

        public ServiceReferenceInstance(ServiceModel model, ModuleInstance instance)
        {
        }

        public string Identity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Get()
        {
            throw new NotImplementedException();
        }

        public CompositeInstance GetInstance()
        {
            throw new NotImplementedException();
        }

        public class ServiceInvocationHandler : InvocationHandler
        {
            private readonly ServiceReferenceInstance owner;

            public ServiceInvocationHandler(ServiceReferenceInstance instance)
            {
                this.owner = instance;
            }

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
}