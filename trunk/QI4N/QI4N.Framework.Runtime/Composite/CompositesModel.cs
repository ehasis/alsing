namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public class CompositesModel
    {
        private readonly IList<CompositeModel> compositeModels;

        public CompositesModel(IList<CompositeModel> compositeModels)
        {
            this.compositeModels = compositeModels;
        }

        public static CompositesModel NewModel(ModuleAssembly module)
        {
            var compositeModels = new List<CompositeModel>();
            foreach (TransientDeclaration transients in module.TransientDeclarations)
            {
                CompositeModel compositeModel = null;
                compositeModels.Add(compositeModel);
            }

            var model = new CompositesModel(compositeModels);
            return model;
        }

        public CompositeModel GetCompositeModelFor(Type mixinType, Visibility visibility)
        {
            CompositeModel foundModel = null;
            foreach (CompositeModel composite in this.compositeModels)
            {
                if (typeof(Composite).IsAssignableFrom(mixinType))
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
            foreach (CompositeModel compositeModel in this.compositeModels)
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
            foreach (CompositeModel compositeModel in this.compositeModels)
            {
                compositeModel.VisitModel(visitor);
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