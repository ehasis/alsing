namespace QI4N.Framework.Bootstrap
{
    using System.Collections.Generic;

    public interface LayerAssembly
    {
        ModuleAssembly NewModuleAssembly(string name);

        ApplicationAssembly ApplicationAssembly { get; }

        string Name { get; }

        IList<ModuleAssembly> Modules { get; }

        MetaInfo MetaInfo { get; }

        LayerAssembly SetName(string name);

        LayerAssembly SetMetaInfo(object info);

        LayerAssembly Uses(params LayerAssembly[] layerAssembly);

        void Visit(AssemblyVisitor visitor);

        ModuleAssembly NewModuleAssembly();
    }
}