namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class CompositesModel
    {
        public static CompositesModel NewModel(ModuleAssembly module)
        {
            var compositeModels = new List<CompositeModel>();
            foreach (TransientDeclaration composite in module.TransientDeclarations)
            {
            }

            var model = new CompositesModel();
            return model;
        }

        public CompositeModel GetCompositeModelFor(Type type, Visibility visibility)
        {
            return null;
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}