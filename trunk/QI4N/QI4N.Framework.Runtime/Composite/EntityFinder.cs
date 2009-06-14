namespace QI4N.Framework.Runtime
{
    public class EntityFinder : TypeFinder<EntityModel>
    {
        protected override EntityModel FindModel(ModuleModel model, Visibility visibility)
        {
            EntityModel m = EntityModel.NewModel(this.Type, null);
            return m;
        }
    }

    public interface ModuleVisitor
    {
        bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility);
    }

    public class ModuleModel
    {
    }

    public enum Visibility
    {
        Module,
        Layer
    }
}