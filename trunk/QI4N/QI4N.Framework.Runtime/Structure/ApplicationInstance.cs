namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationInstance : Application
    {
        private readonly List<LayerInstance> layerInstances;

        private readonly ApplicationModel model;


        public ApplicationInstance(ApplicationModel applicationModel, List<LayerInstance> layers)
        {
            this.model = applicationModel;
            this.layerInstances = layers;
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

        #region Application Members

        public string Name
        {
            get 
            {
                return model.Name; 
            }
        }

        public ApplicationMode Mode
        {
            get { throw new System.NotImplementedException(); }
        }

        public MetaInfo MetaInfo
        {
            get 
            {
                return model.MetaInfo;
            }
        }

        #endregion
    }
}