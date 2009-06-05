namespace QI4N.Framework
{
    [Mixins(typeof(IdentityMixin))]
    public interface Identity
    {
        [Immutable]
        Property<string> Identity { get; }
    }
}