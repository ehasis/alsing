namespace QI4N.Framework
{
    using System;

    [Mixins(typeof(PropertyMixin))]
    public interface Composite
    {
        Type CompositeType { get; }
    }
}