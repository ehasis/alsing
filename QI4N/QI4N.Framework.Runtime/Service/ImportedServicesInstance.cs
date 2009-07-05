namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ImportedServicesInstance
    {
        private readonly ImportedServicesModel model;

        private readonly object references;

        public ImportedServicesInstance(ImportedServicesModel model, object references)
        {
            this.model = model;
            this.references = references;
        }

        public ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            throw new NotImplementedException();
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            throw new NotImplementedException();
        }
    }
}