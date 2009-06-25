namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public sealed class TransientsModel
    {
        private readonly IList<TransientModel> compositeModels;

        public TransientsModel(IList<TransientModel> compositeModels)
        {
            this.compositeModels = compositeModels;
        }

        public TransientModel GetCompositeModelFor(Type mixinType, Visibility visibility)
        {
            TransientModel foundModel = null;
            foreach (TransientModel composite in this.compositeModels)
            {
                if (typeof(TransientComposite).IsAssignableFrom(mixinType))
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

        public Type GetTypeForName(String type)
        {
            foreach (TransientModel compositeModel in this.compositeModels)
            {
                if (compositeModel.CompositeType.Name == type)
                {
                    return compositeModel.CompositeType;
                }
            }

            return null;
        }

        public void VisitModel(ModelVisitor visitor)
        {
            foreach (TransientModel compositeModel in this.compositeModels)
            {
                TransientModel.VisitModel(visitor);
            }
        }
    }

    public class AmbiguousTypeException : Exception
    {
        public AmbiguousTypeException(Type type, Type compositeType, Type type1)
        {
        }
    }
}