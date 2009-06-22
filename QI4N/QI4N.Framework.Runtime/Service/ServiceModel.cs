namespace QI4N.Framework.Runtime
{
    using System;

    public sealed class ServiceModel : AbstractCompositeModel
    {
        public ServiceModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, AbstractStateModel stateModel, CompositeMethodsModel compositeMethodsModel)
                : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {
        }
    }
}