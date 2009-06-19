namespace QI4N.Framework.Bootstrap
{
    public interface ModuleAssembly
    {
        EntityDeclaration AddEntities();

        ServiceDeclaration AddServices();

        ValueDeclaration AddValues();

        TransientDeclaration AddTransients();

        string Name { get; }
    }
}