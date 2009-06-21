namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ValuesModel
    {
        private readonly List<ValueModel> valueModels;

        public ValuesModel(List<ValueModel> valueModels)
        {
            this.valueModels = valueModels;
        }

        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public ValueModel GetValueModelFor(Type mixinType, Visibility visibility)
        {
            ValueModel foundModel = null;
            foreach (ValueModel composite in this.valueModels)
            {
                if (typeof(Value).IsAssignableFrom(mixinType))
                {
                    if (mixinType == composite.CompositeType && composite.Visibility == visibility)
                    {
                        if (foundModel != null)
                        {
                            throw new AmbiguousTypeException(mixinType, foundModel.CompositeType, composite.CompositeType);
                        }

                        foundModel = composite;
                    }
                }
                else
                {
                    if (mixinType.IsAssignableFrom(composite.CompositeType) && composite.Visibility == visibility)
                    {
                        if (foundModel != null)
                        {
                            throw new AmbiguousTypeException(mixinType, foundModel.CompositeType, composite.CompositeType);
                        }
                        foundModel = composite;
                    }
                }
            }

            return foundModel;
        }
    }
}