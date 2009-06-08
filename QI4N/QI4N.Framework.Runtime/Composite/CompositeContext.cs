namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeContext
    {
        public CompositeBinding GetCompositeBinding()
        {
            throw new NotImplementedException();
        }

        public MethodDescriptor GetMethodDescriptor(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PropertyContext> GetPropertyContexts()
        {
            throw new NotImplementedException();
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, HashSet<object> uses, CompositeBuilderState compositeBuilderState)
        {
            throw new NotImplementedException();
        }

    }

    public interface MethodDescriptor
    {
        CompositeMethodContext GetCompositeMethodContext();
    }

    public interface CompositeMethodContext
    {
        PropertyContext GetPropertyContext();
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