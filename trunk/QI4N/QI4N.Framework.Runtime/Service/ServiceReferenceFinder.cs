namespace QI4N.Framework.Runtime
{
    using System;

    public class ServiceReferenceFinder : ModuleVisitor
    {
        public ServiceReference Service { get; set; }

        public Type Type { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            this.Service = moduleInstance.GetServiceFor(this.Type, visibility);

            return this.Service == null;
        }
    }
}