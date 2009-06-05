namespace QI4N.Framework
{
    public interface PropertyInfo<T>
    {
        bool IsMutable { get; }

        bool IsComputed { get; }
    }
}