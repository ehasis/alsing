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