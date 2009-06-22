namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public sealed class TransientFinder : TypeFinder<TransientModel>
    {
        protected override TransientModel FindModel(ModuleModel model, Visibility visibility)
        {
            return model.Transients.GetCompositeModelFor(this.MixinType, visibility);
        }
    }
}