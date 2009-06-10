namespace QI4N.Framework.Runtime
{
    using System;

    public class CompositeBuilderFactoryInstance : CompositeBuilderFactory
    {
        private readonly ModuleInstance moduleInstance;

        public CompositeBuilderFactoryInstance(ModuleInstance moduleInstance)
        {
            this.moduleInstance = moduleInstance;
        }

        public T NewComposite<T>()
        {
            CompositeBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder.NewInstance();
        }

        public CompositeBuilder<T> NewCompositeBuilder<T>()
        {
            CompositeBuilder<T> builder = this.GetBuilder<T>(typeof(T));
            return builder;
        }

        public CompositeBuilder<object> NewCompositeBuilder(Type mixinType)
        {
            CompositeBuilder<object> builder = this.GetBuilder<object>(mixinType);
            return builder;
        }

        private CompositeBuilder<T> GetBuilder<T>(Type mixinType)
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