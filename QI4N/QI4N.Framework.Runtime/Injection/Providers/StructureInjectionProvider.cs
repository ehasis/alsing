namespace QI4N.Framework.Runtime
{
    using System;

    public class StructureInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (fieldType == typeof(TransientBuilderFactory))
            {
                return context.ModuleInstance.TransientBuilderFactory;
            }
            if (fieldType == typeof(ObjectBuilderFactory))
            {
                return context.ModuleInstance.ObjectBuilderFactory;
            }
            if (fieldType == typeof(ValueBuilderFactory))
            {
                return context.ModuleInstance.ValueBuilderFactory;
            }
            if (fieldType == typeof(UnitOfWorkFactory))
            {
                return context.ModuleInstance.UnitOfWorkFactory;
            }
            if (fieldType == typeof(ServiceFinder))
            {
                return context.ModuleInstance.ServiceFinder;
            }
            if (fieldType == typeof(ModuleInstance))
            {
                return context.ModuleInstance;
            }
            if (fieldType == typeof(Layer))
            {
                return context.ModuleInstance.LayerInstance;
            }
            if (fieldType == typeof(Application))
            {
                return context.ModuleInstance.LayerInstance.ApplicationInstance;
            }
            //else if( typeof( Qi4j) ) || typeof( Qi4jSPI) )
            //{
            //    return context.ModuleInstance.layerInstance().applicationInstance().runtime();
            //}

            return null;
        }
    }
}