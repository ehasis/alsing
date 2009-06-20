namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationInstance
    {
        private readonly List<LayerInstance> layerInstances;

        private ApplicationModel applicationModel;


        public ApplicationInstance(ApplicationModel applicationModel, List<LayerInstance> layers)
        {
            this.applicationModel = applicationModel;
            this.layerInstances = layers;
        }

        public LayerInstance FindLayer(string layerName)
        {
            LayerInstance layerInstance = this.layerInstances
                    .Where(l => l.Model.Name == layerName)
                    .FirstOrDefault();

            return layerInstance;
        }

        public ModuleInstance FindModule(string layerName, string moduleName)
        {
            LayerInstance layer = this.FindLayer(layerName);

            if (layer == null)
            {
                return null;
            }

            ModuleInstance module = layer.FindModule(moduleName);

            return module;
        }
    }
}