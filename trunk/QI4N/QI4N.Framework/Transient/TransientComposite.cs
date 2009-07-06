namespace QI4N.Framework
{
    [Mixins(typeof(PropertyGetterMixin))]
    [Mixins(typeof(PropertySetterMixin))]
    public interface TransientComposite : Composite
    {
    }
}