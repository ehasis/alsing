namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class LayerModel
    {
        private MetaInfo metaInfo;

        private List<ModuleModel> moduleModels;

        private string name;

        private List<LayerModel> usedLayers;

        private LayerModel(string name, MetaInfo metaInfo, List<LayerModel> usedLayers, List<ModuleModel> moduleModels)
        {
            this.name = name;
            this.metaInfo = metaInfo;
            this.usedLayers = usedLayers;
            this.moduleModels = moduleModels;
        }

        public static LayerModel NewModel(LayerAssembly layer)
        {
            var moduleModels = new List<ModuleModel>();

            foreach (ModuleAssembly module in layer.Modules)
            {
                ModuleModel moduleModel = ModuleModel.NewModel(module);
                moduleModels.Add(moduleModel);
            }

            //TODO:fix usedLayerModel

            var usedLayersModel = new List<LayerModel>();

            var layerModel = new LayerModel(layer.Name, layer.MetaInfo, usedLayersModel, moduleModels);

            return layerModel;
        }

        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }

        public LayerInstance NewInstance(LayerModel layer)
        {
            var layerInstance = new LayerInstance();

            return layerInstance;
        }
    }
}