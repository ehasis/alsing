namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ModuleInstance
    {
        private Type GetMatchingComposite(Type mixinType)
        {
            IEnumerable<Type> matchingComposites = from composite in CompositeTypeCache.Composites
                                                   where mixinType.IsAssignableFrom(composite) &&
                                                         composite.IsInterface
                                                   select composite;

            return matchingComposites.Single();
        }

        public CompositeFinder FindCompositeModel(Type mixinType)
        {
            Type compositeType = GetMatchingComposite(mixinType);
            var finder = new CompositeFinder
                             {
                                     Module = this,
                                     Model = new CompositeModel(null, compositeType)
                             };
            return finder;
        }

        public StructureContext GetStructureContext()
        {
            throw new Exception();
        }
    }
}