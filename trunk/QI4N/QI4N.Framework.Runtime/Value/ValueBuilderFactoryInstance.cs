namespace QI4N.Framework.Runtime
{
    using System;

    public class ValueBuilderFactoryInstance : ValueBuilderFactory
    {
        public T NewValue<T>()
        {
            throw new NotImplementedException();
        }

        public TransientBuilder<T> NewValueBuilder<T>()
        {
            throw new NotImplementedException();
        }

        public TransientBuilder<object> NewValueBuilder(Type fragmentType)
        {
            throw new NotImplementedException();
        }
    }
}