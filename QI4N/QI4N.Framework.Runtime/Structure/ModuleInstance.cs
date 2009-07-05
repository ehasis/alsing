namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, TransientFinder> transientFinders;

        private readonly EntitiesInstance entities;

        private readonly IDictionary<Type, EntityFinder> entityFinders;

        private readonly ImportedServicesInstance importedServices;

        private readonly IDictionary<Type, ObjectFinder> objectFinders;

        private readonly ObjectsInstance objects;

        private readonly ServiceFinderInstance serviceFinder;

        private readonly ServicesInstance services;

        private readonly TransientsInstance transients;

        private readonly IDictionary<Type, ValueFinder> valueFinders;

        private readonly ValuesInstance values;


        public ModuleInstance(ModuleModel moduleModel, LayerInstance layerInstance, TransientsModel transientsModel,
                              EntitiesModel entitiesModel, ObjectsModel objectsModel, ValuesModel valuesModel,
                              ServicesModel servicesModel, ImportedServicesModel importedServicesModel)
        {
            this.Model = moduleModel;
            this.LayerInstance = layerInstance;
            this.transients = new TransientsInstance(transientsModel, this);
            this.entities = new EntitiesInstance(entitiesModel, this);
            this.objects = new ObjectsInstance(objectsModel, this);
            this.values = new ValuesInstance(valuesModel, this);
            this.services = servicesModel.NewInstance(this);
            this.importedServices = importedServicesModel.NewInstance(this);

            this.TransientBuilderFactory = new TransientBuilderFactoryInstance(this);
            this.ObjectBuilderFactory = new ObjectBuilderFactoryInstance();
            this.ValueBuilderFactory = new ValueBuilderFactoryInstance(this);
            this.UnitOfWorkFactory = new UnitOfWorkFactoryInstance();
            this.serviceFinder = new ServiceFinderInstance();

            this.entityFinders = new Dictionary<Type, EntityFinder>();
            this.transientFinders = new Dictionary<Type, TransientFinder>();
            this.objectFinders = new Dictionary<Type, ObjectFinder>();
            this.valueFinders = new Dictionary<Type, ValueFinder>();
        }

        public LayerInstance LayerInstance { get; private set; }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.Model.MetaInfo;
            }
        }

        public ModuleModel Model { get; private set; }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
        }


        public ObjectBuilderFactory ObjectBuilderFactory { get; private set; }

        public ServiceFinder ServiceFinder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TransientBuilderFactory TransientBuilderFactory { get; private set; }

        public UnitOfWorkFactory UnitOfWorkFactory { get; private set; }

        public ValueBuilderFactory ValueBuilderFactory { get; private set; }


        public TransientFinder FindCompositeModel(Type mixinType)
        {
            TransientFinder finder;
            if (!this.transientFinders.TryGetValue(mixinType, out finder))
            {
                finder = new TransientFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.transientFinders.Add(mixinType, finder);
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

        public ValueFinder FindValueModel(Type mixinType)
        {
            ValueFinder finder;
            if (!this.valueFinders.TryGetValue(mixinType, out finder))
            {
                finder = new ValueFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.valueFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(this, this.Model, Visibility.Module))
            {
                return;
            }

            // Visit layer
            this.LayerInstance.VisitModules(visitor, Visibility.Layer);
        }
    }

    public class ImportedServicesInstance
    {
    }

    public class ServicesInstance
    {
        public ServicesInstance(ServicesModel model, List<ServiceReference> references)
        {
        }
    }


    public class UnitOfWorkFactoryInstance : UnitOfWorkFactory
    {
        public UnitOfWork CurrentUnitOfWork
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class ServiceFinderInstance
    {
    }

    public class ObjectFinder
    {
    }
}