namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ServicesModel
    {
        private List<ServiceModel> serviceModels;

        public ServicesModel(List<ServiceModel> serviceModels)
        {
            this.serviceModels = serviceModels;
        }


        public ServicesInstance NewInstance(ModuleInstance moduleInstance)
        {
            return null;
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}