namespace QI4N.Framework.Runtime
{
    using System;

    public class CompositeFinder : TypeFinder<CompositeModel>
    {
        protected override CompositeModel FindModel(ModuleModel model, Visibility visibility)
        {
            //return model.composites().getCompositeModelFor(type, visibility);

            Type compositeType = CompositeCache.GetMatchingComposite(this.MixinType);

            CompositeModel m = CompositeModel.NewModel(compositeType, null);
            return m;
        }
    }
}