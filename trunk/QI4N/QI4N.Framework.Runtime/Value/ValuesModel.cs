namespace QI4N.Framework.Runtime
{
    using System;

    public class ValuesModel
    {
        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public static ValuesModel NewModel(QI4N.Framework.Bootstrap.ModuleAssembly module)
        {
            return new ValuesModel();
        }
    }
}