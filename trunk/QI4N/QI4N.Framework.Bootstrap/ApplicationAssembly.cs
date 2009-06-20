namespace QI4N.Framework.Bootstrap
{
    using System.Collections.Generic;

    public interface ApplicationAssembly
    {
        LayerAssembly NewLayerAssembly();

        LayerAssembly NewLayerAssembly(string name);

        IList<LayerAssembly> Layers { get; }

        string Name { get; }

        MetaInfo MetaInfo { get; }
    }
}