namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;

    using Bootstrap;

    public class ApplicationAssemblyImpl : ApplicationAssembly
    {
        private readonly IList<LayerAssembly> layers = new List<LayerAssembly>();

        public IList<LayerAssembly> Layers
        {
            get
            {
                return this.layers;
            }
        }

        public MetaInfo MetaInfo { get; set; }

        public string Name { get; set; }

        public LayerAssembly NewLayerAssembly()
        {
            return this.NewLayerAssembly("");
        }

        public LayerAssembly NewLayerAssembly(string name)
        {
            var layer = new LayerAssemblyImpl(this, name);
            this.layers.Add(layer);
            return layer;
        }
    }
}