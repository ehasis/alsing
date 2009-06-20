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

            Type compositeType = CompositeCache.GetMatchingComposite(this.MixinType);

            var metaInfo = new MetaInfo();
            PropertyDeclarations propertyDeclarations = null;
            var assemblyConcerns = new List<Type>();

            var sideEffectsDeclaration = new List<Type>();
            var mixins = new List<Type>();

            CompositeModel m = CompositeModel.NewModel(compositeType, visibility, metaInfo, propertyDeclarations, assemblyConcerns, sideEffectsDeclaration, mixins);
            return m;
        }
    }
}