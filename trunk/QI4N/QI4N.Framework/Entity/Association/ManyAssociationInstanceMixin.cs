namespace QI4N.Framework
{
    using System.Collections.ObjectModel;

    public class ManyAssociationInstanceMixin<T> : Collection<T>, ManyAssociation<T>
    {
    }
}