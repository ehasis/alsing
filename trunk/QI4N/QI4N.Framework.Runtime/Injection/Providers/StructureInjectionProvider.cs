namespace QI4N.Framework.Runtime
{
    using System;

    public class StructureInjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (typeof(TransientBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.Module.TransientBuilderFactory;
            }
            if (typeof(ObjectBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.Module.ObjectBuilderFactory;
            }
            if (typeof(ValueBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.Module.ValueBuilderFactory;
            }
            if (typeof(UnitOfWorkFactory).IsAssignableFrom(fieldType))
            {
                return context.Module.UnitOfWorkFactory;
            }
            if (typeof(ServiceFinder).IsAssignableFrom(fieldType))
            {
                return context.Module.ServiceFinder;
            }
            if (typeof(Module).IsAssignableFrom(fieldType))
            {
                return context.Module;
            }
            if (typeof(Layer).IsAssignableFrom(fieldType))
            {
                return context.Module.Layer;
            }
            if (typeof(Application).IsAssignableFrom(fieldType))
            {
                return context.Module.Layer.Application;
            }
            //else if( typeof( Qi4j).IsAssignableFrom(fieldType) ) || typeof( Qi4jSPI).IsAssignableFrom(fieldType) )
            //{
            //    return context.ModuleInstance.layerInstance().applicationInstance().runtime();
            //}

            return null;
        }
    }
}