namespace QI4N.Framework
{
    public interface CompositePropertyInfo
    {
        bool IsImmutable { get; }

        bool IsComputed { get; }

        string QualifiedName { get; }
    }
}