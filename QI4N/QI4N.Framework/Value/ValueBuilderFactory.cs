namespace QI4N.Framework
{
    using System;

    public interface ValueBuilderFactory
    {
        T NewValue<T>();

        TransientBuilder<T> NewValueBuilder<T>();

        TransientBuilder<object> NewValueBuilder(Type fragmentType);
    }
}