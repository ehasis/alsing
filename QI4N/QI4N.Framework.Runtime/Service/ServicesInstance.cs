using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public class ServicesInstance
    {
        private readonly ServicesModel servicesModel;

        private readonly List<ServiceReference> serviceReferences;

        private readonly Dictionary<string, ServiceReference> mapIdentityServiceReference = new Dictionary<string, ServiceReference>();
        public ServicesInstance(ServicesModel servicesModel, List<ServiceReference> serviceReferences)
        {
            this.servicesModel = servicesModel;
            this.serviceReferences = serviceReferences;

            foreach (ServiceReference serviceReference in serviceReferences)
            {
                mapIdentityServiceReference.Add(serviceReference.Identity, serviceReference);
            }
        }

        internal ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            ServiceModel serviceModel = servicesModel.GetServiceFor(type, visibility);

            ServiceReference serviceRef = null;
            if (serviceModel != null)
            {
                serviceRef = mapIdentityServiceReference[serviceModel.Identity];
            }

            return serviceRef;
        }

        internal void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            var serviceModels = new List<ServiceModel>();
            servicesModel.GetServicesFor(type, visibility, serviceModels);
            foreach (ServiceModel serviceModel in serviceModels)
            {
                serviceReferences.Add(mapIdentityServiceReference[serviceModel.Identity]);
            }
        }
    }
}
