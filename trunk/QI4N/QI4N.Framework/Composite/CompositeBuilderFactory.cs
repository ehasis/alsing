namespace QI4N.Framework
{
    using System;

    public interface CompositeBuilderFactory
    {
        T NewComposite<T>();

        CompositeBuilder<T> NewCompositeBuilder<T>();

        CompositeBuilder<object> NewCompositeBuilder(Type fragmentType);
    }
}