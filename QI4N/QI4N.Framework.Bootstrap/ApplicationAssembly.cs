namespace QI4N.Framework.Bootstrap
{
    using System.Collections.Generic;

    public interface ApplicationAssembly
    {
        LayerAssembly NewLayerAssembly();

        LayerAssembly NewLayerAssembly(string name);
    }

    public class ApplicationAssemblyImpl : ApplicationAssembly
    {
        private readonly IList<LayerAssembly> layers = new List<LayerAssembly>();

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