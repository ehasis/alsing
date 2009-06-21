namespace QI4N.Framework.Runtime
{
    using System;

    public class EntityFinder : TypeFinder<EntityModel>
    {
        protected override EntityModel FindModel(ModuleModel model, Visibility visibility)
        {
            Type compositeType = CompositeCache.GetMatchingComposite(this.MixinType);

            EntityModel m = EntityModel.NewModel(compositeType, null);
            return m;
        }
    }

    public interface ModuleVisitor
    {
        bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility);
    }
}