namespace QI4N.Framework.Runtime
{
    using System;

    public sealed class ValueBuilderFactoryInstance : ValueBuilderFactory
    {
        private readonly ModuleInstance moduleInstance;

        public ValueBuilderFactoryInstance(ModuleInstance moduleInstance)
        {
            this.moduleInstance = moduleInstance;
        }

        public T NewValue<T>()
        {
            ValueBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder.NewInstance();
        }

        public ValueBuilder<T> NewValueBuilder<T>()
        {
            ValueBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder;
        }

        public ValueBuilder<object> NewValueBuilder(Type mixinType)
        {
            ValueBuilder<object> builder = this.GetBuilder<object>(mixinType);
            return builder;
        }

        private ValueBuilder<T> GetBuilder<T>(Type mixinType)
        {
            ValueFinder finder = this.moduleInstance.FindValueModel(mixinType);

            if (finder.Model == null)
            {
                throw new Exception("Composite not found");
            }

            return new ValueBuilderInstance<T>(finder.Module, finder.Model);
        }
    }

    
}