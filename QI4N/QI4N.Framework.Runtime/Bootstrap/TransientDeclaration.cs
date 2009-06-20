namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bootstrap;

    public class TransientDeclarationImpl : AbstractCompositeDeclarationImpl<TransientDeclaration, TransientComposite>, TransientDeclaration
    {
        public void AddTransients(List<CompositeModel> compositeModels, PropertyDeclarations propertyDecs)
        {
            foreach (Type compositeType in this.CompositeTypes)
            {
                CompositeModel compositeModel = CompositeModel.NewModel(compositeType,
                                                            this.visibility,
                                                            new MetaInfo(this.metaInfo).WithAnnotations(compositeType),
                                                            propertyDecs,
                                                            this.concerns.Cast<object>(),
                                                            this.sideEffects.Cast<object>(),
                                                            this.mixins);
                compositeModels.Add(compositeModel);
            }
        }
    }
}