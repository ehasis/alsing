namespace QI4N.Framework
{
    using System;

    using Runtime;

    public class CompositeBuilderFactoryInstance : CompositeBuilderFactory
    {
        private readonly ModuleInstance moduleInstance;

        public CompositeBuilderFactoryInstance(ModuleInstance moduleInstance)
        {
            this.moduleInstance = moduleInstance;
        }

        public T NewComposite<T>()
        {
            CompositeBuilder<T> builder = GetBuilder<T>(typeof(T));
            return builder.NewInstance();
        }

        public CompositeBuilder<T> NewCompositeBuilder<T>()
        {
            CompositeBuilder<T> builder = GetBuilder<T>(typeof(T));
            return builder;
        }

        public CompositeBuilder<object> NewCompositeBuilder(Type mixinType)
        {
            CompositeBuilder<object> builder = GetBuilder<object>(mixinType);
            return builder;
        }

        private CompositeBuilder<T> GetBuilder<T>(Type mixinType)
        {
            CompositeFinder finder = moduleInstance.FindCompositeModel( mixinType );

            if( finder.Model == null )
            {
                throw new Exception("Composite not found");
            }

            return new CompositeBuilderInstance<T>(finder.Module, finder.Model);
        }


    }

    public class CompositeFinder
    {
        public CompositeModel Model
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ModuleInstance Module
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}