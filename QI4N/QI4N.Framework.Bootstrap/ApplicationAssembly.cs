namespace QI4N.Framework.Bootstrap
{
    public interface ApplicationAssembly
    {
        LayerAssembly NewLayerAssembly();

        LayerAssembly NewLayerAssembly(string name);
    }
}