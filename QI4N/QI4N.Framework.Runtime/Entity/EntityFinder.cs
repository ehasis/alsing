namespace QI4N.Framework.Runtime
{
    using System;

    public class EntityFinder : TypeFinder<EntityModel>
    {
        protected override EntityModel FindModel(ModuleModel model, Visibility visibility)
        {
            return null;
        }
    }

    public interface ModuleVisitor
    {
        bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility);
    }
}