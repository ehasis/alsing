namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ModuleModel
    {
        private readonly CompositesModel compositesModel;

        private readonly EntitiesModel entitiesModel;

        private readonly ImportedServicesModel importedServicesModel;

        private readonly MetaInfo metaInfo;

        private readonly string name;

        private readonly ObjectsModel objectsModel;

        private readonly ServicesModel servicesModel;

        private readonly ValuesModel valuesModel;

        private LayerModel layerModel;

        public ModuleModel(string name,
                           MetaInfo metaInfo, CompositesModel compositesModel,
                           EntitiesModel entitiesModel,
                           ObjectsModel objectsModel,
                           ValuesModel valuesModel,
                           ServicesModel servicesModel,
                           ImportedServicesModel importedServicesModel)
        {
            this.name = name;
            this.metaInfo = metaInfo;
            this.compositesModel = compositesModel;
            this.entitiesModel = entitiesModel;
            this.objectsModel = objectsModel;
            this.valuesModel = valuesModel;
            this.servicesModel = servicesModel;
            this.importedServicesModel = importedServicesModel;
        }


        public CompositesModel Composites
        {
            get
            {
                return this.compositesModel;
            }
        }

        public EntitiesModel entities
        {
            get
            {
                return this.entitiesModel;
            }
        }

        public ImportedServicesModel ImportedServicesModel
        {
            get
            {
                return this.importedServicesModel;
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.metaInfo;
            }
        }

        public String Name
        {
            get
            {
                return this.name;
            }
        }

        public ObjectsModel Objects
        {
            get
            {
                return this.objectsModel;
            }
        }

        public ServicesModel Services
        {
            get
            {
                return this.servicesModel;
            }
        }

        public ValuesModel Values
        {
            get
            {
                return this.valuesModel;
            }
        }

        public ModuleInstance NewInstance(LayerInstance layerInstance)
        {
            return new ModuleInstance(this, layerInstance, this.compositesModel, this.entitiesModel, this.objectsModel, this.valuesModel, this.servicesModel, this.importedServicesModel);
        }

        public void VisitModel(ModelVisitor modelVisitor)
        {
            modelVisitor.Visit(this);

            this.compositesModel.VisitModel(modelVisitor);
            this.entitiesModel.VisitModel(modelVisitor);
            this.servicesModel.VisitModel(modelVisitor);
            this.importedServicesModel.VisitModel(modelVisitor);
            this.objectsModel.VisitModel(modelVisitor);
            this.valuesModel.VisitModel(modelVisitor);
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

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public ImportedServicesInstance NewInstance(ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }
}