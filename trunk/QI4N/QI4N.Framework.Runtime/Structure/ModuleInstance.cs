namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, EntityFinder> entityFinders;

        private readonly IDictionary<Type, TransientFinder> transientFinders;

        private readonly IDictionary<Type, ValueFinder> valueFinders;


        public ModuleInstance(ModuleModel moduleModel, LayerInstance layerInstance, TransientsModel transientsModel,
                              EntitiesModel entitiesModel, ObjectsModel objectsModel, ValuesModel valuesModel,
                              ServicesModel servicesModel, ImportedServicesModel importedServicesModel)
        {
            this.Model = moduleModel;
            this.LayerInstance = layerInstance;
            this.Transients = new TransientsInstance(transientsModel, this);
            this.Entities = new EntitiesInstance(entitiesModel, this);
            this.Objects = new ObjectsInstance(objectsModel, this);
            this.Values = new ValuesInstance(valuesModel, this);
            this.Services = servicesModel.NewInstance(this);
            this.ImportedServices = importedServicesModel.NewInstance(this);

            this.TransientBuilderFactory = new TransientBuilderFactoryInstance(this);
            this.ObjectBuilderFactory = new ObjectBuilderFactoryInstance();
            this.ValueBuilderFactory = new ValueBuilderFactoryInstance(this);
            this.UnitOfWorkFactory = new UnitOfWorkFactoryInstance();
            this.ServiceFinder = new ServiceFinderInstance(this);

            this.entityFinders = new Dictionary<Type, EntityFinder>();
            this.transientFinders = new Dictionary<Type, TransientFinder>();
            this.ObjectFinders = new Dictionary<Type, ObjectFinder>();
            this.valueFinders = new Dictionary<Type, ValueFinder>();
        }

        public EntitiesInstance Entities { get; private set; }

        public ImportedServicesInstance ImportedServices { get; private set; }

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

        public IDictionary<Type, ObjectFinder> ObjectFinders { get; private set; }

        public ObjectsInstance Objects { get; private set; }

        public ServiceFinder ServiceFinder { get; private set; }

        public ServicesInstance Services { get; private set; }

        public TransientBuilderFactory TransientBuilderFactory { get; private set; }

        public TransientsInstance Transients { get; private set; }

        public UnitOfWorkFactory UnitOfWorkFactory { get; private set; }

        public ValueBuilderFactory ValueBuilderFactory { get; private set; }

        public ValuesInstance Values { get; private set; }


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

        public ServiceReference GetServiceFor(Type type, Visibility visibility)
        {
            ServiceReference service = 
                this.Services.GetServiceFor(type, visibility) 
                ?? this.ImportedServices.GetServiceFor(type, visibility);

            return service;
        }

        public void GetServicesFor(Type type, Visibility visibility, List<ServiceReference> serviceReferences)
        {
            Services.GetServicesFor(type, visibility, serviceReferences);
            ImportedServices.GetServicesFor(type, visibility, serviceReferences);
        }
    }



    


    
    internal class ServiceReferenceFinder : ModuleVisitor
    {
        #region ModuleVisitor Members

        public ServiceReference Service { get; set; }

        public Type Type { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            Service = moduleInstance.GetServiceFor(Type, visibility);

            return Service == null;
        }

        #endregion
    }

    public class ServiceReferencesFinder : ModuleVisitor
    {
        public List<ServiceReference> Services;

        public Type Type { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            moduleInstance.GetServicesFor(Type, visibility, Services);

            return true;
        }
    }

    public class ObjectFinder
    {
    }
}