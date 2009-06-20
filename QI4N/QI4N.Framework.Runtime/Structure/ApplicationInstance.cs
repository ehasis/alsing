namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;

    public class ApplicationInstance
    {
        private ApplicationModel applicationModel;

        private List<LayerInstance> layers;


        public ApplicationInstance(ApplicationModel applicationModel, List<LayerInstance> layers)
        {
            this.applicationModel = applicationModel;
            this.layers = layers;
        }
    }
}