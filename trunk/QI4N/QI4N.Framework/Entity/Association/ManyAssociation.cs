namespace QI4N.Framework
{
    using System.Collections.Generic;

    [Mixins(typeof(ManyAssociationInstanceMixin<>))]
    public interface ManyAssociation<T> : ICollection<T> , AbstractAssociation
    {

    }
}