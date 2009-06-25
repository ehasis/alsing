namespace QI4N.Framework
{
    [Mixins(typeof(IdentityMixin))]
    public interface Identity
    {
        [Immutable]
        string Identity { get; }
    }
}