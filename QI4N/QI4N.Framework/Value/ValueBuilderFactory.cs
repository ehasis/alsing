namespace QI4N.Framework
{
    using System;

    public interface ValueBuilderFactory
    {
        T NewValue<T>();

        ValueBuilder<T> NewValueBuilder<T>();

        ValueBuilder<object> NewValueBuilder(Type fragmentType);
    }
}