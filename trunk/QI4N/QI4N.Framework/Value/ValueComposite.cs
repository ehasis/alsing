namespace QI4N.Framework
{
    [Immutable]
    [Mixins(typeof(PropertyGetterMixin))]
    [Mixins(typeof(PropertySetterMixin))]
    public interface ValueComposite : Value, Composite
    {
    }
}