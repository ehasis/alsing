namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationInstance
    {
        private ApplicationModel applicationModel;

        private readonly List<LayerInstance> layerInstances;


        public ApplicationInstance(ApplicationModel applicationModel, List<LayerInstance> layers)
        {
            this.applicationModel = applicationModel;
            this.layerInstances = layers;
        }

        public LayerInstance FindLayer( string layerName )
        {
            var layerInstance = layerInstances
                    .Where(l => l.Model.Name == layerName)
                    .FirstOrDefault();


            return layerInstance;
        }

        public ModuleInstance FindModule(string layerName, string moduleName)
        {
            var layer = FindLayer(layerName);
            var module = layer.FindModule(moduleName);

            return module;
        }
    }
}