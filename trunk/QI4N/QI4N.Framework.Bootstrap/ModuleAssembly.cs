namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;

    public interface ModuleAssembly
    {
        EntitiesDeclaration AddEntities(params Type[] types);

        ServiceDeclaration AddServices(params Type[] types);

        ValueDeclaration AddValues(params Type[] types);

        TransientDeclaration AddTransients(params Type[] compositeTypes);
    }

    public class ModuleAssemblyImpl : ModuleAssembly
    {
        private LayerAssembly layerAssembly;

        private string name;

        private readonly IList<TransientDeclaration> transientDeclarations = new List<TransientDeclaration>();

        public ModuleAssemblyImpl(LayerAssembly layerAssembly, string name)
        {
            this.layerAssembly = layerAssembly;
            this.name = name;
        }



        #region ModuleAssembly Members

        public EntitiesDeclaration AddEntities(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public ServiceDeclaration AddServices(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public ValueDeclaration AddValues(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public TransientDeclaration AddTransients(params Type[] compositeTypes)
        {
            foreach (Type compositeType in compositeTypes)
            {
                if (!typeof(TransientComposite).IsAssignableFrom(compositeType))
                {
                    throw new Exception("Type is not a transient composite " + compositeType.Name);
                }
            }

            TransientDeclarationImpl transientDeclaration = new TransientDeclarationImpl(compositeTypes);
            transientDeclarations.Add(transientDeclaration);
            return transientDeclaration;
        }

        #endregion

        public void Visit(AssemblyVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}