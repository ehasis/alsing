namespace QI4N.Framework
{
    public interface Property<T> : AbstractProperty, PropertyInfo<T>
    {
        new T Value { get; set; }

        void Set(T value);

        T Get();
    }
}