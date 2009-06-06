namespace QI4N.Framework
{
    [Mixins(typeof(PropertyInstanceMixin<>))]   
    public interface Property<T> : Property, PropertyInfo<T>
    {
        T Value { get; set; }

        void Set(T value);

        T Get();
    }

    public interface Property
    {
    }
}