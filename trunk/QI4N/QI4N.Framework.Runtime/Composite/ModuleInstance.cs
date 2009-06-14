namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ModuleInstance : Module
    {
        private readonly IDictionary<Type, CompositeFinder> compositeFinders = new Dictionary<Type, CompositeFinder>();

        private readonly IDictionary<Type, EntityFinder> entityFinders = new Dictionary<Type, EntityFinder>();

        private LayerInstance layerInstance;

        private ModuleModel moduleModel;


        public CompositeFinder FindCompositeModel(Type mixinType)
        {
            CompositeFinder finder;
            if (!this.compositeFinders.TryGetValue(mixinType, out finder))
            {
                finder = new CompositeFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.compositeFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public EntityFinder FindEntityModel(Type mixinType)
        {
            EntityFinder finder;
            if (!this.entityFinders.TryGetValue(mixinType, out finder))
            {
                finder = new EntityFinder
                             {
                                     MixinType = mixinType
                             };
                this.VisitModules(finder);
                if (finder.Model != null)
                {
                    this.entityFinders.Add(mixinType, finder);
                }
            }

            return finder;
        }

        public StructureContext GetStructureContext()
        {
            throw new Exception();
        }

        public void VisitModules(ModuleVisitor visitor)
        {
            // Visit this module
            if (!visitor.VisitModule(this, this.moduleModel, Visibility.Module))
            {
                return;
            }

            // Visit layer
            //   layerInstance.VisitModules(visitor, Visibility.Layer);
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