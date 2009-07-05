namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public sealed class ServicesModel
    {
        private readonly List<ServiceModel> serviceModels;

        public ServicesModel(List<ServiceModel> serviceModels)
        {
            this.serviceModels = serviceModels;
        }


        public ServiceModel GetServiceFor(Type type, Visibility visibility)
        {
            foreach (ServiceModel serviceModel in this.serviceModels)
            {
                if (serviceModel.IsServiceFor(type, visibility))
                {
                    return serviceModel;
                }
            }

            return null;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceModel> models)
        {
            foreach (ServiceModel serviceModel in this.serviceModels)
            {
                if (serviceModel.IsServiceFor(type, visibility))
                {
                    models.Add(serviceModel);
                }
            }
        }

        public ServicesInstance NewInstance(ModuleInstance moduleInstance)
        {
            var serviceReferences = new List<ServiceReference>();
            foreach (ServiceModel serviceModel in this.serviceModels)
            {
                var serviceReferenceInstance = new ServiceReferenceInstance(serviceModel, moduleInstance);
                serviceReferences.Add(serviceReferenceInstance);
            }

            return new ServicesInstance(this, serviceReferences);
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}