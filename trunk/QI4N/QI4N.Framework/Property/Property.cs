namespace QI4N.Framework
{
    public interface Property<T> : AbstractProperty, CompositePropertyInfo
    {
        new T Value { get; set; }

        void Set(T value);

        T Get();
    }
}