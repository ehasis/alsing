namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class ValueDeclarationImpl : AbstractCompositeDeclarationImpl<ValueDeclaration, ValueComposite>, ValueDeclaration
    {
        public void AddValues(List<ValueModel> values, PropertyDeclarations propertyDecs)
        {
            foreach (Type compositeType in this.CompositeTypes)
            {
                ValueModel valueModel = ValueModel.NewModel(compositeType,
                                                            this.visibility,
                                                            new MetaInfo(metaInfo).WithAnnotations(compositeType),
                                                            propertyDecs,
                                                            this.concerns,
                                                            this.sideEffects,
                                                            this.mixins);
                values.Add(valueModel);
            }
        }
    }
}