namespace QI4N.Framework
{
    public interface ServiceReference
    {
        string Identity { get; }

        MetaInfo MetaInfo { get; }

        object Get();

        bool IsActive { get; }
    }
}