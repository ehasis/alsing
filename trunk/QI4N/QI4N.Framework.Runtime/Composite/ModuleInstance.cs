namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, CompositeFinder> compositeFinders;

        private readonly CompositesInstance composites;

        private readonly EntitiesInstance entities;

        private readonly IDictionary<Type, EntityFinder> entityFinders;

        private readonly ImportedServicesModel importedServices;

        private readonly LayerInstance layerInstance;

        private readonly ModuleModel moduleModel;

        private readonly IDictionary<Type, ObjectFinder> objectFinders;

        private readonly ObjectsInstance objects;

        private readonly ServicesModel services;

        private readonly IDictionary<Type, ValueFinder> valueFinders;

        private readonly ValuesInstance values;

        private TransientBuilderFactory compositeBuilderFactory;

        private ObjectBuilderFactory objectBuilderFactory;

        private ServiceFinderInstance serviceFinder;

        private UnitOfWorkFactoryInstance unitOfWorkFactory;

        private ValueBuilderFactory valueBuilderFactory;


        public ModuleInstance(ModuleModel moduleModel, LayerInstance layerInstance, CompositesModel compositesModel,
                              EntitiesModel entitiesModel, ObjectsModel objectsModel, ValuesModel valuesModel,
                              ServicesModel servicesModel, ImportedServicesModel importedServicesModel)
        {
            this.moduleModel = moduleModel;
            this.layerInstance = layerInstance;
            this.composites = new CompositesInstance(compositesModel, this);
            this.entities = new EntitiesInstance(entitiesModel, this);
            this.objects = new ObjectsInstance(objectsModel, this);
            this.values = new ValuesInstance(valuesModel, this);
            this.services = servicesModel.NewInstance(this);
            this.importedServices = importedServicesModel.NewInstance(this);

            this.compositeBuilderFactory = new TransientBuilderFactoryInstance(this);
            this.objectBuilderFactory = new ObjectBuilderFactoryInstance();
            this.valueBuilderFactory = new ValueBuilderFactoryInstance();
            this.unitOfWorkFactory = new UnitOfWorkFactoryInstance();
            this.serviceFinder = new ServiceFinderInstance();

            this.entityFinders = new Dictionary<Type, EntityFinder>();
            this.compositeFinders = new Dictionary<Type, CompositeFinder>();
            this.objectFinders = new Dictionary<Type, ObjectFinder>();
            this.valueFinders = new Dictionary<Type, ValueFinder>();
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

    public class ObjectBuilderFactoryInstance : ObjectBuilderFactory
    {
        public ObjectBuilder<T> NewObjectBuilder<T>()
        {
            throw new NotImplementedException();
        }
    }

    public class ValueBuilderFactoryInstance : ValueBuilderFactory
    {
    }

    public class UnitOfWorkFactoryInstance
    {
    }

    public class ServiceFinderInstance
    {
    }

    public class ValueFinder
    {
    }

    public class ObjectFinder
    {
    }

    public class ValuesInstance
    {
        public ValuesInstance(ValuesModel model, ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjectsInstance
    {
        public ObjectsInstance(ObjectsModel model, ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }

    public class EntitiesInstance
    {
        public EntitiesInstance(EntitiesModel model, ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }

    public class CompositesInstance
    {
        public CompositesInstance(CompositesModel model, ModuleInstance instance)
        {
            throw new NotImplementedException();
        }
    }

    public class LayerInstance
    {
        public void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }

    public interface Module
    {
    }
}