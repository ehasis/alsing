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


        public ServicesInstance NewInstance(ModuleInstance moduleInstance)
        {
            var serviceReferences = new List<ServiceReference>();
            foreach( ServiceModel serviceModel in serviceModels )
            {
                var serviceReferenceInstance = new ServiceReferenceInstance(serviceModel, moduleInstance);
                serviceReferences.Add( serviceReferenceInstance );
            }

            return new ServicesInstance( this, serviceReferences );
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}