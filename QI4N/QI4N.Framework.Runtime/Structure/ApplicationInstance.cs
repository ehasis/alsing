namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationInstance : Application
    {
        private readonly List<LayerInstance> layerInstances;

        public ApplicationInstance(ApplicationModel applicationModel, List<LayerInstance> layers)
        {
            this.Model = applicationModel;
            this.layerInstances = layers;
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.Model.MetaInfo;
            }
        }

        public ApplicationMode Mode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ApplicationModel Model { get; private set; }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
        }

        public Layer FindLayer(string layerName)
        {
            LayerInstance layerInstance = this.layerInstances
                    .Where(l => l.Model.Name == layerName)
                    .FirstOrDefault();

            return layerInstance;
        }

        public Module FindModule(string layerName, string moduleName)
        {
            Layer layer = this.FindLayer(layerName);

            if (layer == null)
            {
                return null;
            }

            Module module = layer.FindModule(moduleName);

            return module;
        }
    }
}