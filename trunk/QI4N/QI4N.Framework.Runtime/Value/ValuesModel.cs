namespace QI4N.Framework.Runtime
{
    using System;

    using Bootstrap;

    public class ValuesModel
    {
        public static ValuesModel NewModel(ModuleAssembly module)
        {
            return new ValuesModel();
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}