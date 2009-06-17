namespace QI4N.Framework.Runtime
{
    using System;

    public class TransientBuilderFactoryInstance : TransientBuilderFactory
    {
        private readonly ModuleInstance moduleInstance;

        public TransientBuilderFactoryInstance(ModuleInstance moduleInstance)
        {
            this.moduleInstance = moduleInstance;
        }

        public T NewTransient<T>()
        {
            TransientBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder.NewInstance();
        }

        public TransientBuilder<T> NewTransientBuilder<T>()
        {
            TransientBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder;
        }

        public TransientBuilder<object> NewTransientBuilder(Type mixinType)
        {
            TransientBuilder<object> builder = this.GetBuilder<object>(mixinType);
            return builder;
        }

        private TransientBuilder<T> GetBuilder<T>(Type mixinType)
        {
            CompositeFinder finder = this.moduleInstance.FindCompositeModel(mixinType);

            if (finder.Model == null)
            {
                throw new Exception("Composite not found");
            }

            return new CompositeBuilderInstance<T>(finder.Module, finder.Model);
        }
    }
}