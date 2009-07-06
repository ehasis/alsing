namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class ServiceReferenceInstance : ServiceReference
    {
        private readonly ModuleInstance moduleInstance;

        private readonly object serviceProxy;

        private readonly object syncroot = new object();

        private ServiceInstance instance;

        public ServiceReferenceInstance(ServiceModel serviceModel, ModuleInstance moduleInstance)
        {
            this.ServiceModel = serviceModel;
            this.moduleInstance = moduleInstance;

            this.serviceProxy = this.NewProxy();
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
            return this.serviceProxy;
        }

        [DebuggerStepThrough]
        //[DebuggerHidden]
        public CompositeInstance GetInstance()
        {
            lock (this.syncroot)
            {
                if (this.instance == null)
                {
                    this.instance = this.ServiceModel.NewInstance(this.moduleInstance);
                }
            }

            return this.instance;
        }

        public object NewProxy()
        {
            return this.ServiceModel.NewProxy(new ServiceInvocationHandler(this));
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