namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class CompositeFinder : TypeFinder<CompositeModel>
    {
        protected override CompositeModel FindModel(ModuleModel model, Visibility visibility)
        {
            //return model.composites().getCompositeModelFor(type, visibility);

            Type compositeType = CompositeCache.GetMatchingComposite(this.MixinType);

            var metaInfo = new MetaInfo();
            PropertyDeclarations propertyDeclarations = null;
            var assemblyConcerns = new List<object>();
            var sideEffectsDeclaration = new List<object>();
            var mixins = new List<Type>();

            CompositeModel m = CompositeModel.NewModel(compositeType,visibility,metaInfo,propertyDeclarations,assemblyConcerns,sideEffectsDeclaration,mixins);
            return m;
        }
    }
}