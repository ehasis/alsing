namespace QI4N.Framework
{
    public interface ServiceImporter
    {
        object importService(ImportedServiceDescriptor serviceDescriptor);

        bool IsActive(object instance);
    }
}