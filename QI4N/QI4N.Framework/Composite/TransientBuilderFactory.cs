namespace QI4N.Framework
{
    using System;

    public interface TransientBuilderFactory
    {
        T NewTransient<T>();

        TransientBuilder<T> NewTransientBuilder<T>();

        TransientBuilder<object> NewTransientBuilder(Type fragmentType);
    }
}