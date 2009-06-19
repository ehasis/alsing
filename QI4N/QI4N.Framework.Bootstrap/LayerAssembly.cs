namespace QI4N.Framework.Bootstrap
{
    public interface LayerAssembly
    {
        ModuleAssembly NewModuleAssembly(string name);

        ApplicationAssembly ApplicationAssembly { get; }

        string Name { get; }

        LayerAssembly SetName(string name);

        LayerAssembly SetMetaInfo(object info);

        LayerAssembly Uses(params LayerAssembly[] layerAssembly);

        void Visit(AssemblyVisitor visitor);

        ModuleAssembly NewModuleAssembly();
    }
}