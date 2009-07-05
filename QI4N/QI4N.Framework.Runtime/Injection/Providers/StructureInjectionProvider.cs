namespace QI4N.Framework.Runtime
{
    using System;

    public class StructureInjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (typeof(TransientBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.TransientBuilderFactory;
            }
            if (typeof(ObjectBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.ObjectBuilderFactory;
            }
            if (typeof(ValueBuilderFactory).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.ValueBuilderFactory;
            }
            if (typeof(UnitOfWorkFactory).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.UnitOfWorkFactory;
            }
            if (typeof(ServiceFinder).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.ServiceFinder;
            }
            if (typeof(Module).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance;
            }
            if (typeof(Layer).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.LayerInstance;
            }
            if (typeof(Application).IsAssignableFrom(fieldType))
            {
                return context.ModuleInstance.LayerInstance.ApplicationInstance;
            }
            //else if( typeof( Qi4j).IsAssignableFrom(fieldType) ) || typeof( Qi4jSPI).IsAssignableFrom(fieldType) )
            //{
            //    return context.ModuleInstance.layerInstance().applicationInstance().runtime();
            //}

            return null;
        }
    }
}