namespace QI4N.Framework.Runtime
{
    public class ImportedServiceInstance<T>
    {
        private readonly ServiceImporter importer;

        private readonly T instance;


        public ImportedServiceInstance(T instance, ServiceImporter importer)
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

        public T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}