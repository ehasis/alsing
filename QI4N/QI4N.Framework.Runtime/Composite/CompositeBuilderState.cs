namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeBuilderState
    {
        private readonly IDictionary<MethodInfo, AbstractProperty> properties;

        public CompositeBuilderState(IDictionary<MethodInfo, AbstractProperty> properties)
        {
            this.properties = properties;
        }


        public AbstractAssociation GetAssociation(MethodInfo qualifiedName)
        {
            throw new NotSupportedException("May not use Associations in Composites that are not accessed through a UnitOfWork");
        }

        public AbstractProperty GetProperty(MethodInfo method)
        {
            return this.properties[method];
        }
    }
}