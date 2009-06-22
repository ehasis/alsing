namespace QI4N.Framework.Runtime
{
    public class EntityFinder : AbstractTypeFinder<EntityModel>
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