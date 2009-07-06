namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class ServiceReferenceInstance : ServiceReference
    {
        private readonly ModuleInstance moduleInstance;

        private object serviceProxy;

        public ServiceReferenceInstance(ServiceModel serviceModel, ModuleInstance moduleInstance)
        {
            this.ServiceModel = serviceModel;
            this.moduleInstance = moduleInstance;

            serviceProxy = this.NewProxy();
        }

        public object NewProxy()
        {
            return ServiceModel.NewProxy(new ServiceInvocationHandler(this));
        }

        public string Identity
        {
            get
            {
                return this.ServiceModel.Identity;
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

        public ServiceModel ServiceModel { get; private set; }

        public object Get()
        {
            return serviceProxy;
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
}