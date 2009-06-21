namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class CompositeFinder : TypeFinder<CompositeModel>
    {
        protected override CompositeModel FindModel(ModuleModel model, Visibility visibility)
        {
            return model.Composites.GetCompositeModelFor(this.MixinType, visibility);
        }
    }
}