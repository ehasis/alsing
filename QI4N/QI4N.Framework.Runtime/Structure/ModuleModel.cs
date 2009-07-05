namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ModuleModel
    {
        private LayerModel layerModel;

        public ModuleModel(string name,
                           MetaInfo metaInfo, 
                           TransientsModel transientsModel,
                           EntitiesModel entitiesModel,
                           ObjectsModel objectsModel,
                           ValuesModel valuesModel,
                           ServicesModel servicesModel,
                           ImportedServicesModel importedServicesModel)
        {
            this.Name = name;
            this.MetaInfo = metaInfo;
            this.Transients = transientsModel;
            this.entities = entitiesModel;
            this.Objects = objectsModel;
            this.Values = valuesModel;
            this.Services = servicesModel;
            this.ImportedServicesModel = importedServicesModel;
        }


        public EntitiesModel entities { get; private set; }

        public ImportedServicesModel ImportedServicesModel { get; private set; }

        public MetaInfo MetaInfo { get; private set; }

        public string Name { get; private set; }

        public ObjectsModel Objects { get; private set; }

        public ServicesModel Services { get; private set; }

        public TransientsModel Transients { get; private set; }

        public ValuesModel Values { get; private set; }

        public ModuleInstance NewInstance(LayerInstance layerInstance)
        {
            return new ModuleInstance(this, layerInstance, this.Transients, this.entities, this.Objects, this.Values, this.Services, this.ImportedServicesModel);
        }

        public void VisitModel(ModelVisitor modelVisitor)
        {
            modelVisitor.Visit(this);

            this.Transients.VisitModel(modelVisitor);
            this.entities.VisitModel(modelVisitor);
            this.Services.VisitModel(modelVisitor);
            this.ImportedServicesModel.VisitModel(modelVisitor);
            this.Objects.VisitModel(modelVisitor);
            this.Values.VisitModel(modelVisitor);
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(null, this, Visibility.Module))
            {
                return;
            }

            // Visit layer
            this.layerModel.VisitModules(visitor, Visibility.Layer);
        }

        // Context
    }

    public class ModelVisitor
    {
        public void Visit(ModuleModel model)
        {
            throw new NotImplementedException();
        }
    }

    public class ImportedServicesModel
    {
        private List<ImportedServiceModel> importedServiceModels;

        public ImportedServicesModel(List<ImportedServiceModel> importedServiceModels)
        {
            this.importedServiceModels = importedServiceModels;
        }

        public ImportedServicesInstance NewInstance(ModuleInstance instance)
        {
            return null;
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}