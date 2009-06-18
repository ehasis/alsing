namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, CompositeFinder> compositeFinders;

        private readonly IDictionary<Type, EntityFinder> entityFinders;

        private LayerInstance layerInstance;

        private ModuleModel moduleModel;

        public ModuleInstance(ModuleModel moduleModel, LayerInstance layerInstance, CompositesModel compositesModel,
                           EntitiesModel entitiesModel, ObjectsModel objectsModel, ValuesModel valuesModel,
                           ServicesModel servicesModel, ImportedServicesModel importedServicesModel)
        {
            this.moduleModel = moduleModel;
            this.layerInstance = layerInstance;
            composites = new CompositesInstance(compositesModel, this);
            entities = new EntitiesInstance(entitiesModel, this);
            objects = new ObjectsInstance(objectsModel, this);
            values = new ValuesInstance(valuesModel, this);
            services = servicesModel.newInstance(this);
            importedServices = importedServicesModel.newInstance(this);

            compositeBuilderFactory = new CompositeBuilderFactoryInstance();
            objectBuilderFactory = new ObjectBuilderFactoryInstance();
            valueBuilderFactory = new ValueBuilderFactoryInstance();
            unitOfWorkFactory = new UnitOfWorkFactoryInstance();
            serviceFinder = new ServiceFinderInstance();

            entityFinders = new Dictionary<Type, EntityFinder>();
            compositeFinders = new Dictionary<Type, CompositeFinder>();
            objectFinders = new Dictionary<Type, ObjectFinder>();
            valueFinders = new Dictionary<Type, ValueFinder>();
        }


        public CompositeFinder FindCompositeModel(Type mixinType)
        {
            CompositeFinder finder;
            if (!this.compositeFinders.TryGetValue(mixinType, out finder))
            {
                finder = new CompositeFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.compositeFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public EntityFinder FindEntityModel(Type mixinType)
        {
            EntityFinder finder;
            if (!this.entityFinders.TryGetValue(mixinType, out finder))
            {
                finder = new EntityFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.entityFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public StructureContext GetStructureContext()
        {
            throw new Exception();
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(this, this.moduleModel, Visibility.Module))
            {
                return;
            }

            // Visit layer
            //   layerInstance.VisitModules(visitor, Visibility.Layer);
        }
    }

    public class LayerInstance
    {
        internal void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }

    public interface Module
    {
    }
}