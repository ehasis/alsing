namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ModuleAssembly
    {
        EntitiesDeclaration AddEntity<T>() where T : EntityComposite;

        ServiceDeclaration AddService<T>() where T : ServiceComposite;

        ValueDeclaration AddValue<T>() where T : ValueComposite;

        TransientDeclaration AddTransient<T>() where T : TransientComposite;
    }

    public class ModuleAssemblyImpl : ModuleAssembly
    {
        public ModuleAssemblyImpl(LayerAssemblyImpl impl, string name)
        {
            throw new NotImplementedException();
        }

        public EntitiesDeclaration AddEntity<T>() where T : EntityComposite
        {
            throw new NotImplementedException();
        }

        public ServiceDeclaration AddService<T>() where T : ServiceComposite
        {
            throw new NotImplementedException();
        }

        public TransientDeclaration AddTransient<T>() where T : TransientComposite
        {
            throw new NotImplementedException();
        }

        public ValueDeclaration AddValue<T>() where T : ValueComposite
        {
            throw new NotImplementedException();
        }

        public void Visit(AssemblyVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}