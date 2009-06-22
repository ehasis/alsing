namespace QI4N.Framework.Runtime
{
    using System;

    public abstract class AbstractTypeFinder<T> : ModuleVisitor where T : AbstractCompositeModel
    {
        public Type MixinType { get; set; }

        public T Model { get; set; }

        public ModuleInstance Module { get; set; }

        public Visibility Visibility { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            T foundModel = this.FindModel(moduleModel, visibility);
            if (foundModel != null)
            {
                if (this.Model == null)
                {
                    this.Model = foundModel;
                    this.Module = moduleInstance;
                    this.Visibility = visibility;
                }
                else
                {
                    // If same visibility -> ambiguous types
                    if (this.Visibility == visibility)
                    {
                        throw new Exception("AmbigiousType"); //AmbiguousTypeException(type);
                    }
                }
            }

            // Break if we have found a model and visibility has changed since the find
            return !(this.Model != null && this.Visibility != visibility);
        }

        protected abstract T FindModel(ModuleModel model, Visibility visibility);
    }
}