namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Linq;

    public class LayerInstance : Layer
    {
        private readonly IList<ModuleInstance> moduleInstances;

        private UsedLayersInstance usedLayersInstance;

        public LayerInstance(LayerModel model, ApplicationInstance applicationInstance, IList<ModuleInstance> moduleInstances, UsedLayersInstance usedLayersInstance)
        {
            this.Model = model;
            this.Application = applicationInstance;
            this.moduleInstances = moduleInstances;
            this.usedLayersInstance = usedLayersInstance;
            //   this.moduleActivator = new Activator();
        }

        public Application Application{ get;private set; }

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

        public Module FindModule(string moduleName)
        {
            ModuleInstance moduleInstance = this.moduleInstances
                    .Where(l => l.Model.Name == moduleName)
                    .FirstOrDefault();

            return moduleInstance;
        }

        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
        }
    }
}