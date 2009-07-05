namespace QI4N.Framework.Runtime
{
    public class ImportedServiceReferenceInstance
    {
        private Module module;

        private ImportedServiceModel serviceModel;

        public ImportedServiceReferenceInstance(ImportedServiceModel serviceModel, Module module)
        {
            this.module = module;
            this.serviceModel = serviceModel;
        }
    }
}