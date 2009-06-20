namespace QI4N.Framework.Runtime
{
    using System;

    public class ServicesModel
    {
        public ServicesModel NewInstance(ModuleInstance instance)
        {
            throw new NotImplementedException();
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        internal static ServicesModel NewModel(QI4N.Framework.Bootstrap.ModuleAssembly module)
        {
            return new ServicesModel();
        }
    }
}