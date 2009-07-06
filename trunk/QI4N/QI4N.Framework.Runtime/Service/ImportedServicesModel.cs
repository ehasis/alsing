namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ImportedServicesModel
    {
        private readonly List<ImportedServiceModel> importedServiceModels;

        private List<ImportedServiceReferenceInstance> serviceReferences;

        public ImportedServicesModel(List<ImportedServiceModel> importedServiceModels)
        {
            this.importedServiceModels = importedServiceModels;
        }

        public ImportedServiceModel GetServiceFor(Type type, Visibility visibility)
        {
            foreach (ImportedServiceModel serviceModel in this.importedServiceModels)
            {
                if (serviceModel.IsServiceFor(type, visibility))
                {
                    return serviceModel;
                }
            }

            return null;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ImportedServiceModel> serviceModels)
        {
            foreach (ImportedServiceModel importedServiceModel in this.importedServiceModels)
            {
                if (importedServiceModel.IsServiceFor(type, visibility))
                {
                    serviceModels.Add(importedServiceModel);
                }
            }
        }

        public ImportedServicesInstance NewInstance(ModuleInstance module)
        {
            this.serviceReferences = new List<ImportedServiceReferenceInstance>();
            foreach (ImportedServiceModel serviceModel in this.importedServiceModels)
            {
                var serviceReferenceInstance = new ImportedServiceReferenceInstance(serviceModel, module);
                this.serviceReferences.Add(serviceReferenceInstance);
            }

            return new ImportedServicesInstance(this, this.serviceReferences);
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}