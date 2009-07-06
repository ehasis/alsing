namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ServiceReferencesFinder : ModuleVisitor
    {
        public List<ServiceReference> Services;

        public Type Type { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            moduleInstance.GetServicesFor(this.Type, visibility, this.Services);

            return true;
        }
    }
}