namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;

    using Bootstrap;

    public class ApplicationModel
    {
        private IList<LayerModel> layers;

        private MetaInfo metaInfo;

        private string name;

        private ApplicationModel(string name, MetaInfo metaInfo, IList<LayerModel> layers)
        {
            this.name = name;
            this.metaInfo = metaInfo;
            this.layers = layers;
        }

        public static ApplicationModel NewModel(ApplicationAssembly application)
        {
            var layerModels = new List<LayerModel>();

            foreach (LayerAssembly layer in application.Layers)
            {
                LayerModel layerModel = LayerModel.NewModel(layer);

                layerModels.Add(layerModel);
            }

            var app = new ApplicationModel(application.Name, application.MetaInfo, layerModels);

            return app;
        }

        public ApplicationInstance NewInstance()
        {
            var layerInstances = new List<LayerInstance>();

            foreach(var layer in layers)
            {
                var layerInstance = layer.NewInstance(layer);
                layerInstances.Add(layerInstance);
            }

            return new ApplicationInstance(this, layerInstances);
        }
    }
}