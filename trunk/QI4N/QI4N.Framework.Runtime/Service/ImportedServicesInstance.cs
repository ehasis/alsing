namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ImportedServicesInstance
    {
        private readonly Dictionary<string, ServiceReference> mapIdentityServiceReference = new Dictionary<string, ServiceReference>();

        private readonly object references;

        private readonly ImportedServicesModel servicesModel;

        public ImportedServicesInstance(ImportedServicesModel servicesModel, List<ImportedServiceReferenceInstance> serviceReferences)
        {
            this.servicesModel = servicesModel;
            this.references = this.references;

            foreach (ServiceReference serviceReference in serviceReferences)
            {
                this.mapIdentityServiceReference.Add(serviceReference.Identity, serviceReference);
            }
        }

        public ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            ImportedServiceModel serviceModel = this.servicesModel.GetServiceFor(type, visibility);

            ServiceReference serviceRef = null;
            if (serviceModel != null)
            {
                serviceRef = this.mapIdentityServiceReference[serviceModel.Identity];
            }

            return serviceRef;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            var serviceModels = new List<ImportedServiceModel>();
            this.servicesModel.GetServicesFor(type, visibility, serviceModels);
            foreach (ImportedServiceModel serviceModel in serviceModels)
            {
                serviceReferences.Add(this.mapIdentityServiceReference[serviceModel.Identity]);
            }
        }
    }
}