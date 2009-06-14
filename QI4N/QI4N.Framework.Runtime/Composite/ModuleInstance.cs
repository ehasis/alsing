namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, EntityFinder> entityFinders = new Dictionary<Type, EntityFinder>();
        private readonly IDictionary<Type, CompositeFinder> compositeFinders = new Dictionary<Type, CompositeFinder>();

        private ModuleModel moduleModel;
        private LayerInstance layerInstance;

        private static Type GetMatchingComposite(Type mixinType)
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where mixinType.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
        }

        public EntityFinder FindEntityModel(Type mixinType)
        {
            EntityFinder finder;
            if (!entityFinders.TryGetValue(mixinType, out finder))
            {
                finder = new EntityFinder
                {
                    Type = mixinType
                };
                VisitModules(finder);
                if (finder.Model != null)
                {
                    entityFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public CompositeFinder FindCompositeModel(Type mixinType)
        {
            CompositeFinder finder;
            if (!compositeFinders.TryGetValue(mixinType,out finder))
            {
                finder = new CompositeFinder
                             {
                                     Type = mixinType
                             };
                VisitModules(finder);
                if (finder.Model != null)
                {
                    compositeFinders.Add(mixinType, finder);
                }
            }

            return finder;            
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(this, moduleModel, Visibility.Module))
            {
                return;
            }

            // Visit layer
            layerInstance.VisitModules(visitor, Visibility.Layer);
        }

        public StructureContext GetStructureContext()
        {
            throw new Exception();
        }
    }

    public class LayerInstance
    {
        internal void VisitModules(ModuleVisitor visitor, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }

    public interface Module
    {
    }
}