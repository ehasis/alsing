namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ModuleInstance
    {
        private readonly IDictionary<Type, CompositeFinder> compositeFinders = new Dictionary<Type, CompositeFinder>();

        private static Type GetMatchingComposite(Type mixinType)
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where mixinType.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
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

        private void VisitModules(CompositeFinder finder)
        {
            finder.Module = this;
            Type compositeType = GetMatchingComposite(finder.Type);

            finder.Model = CompositeModel.NewModel(compositeType, null);
        }

        public StructureContext GetStructureContext()
        {
            throw new Exception();
        }
    }
}