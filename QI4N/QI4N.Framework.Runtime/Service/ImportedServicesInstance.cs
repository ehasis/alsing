namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ImportedServicesInstance
    {
        private readonly ImportedServicesModel servicesModel;

        private readonly object references;

        private readonly Dictionary<string, ServiceReference> mapIdentityServiceReference = new Dictionary<string, ServiceReference>();

        public ImportedServicesInstance(ImportedServicesModel servicesModel, List<ImportedServiceReferenceInstance> serviceReferences)
        {
            this.servicesModel = servicesModel;
            this.references = references;

            foreach( ServiceReference serviceReference in serviceReferences )
            {
                mapIdentityServiceReference.Add( serviceReference.Identity, serviceReference );
            }
        }

        public ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            ImportedServiceModel serviceModel = servicesModel.GetServiceFor(type, visibility);

            ServiceReference serviceRef = null;
            if (serviceModel != null)
            {
                serviceRef = mapIdentityServiceReference[serviceModel.Identity];
            }

            return serviceRef;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            var serviceModels = new List<ImportedServiceModel>();
            servicesModel.GetServicesFor(type, visibility, serviceModels);
            foreach (ImportedServiceModel serviceModel in serviceModels)
            {
                serviceReferences.Add(mapIdentityServiceReference[serviceModel.Identity]);
            }
        }
    }
}