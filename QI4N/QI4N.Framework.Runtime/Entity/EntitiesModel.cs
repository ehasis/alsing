namespace QI4N.Framework.Runtime
{
    using System;

    using Bootstrap;

    public class EntitiesModel
    {
        public static EntitiesModel NewModel(ModuleAssembly module)
        {
            return new EntitiesModel();
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}