namespace QI4N.Framework.Runtime
{
    public class ImportedServiceInstance
    {
        private readonly ServiceImporter importer;

        private readonly object instance;


        public ImportedServiceInstance(object instance, ServiceImporter importer)
        {
            this.importer = importer;
            this.instance = instance;
        }

        public ServiceImporter Importer
        {
            get
            {
                return this.importer;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.importer.IsActive(instance);
            }
        }

        public object Instance
        {
            get
            {
                return instance;
            }
        }
    }
}