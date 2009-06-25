namespace QI4N.Framework
{
    using System;

    [Mixins(typeof(PropertyGetterMixin))]
    [Mixins(typeof(PropertySetterMixin))]
    public interface Composite
    {
        Type CompositeType { get; }
    }
}