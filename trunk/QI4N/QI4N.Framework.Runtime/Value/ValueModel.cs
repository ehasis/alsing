namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ValueModel : AbstractCompositeModel
    {
        public ValueModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }

        public static ValueModel NewModel(Type type, Visibility visibility, MetaInfo info, PropertyDeclarations decs, List<Type> concerns, List<Type> effects, List<Type> mixins)
        {
            throw new NotImplementedException();
        }

        public static ValueModel NewModel(Type type, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }
}