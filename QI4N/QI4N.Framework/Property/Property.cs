namespace QI4N.Framework
{
    [Mixins(typeof(PropertyInstanceMixin<>))]
    public interface Property<T> : Property, PropertyInfo<T>
    {
        T Value { get; set; }
    }

    public interface Property
    {
    }
}