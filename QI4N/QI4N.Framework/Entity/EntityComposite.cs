namespace QI4N.Framework
{
    [Mixins(typeof(AssociationMixin))]
    public interface EntityComposite : Identity, Entity, Composite
    {
    }
}