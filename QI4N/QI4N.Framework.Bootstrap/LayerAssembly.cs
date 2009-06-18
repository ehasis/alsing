namespace QI4N.Framework.Bootstrap
{
    using Runtime;

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

    public class AssemblyVisitor
    {
    }

    public interface ApplicationAssembly
    {
        LayerAssembly NewLayerAssembly();
    }

    public interface ModuleAssembly
    {
        EntitiesDeclaration AddEntity<T>() where T : EntityComposite;

        ServiceDeclaration AddService<T>() where T : ServiceComposite;

        ValueDeclaration AddValue<T>() where T : ValueComposite;

        TransientDeclaration AddTransient<T>() where T : TransientComposite;
    }

    public class TransientDeclaration
    {
    }

    public class ValueDeclaration
    {
    }

    public class ServiceDeclaration
    {
        public void VisibleIn(Visibility layer)
        {

        }
    }

    public class EntitiesDeclaration
    {
    }
}