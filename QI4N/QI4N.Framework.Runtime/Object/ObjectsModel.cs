namespace QI4N.Framework.Runtime
{
    using System;

    using Bootstrap;

    public class ObjectsModel
    {
        public static ObjectsModel NewModel(ModuleAssembly module)
        {
            return new ObjectsModel();
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}