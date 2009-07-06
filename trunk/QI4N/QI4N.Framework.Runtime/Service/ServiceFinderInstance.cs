namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ServiceFinderInstance : ServiceFinder
    {
        private readonly ModuleInstance owner;

        private readonly Dictionary<Type, ServiceReference> service = new Dictionary<Type, ServiceReference>();

        private readonly Dictionary<Type, IEnumerable<ServiceReference>> services = new Dictionary<Type, IEnumerable<ServiceReference>>();


        public ServiceFinderInstance(ModuleInstance owner)
        {
            this.owner = owner;
        }

        public ServiceReference FindService(Type serviceType)
        {
            ServiceReference serviceReference;
            if (!this.service.TryGetValue(serviceType, out serviceReference))
            {
                var finder = new ServiceReferenceFinder
                                 {
                                         Type = serviceType
                                 };

                this.owner.VisitModules(finder);
                serviceReference = finder.Service;
                if (serviceReference != null)
                {
                    //   serviceReference = new ServiceReferenceFacade<T>(serviceReference);
                    this.service.Add(serviceType, serviceReference);
                }
            }

            return serviceReference;
        }

        public IEnumerable<ServiceReference> FindServices(Type serviceType)
        {
            IEnumerable<ServiceReference> iterable;
            if (!this.services.TryGetValue(serviceType, out iterable))
            {
                var finder = new ServiceReferencesFinder
                                 {
                                         Type = serviceType
                                 };

                this.owner.VisitModules(finder);
                iterable = finder.Services;
                this.services.Add(serviceType, iterable);
            }

            return iterable;
        }
    }
}