namespace QI4N.Framework
{
    public interface ServiceReference
    {
        string Identity { get; }

        MetaInfo MetaInfo { get; }

        bool IsActive { get; }

        object Get();
    }

    public interface ServiceReference<T> : ServiceReference
    {
        new T Get();
    }
}