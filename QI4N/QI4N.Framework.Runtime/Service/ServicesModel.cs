namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ServicesModel
    {
        private List<ServiceModel> serviceModels;

        public ServicesModel(List<ServiceModel> serviceModels)
        {
            this.serviceModels = serviceModels;
        }



        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public ServicesInstance NewInstance(ModuleInstance moduleInstance)
        {
            throw new NotImplementedException();
        }
    }
}