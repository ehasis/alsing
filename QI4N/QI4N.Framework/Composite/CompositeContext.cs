namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;

    public interface CompositeContext
    {
        CompositeBinding GetCompositeBinding();

        IEnumerable<PropertyContext> GetPropertyContexts();

        CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, HashSet<object> uses, CompositeBuilderState compositeBuilderState);
    }

    public interface CompositeBinding
    {
        CompositeResolution GetCompositeResolution();
    }

    public interface CompositeResolution
    {
        CompositeModel GetCompositeModel();
    }

    public interface CompositeModel
    {
        Type GetCompositeType();
    }
}