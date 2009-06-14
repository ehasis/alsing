namespace QI4N.Framework.Runtime
{
    using System;

    public abstract class TypeFinder<T> : ModuleVisitor
    {
        public Type Type { get; set; }
        public T Model { get; set; }
        public ModuleInstance Module { get; set; }
        public Visibility Visibility { get; set; }

        public bool VisitModule(ModuleInstance moduleInstance, ModuleModel moduleModel, Visibility visibility)
        {
            Model = FindModel(moduleModel, visibility);
            return true;
            throw new NotImplementedException();
        }

        protected abstract T FindModel(ModuleModel model, Visibility visibility);
    }
}