namespace QI4N.Framework
{
    [Mixins(typeof(ManyAssociationMixin<>))]
    public interface ManyAssociation<T>
    {
        void Add(T item);

        void Remove(T item);

        void Clear();
    }
}