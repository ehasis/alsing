namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class LayerInstance
    {
        private ApplicationInstance applicationInstance;

        private LayerModel model;

        private IList<ModuleInstance> moduleInstances;

        private UsedLayersInstance usedLayersInstance;

        public LayerInstance(LayerModel model, ApplicationInstance applicationInstance, IList<ModuleInstance> moduleInstances, UsedLayersInstance usedLayersInstance)
        {
            this.model = model;
            this.applicationInstance = applicationInstance;
            this.moduleInstances = moduleInstances;
            this.usedLayersInstance = usedLayersInstance;
            //   this.moduleActivator = new Activator();
        }

        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }
}