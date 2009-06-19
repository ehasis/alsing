namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ModuleAssembly
    {
        EntitiesDeclaration AddEntities(params Type[] types);

        ServiceDeclaration AddServices(params Type[] types);

        ValueDeclaration AddValues(params Type[] types);

        TransientDeclaration AddTransients(params Type[] types);
    }

    public class ModuleAssemblyImpl : ModuleAssembly
    {
        private LayerAssembly layerAssembly;

        private string name;

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

        public TransientDeclaration AddTransients(params Type[] types)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Visit(AssemblyVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}