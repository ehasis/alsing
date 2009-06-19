namespace QI4N.Framework.Runtime
{
    using System;

    public class ModuleModel
    {
        private LayerModel layerModel;
        private readonly CompositesModel compositesModel;
        private readonly EntitiesModel entitiesModel;
        private readonly ObjectsModel objectsModel;
        private readonly ValuesModel valuesModel;
        private readonly ServicesModel servicesModel;
        private readonly ImportedServicesModel importedServicesModel;

        private readonly string name;
        private readonly MetaInfo metaInfo;

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

        public String Name
        {
            get
            {
                return name;
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return metaInfo;
            }
        }

        public EntitiesModel entities
        {
            get
            {
                return entitiesModel;
            }
        }

        public ObjectsModel Objects
        {
            get
            {
                return objectsModel;
            }
        }

        public ValuesModel Values
        {
            get
            {
                return valuesModel;
            }
        }

        public ServicesModel Services
        {
            get
            {
                return servicesModel;
            }
        }

        public ImportedServicesModel ImportedServicesModel
        {
            get
            {
                return importedServicesModel;
            }
        }

        public void VisitModel(ModelVisitor modelVisitor)
        {
            modelVisitor.Visit(this);

            compositesModel.VisitModel(modelVisitor);
            entitiesModel.VisitModel(modelVisitor);
            servicesModel.VisitModel(modelVisitor);
            importedServicesModel.VisitModel(modelVisitor);
            objectsModel.VisitModel(modelVisitor);
            valuesModel.VisitModel(modelVisitor);
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(null, this, Visibility.Module))
            {
                return;
            }

            // Visit layer
            layerModel.VisitModules(visitor, Visibility.Layer);
        }

        // Context
        public ModuleInstance NewInstance(LayerInstance layerInstance)
        {
            return new ModuleInstance(this, layerInstance, compositesModel, entitiesModel, objectsModel, valuesModel, servicesModel, importedServicesModel);
        }
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
        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public ImportedServicesModel NewInstance(ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }










}