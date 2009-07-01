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

        public object Instance
        {
            get
            {
                return this.instance;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.importer.IsActive(this.instance);
            }
        }
    }
}