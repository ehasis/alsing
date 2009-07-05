namespace QI4N.Framework.Runtime
{
    using System;

    public class UsesInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            object obj = context.Uses.UseForType(fieldType);

            if (obj != null)
                return obj;

            ModuleInstance moduleInstance = context.ModuleInstance;

            var compositeFinder = moduleInstance.FindCompositeModel(fieldType);
            if (compositeFinder.Model != null)
            {
                obj = compositeFinder.Model.NewCompositeInstance(moduleInstance, context.Uses, context.State);
                context.Uses.Use(obj);
                return obj;
            }


            return null;
        }
    }
}