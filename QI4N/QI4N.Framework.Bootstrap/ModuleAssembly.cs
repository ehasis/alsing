namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;

    public interface ModuleAssembly
    {
        EntityDeclaration AddEntities(params Type[] compositeTypes);

        ServiceDeclaration AddServices(params Type[] compositeTypes);

        ValueDeclaration AddValues(params Type[] compositeTypes);

        TransientDeclaration AddTransients(params Type[] compositeTypes);
    }

    public class ModuleAssemblyImpl : ModuleAssembly
    {
        private readonly IList<EntityDeclaration> entityDeclarations = new List<EntityDeclaration>();

        private readonly IList<ServiceDeclaration> serviceDeclarations = new List<ServiceDeclaration>();

        private readonly IList<TransientDeclaration> transientDeclarations = new List<TransientDeclaration>();

        private readonly IList<ValueDeclaration> valueDeclarations = new List<ValueDeclaration>();

        private LayerAssembly layerAssembly;

        private string name;

        public ModuleAssemblyImpl(LayerAssembly layerAssembly, string name)
        {
            this.layerAssembly = layerAssembly;
            this.name = name;
        }

        public EntityDeclaration AddEntities(params Type[] compositeTypes)
        {
            var declaration = new EntityDeclarationImpl(compositeTypes);
            this.entityDeclarations.Add(declaration);
            return declaration;
        }

        public ServiceDeclaration AddServices(params Type[] compositeTypes)
        {
            var declaration = new ServiceDeclarationImpl(compositeTypes);
            this.serviceDeclarations.Add(declaration);
            return declaration;
        }

        public TransientDeclaration AddTransients(params Type[] compositeTypes)
        {
            var declaration = new TransientDeclarationImpl(compositeTypes);
            this.transientDeclarations.Add(declaration);
            return declaration;
        }

        public ValueDeclaration AddValues(params Type[] compositeTypes)
        {
            var declaration = new ValueDeclarationImpl(compositeTypes);
            this.valueDeclarations.Add(declaration);
            return declaration;
        }

        public void Visit(AssemblyVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}