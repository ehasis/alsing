namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class LayerModel
    {
        private readonly List<ModuleModel> modules;

        private readonly List<LayerModel> usedLayers;

        private LayerModel(string name, MetaInfo metaInfo, List<LayerModel> usedLayers, List<ModuleModel> modules)
        {
            this.Name = name;
            this.MetaInfo = metaInfo;
            this.usedLayers = usedLayers;
            this.modules = modules;
        }

        public MetaInfo MetaInfo { get; private set; }

        public string Name { get; private set; }

        public static LayerModel NewModel(LayerAssembly layer)
        {
            var moduleModels = new List<ModuleModel>();

            foreach (ModuleAssemblyImpl module in layer.Modules)
            {
                ModuleModel moduleModel = module.AssembleModule();
                moduleModels.Add(moduleModel);
            }

            //TODO:fix usedLayerModel

            var usedLayersModel = new List<LayerModel>();

            var layerModel = new LayerModel(layer.Name, layer.MetaInfo, usedLayersModel, moduleModels);

            return layerModel;
        }

        public LayerInstance NewInstance(ApplicationInstance applicationInstance, UsedLayersInstance usedLayerInstance)
        {
            var moduleInstances = new List<ModuleInstance>();
            var layerInstance = new LayerInstance(this, applicationInstance, moduleInstances, usedLayerInstance);

            foreach (ModuleModel module in this.modules)
            {
                ModuleInstance moduleInstance = module.NewInstance(layerInstance);
                moduleInstances.Add(moduleInstance);
            }

            return layerInstance;
        }

        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }

    public class UsedLayersInstance
    {
    }
}