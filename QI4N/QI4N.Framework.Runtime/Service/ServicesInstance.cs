namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ServicesInstance
    {
        private readonly Dictionary<string, ServiceReference> mapIdentityServiceReference = new Dictionary<string, ServiceReference>();

        private readonly List<ServiceReference> serviceReferences;

        private readonly ServicesModel servicesModel;

        public ServicesInstance(ServicesModel servicesModel, List<ServiceReference> serviceReferences)
        {
            this.servicesModel = servicesModel;
            this.serviceReferences = serviceReferences;

            foreach (ServiceReference serviceReference in serviceReferences)
            {
                this.mapIdentityServiceReference.Add(serviceReference.Identity, serviceReference);
            }
        }

        public ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            ServiceModel serviceModel = this.servicesModel.GetServiceFor(type, visibility);

            ServiceReference serviceRef = null;
            if (serviceModel != null)
            {
                serviceRef = this.mapIdentityServiceReference[serviceModel.Identity];
            }

            return serviceRef;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            var serviceModels = new List<ServiceModel>();
            this.servicesModel.GetServicesFor(type, visibility, serviceModels);
            foreach (ServiceModel serviceModel in serviceModels)
            {
                serviceReferences.Add(this.mapIdentityServiceReference[serviceModel.Identity]);
            }
        }
    }
}