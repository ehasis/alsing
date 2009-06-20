namespace QI4N.Framework.Runtime
{
    using System;

    using Bootstrap;
    using System.Collections.Generic;

    public class LayerModel
    {
        private string name;

        private MetaInfo metaInfo;

        private List<LayerModel> usedLayers;

        private List<ModuleModel> moduleModels;

        private LayerModel(string name, MetaInfo metaInfo, List<LayerModel> usedLayers, List<ModuleModel> moduleModels)
        {
            this.name = name;
            this.metaInfo = metaInfo;
            this.usedLayers = usedLayers;
            this.moduleModels = moduleModels;
        }

        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }

        public static LayerModel NewModel(LayerAssembly layer)
        {
            var moduleModels = new List<ModuleModel>();

            foreach(ModuleAssembly module in layer.Modules)
            {
                var moduleModel = ModuleModel.NewModel(module);
                moduleModels.Add(moduleModel);
            }

            //TODO:fix usedLayerModel

            var usedLayersModel = new List<LayerModel>();

            var layerModel = new LayerModel(layer.Name,layer.MetaInfo,usedLayersModel, moduleModels);

            return layerModel;
        }
    }
}