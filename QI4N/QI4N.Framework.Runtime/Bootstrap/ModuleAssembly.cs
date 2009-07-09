namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Bootstrap;

    //[DebuggerDisplay("Name {Name}")]
    public class ModuleAssemblyImpl : ModuleAssembly
    {
        private readonly IList<EntityDeclaration> entityDeclarations = new List<EntityDeclaration>();

        private readonly LayerAssembly layerAssembly;

        private readonly MetaInfo metaInfo;

        private readonly MetaInfoDeclaration metaInfoDeclaration = new MetaInfoDeclaration();

        private readonly string name;

        private readonly List<ServiceDeclaration> serviceDeclarations = new List<ServiceDeclaration>();

        private readonly List<TransientDeclaration> transientDeclarations = new List<TransientDeclaration>();

        private readonly List<ValueDeclaration> valueDeclarations = new List<ValueDeclaration>();

        public ModuleAssemblyImpl(LayerAssembly layerAssembly, string name, MetaInfo metaInfo)
        {
            this.layerAssembly = layerAssembly;
            this.name = name;
            this.metaInfo = metaInfo;
        }

        public IList<EntityDeclaration> EntityDeclarations
        {
            get
            {
                return this.entityDeclarations;
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.metaInfo;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public IList<ServiceDeclaration> ServiceDeclarations
        {
            get
            {
                return this.serviceDeclarations;
            }
        }

        public IList<TransientDeclaration> TransientDeclarations
        {
            get
            {
                return this.transientDeclarations;
            }
        }

        public IList<ValueDeclaration> ValueDeclarations
        {
            get
            {
                return this.valueDeclarations;
            }
        }

        public EntityDeclaration AddEntities()
        {
            var declaration = new EntityDeclarationImpl();
            this.EntityDeclarations.Add(declaration);
            return declaration;
        }

        public ServiceDeclaration AddServices()
        {
            var declaration = new ServiceDeclarationImpl(this);
            this.ServiceDeclarations.Add(declaration);
            return declaration;
        }

        public TransientDeclaration AddTransients()
        {
            var declaration = new TransientDeclarationImpl();
            this.transientDeclarations.Add(declaration);
            return declaration;
        }

        public ValueDeclaration AddValues()
        {
            var declaration = new ValueDeclarationImpl();
            this.ValueDeclarations.Add(declaration);
            return declaration;
        }

        public ModuleModel AssembleModule()
        {
            var compositeModels = new List<TransientModel>();
            var entityModels = new List<EntityModel>();
            var objectModels = new List<ObjectModel>();
            var valueModels = new List<ValueModel>();
            var serviceModels = new List<ServiceModel>();
            var importedServiceModels = new List<ImportedServiceModel>();

            if (this.name == null)
            {
                throw new Exception("Module must have name set");
            }

            var moduleModel = new ModuleModel(this.name,
                                              this.metaInfo, new TransientsModel(compositeModels),
                                              new EntitiesModel(entityModels),
                                              new ObjectsModel(objectModels),
                                              new ValuesModel(valueModels),
                                              new ServicesModel(serviceModels),
                                              new ImportedServicesModel(importedServiceModels));

            foreach (TransientDeclarationImpl transientDeclaration in this.transientDeclarations)
            {
                transientDeclaration.AddTransients(compositeModels, this.metaInfoDeclaration);
            }

            foreach (ValueDeclarationImpl valueDeclaration in this.valueDeclarations)
            {
                valueDeclaration.AddValues(valueModels, this.metaInfoDeclaration);
            }

            foreach (EntityDeclarationImpl entityDeclaration in this.entityDeclarations)
            {
                entityDeclaration.AddEntities(entityModels, this.metaInfoDeclaration);
            }

            foreach (ServiceDeclarationImpl serviceDeclaration in this.serviceDeclarations)
            {
                serviceDeclaration.AddServices(serviceModels, this.metaInfoDeclaration);
            }

            return moduleModel;

            //    for( ObjectDeclarationImpl objectDeclaration : objectDeclarations )
            //{
            //    objectDeclaration.addObjects( objectModels );
            //}

            //for( ImportedServiceDeclarationImpl importedServiceDeclaration : importedServiceDeclarations )
            //{
            //    importedServiceDeclaration.addServices( importedServiceModels );
            //}

            //// Check for duplicate service identities
            //Set<String> identities = new HashSet<String>();
            //for( ServiceModel serviceModel : serviceModels )
            //{
            //    String identity = serviceModel.identity();
            //    if( identities.contains( identity ) )
            //    {
            //        throw new DuplicateServiceIdentityException(
            //            "Duplicated service identity: " + identity + " in module " + moduleModel.name()
            //        );
            //    }
            //    identities.add( identity );
            //}
            //for( ImportedServiceModel serviceModel : importedServiceModels )
            //{
            //    String identity = serviceModel.identity();
            //    if( identities.contains( identity ) )
            //    {
            //        throw new DuplicateServiceIdentityException(
            //            "Duplicated service identity: " + identity + " in module " + moduleModel.name()
            //        );
            //    }
            //    identities.add( identity );
            //}

            //for( ImportedServiceModel importedServiceModel : importedServiceModels )
            //{
            //    boolean found = false;
            //    for( ObjectModel objectModel : objectModels )
            //    {
            //        if( objectModel.type().equals( importedServiceModel.serviceImporter() ) )
            //        {
            //            found = true;
            //            break;
            //        }
            //    }
            //    if( !found )
            //    {
            //        Class<? extends ServiceImporter> serviceFactoryType = importedServiceModel.serviceImporter();
            //        ObjectModel objectModel = new ObjectModel( serviceFactoryType, Visibility.module, new MetaInfo() );
            //        objectModels.add( objectModel );
            //    }
            //}
        }

        public void Visit(AssemblyVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }


    public class ObjectModel
    {
    }
}