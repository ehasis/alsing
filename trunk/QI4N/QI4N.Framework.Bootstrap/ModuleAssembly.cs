namespace QI4N.Framework.Bootstrap
{
    using System.Collections.Generic;

    public interface ModuleAssembly
    {
        EntityDeclaration AddEntities();

        ServiceDeclaration AddServices();

        ValueDeclaration AddValues();

        TransientDeclaration AddTransients();

        string Name { get; }

        MetaInfo MetaInfo { get; }

        IList<TransientDeclaration> TransientDeclarations { get; }

        IList<ServiceDeclaration> ServiceDeclarations { get; }

        IList<EntityDeclaration> EntityDeclarations { get; }

        IList<ValueDeclaration> ValueDeclarations { get; }
    }
}