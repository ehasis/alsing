namespace QI4N.Framework
{
    using System;

    public class ManyAssociationMixin<T> : ManyAssociation<T>
    {
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}