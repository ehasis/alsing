namespace QI4N.Framework
{
    [Mixins(typeof(AssociationMixin))]
    [Mixins(typeof(PropertyGetterMixin))]
    [Mixins(typeof(PropertySetterMixin))]
    public interface EntityComposite : Identity, Entity, Composite
    {
    }
}