namespace QI4N.Framework.Runtime
{
    using System;

    public class StructureInjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (fieldType == typeof(TransientBuilderFactory))
            {
                return context.Module.TransientBuilderFactory;
            }
            if (fieldType == typeof(ObjectBuilderFactory))
            {
                return context.Module.ObjectBuilderFactory;
            }
            if (fieldType == typeof(ValueBuilderFactory))
            {
                return context.Module.ValueBuilderFactory;
            }
            if (fieldType == typeof(UnitOfWorkFactory))
            {
                return context.Module.UnitOfWorkFactory;
            }
            if (fieldType == typeof(ServiceFinder))
            {
                return context.Module.ServiceFinder;
            }
            if (fieldType == typeof(Module))
            {
                return context.Module;
            }
            if (fieldType == typeof(Layer))
            {
                return context.Module.Layer;
            }
            if (fieldType == typeof(Application))
            {
                return context.Module.Layer.Application;
            }
            //else if( typeof( Qi4j) ) || typeof( Qi4jSPI) )
            //{
            //    return context.ModuleInstance.layerInstance().applicationInstance().runtime();
            //}

            return null;
        }
    }
}