namespace QI4N.Framework
{
    [Mixins(typeof(AssociationInstanceMixin<>))]
    public interface Association<T> : Association
    {
        T Get();

        void Set(T value);
    }

    public interface Association : AbstractAssociation
    {
    }
}