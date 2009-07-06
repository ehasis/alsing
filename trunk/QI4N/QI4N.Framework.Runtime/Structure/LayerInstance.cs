namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Linq;

    public class LayerInstance : Layer
    {
        private readonly IList<ModuleInstance> moduleInstances;

        public LayerInstance(LayerModel model, ApplicationInstance applicationInstance, IList<ModuleInstance> moduleInstances, UsedLayersInstance usedLayersInstance)
        {
            this.Model = model;
            this.ApplicationInstance = applicationInstance;
            this.moduleInstances = moduleInstances;
            this.UsedLayersInstance = usedLayersInstance;
            //   this.moduleActivator = new Activator();
        }

        public ApplicationInstance ApplicationInstance { get; private set; }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.Model.MetaInfo;
            }
        }

        public LayerModel Model { get; private set; }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
        }

        public UsedLayersInstance UsedLayersInstance { get; private set; }

        public Module FindModule(string moduleName)
        {
            ModuleInstance moduleInstance = this.moduleInstances
                    .Where(l => l.Model.Name == moduleName)
                    .FirstOrDefault();

            return moduleInstance;
        }

        public bool VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            // Visit modules in this layer
            foreach (ModuleInstance moduleInstance in moduleInstances)
            {
                if (!visitor.VisitModule(moduleInstance, moduleInstance.Model, visibility))
                {
                    return false;
                }
            }

            if (visibility == Visibility.Layer)
            {
                // Visit modules in this layer
                if (!VisitModules(visitor, Visibility.Application))
                {
                    return false;
                }

                // Visit modules in used layers
                return UsedLayersInstance.VisitModules(visitor);
            }

            return true;
        }
    }
}