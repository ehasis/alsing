namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ServiceDeclarationImpl : AbstractCompositeDeclarationImpl<ServiceDeclaration, ServiceComposite>, ServiceDeclaration
    {
        private readonly ModuleAssemblyImpl moduleAssembly;

        private string identity;

        private bool instantiateOnStartup;

        public ServiceDeclarationImpl(ModuleAssemblyImpl moduleAssembly)
        {
            this.moduleAssembly = moduleAssembly;
        }

        public void AddServices(List<ServiceModel> serviceModels, PropertyDeclarations propertyDecs)
        {
            foreach (Type serviceType in this.CompositeTypes)
            {
                string id = this.identity ?? GenerateId(serviceModels, serviceType);

                ServiceModel serviceModel = ServiceModel.NewModel(serviceType,
                                                                  this.visibility,
                                                                  this.metaInfo,
                                                                  this.concerns,
                                                                  this.sideEffects,
                                                                  this.mixins,
                                                                  this.moduleAssembly.Name,
                                                                  id,
                                                                  this.instantiateOnStartup);
                serviceModels.Add(serviceModel);
            }
        }

        public ServiceDeclaration IdentifiedBy(String identity)
        {
            this.identity = identity;
            return this;
        }

        public ServiceDeclaration InstantiateOnStartup()
        {
            this.instantiateOnStartup = true;
            return this;
        }

        private static string GenerateId(List<ServiceModel> serviceModels, Type serviceType)
        {
            // Find identity that is not yet used
            int idx = 0;
            string id = serviceType.Name;
            bool invalid;
            do
            {
                invalid = false;
                foreach (ServiceModel serviceModel in serviceModels)
                {
                    if (serviceModel.Identity.Equals(id))
                    {
                        idx++;
                        id = serviceType.Name + "_" + idx;
                        invalid = true;
                        break;
                    }
                }
            } while (invalid);
            return id;
        }
    }
}