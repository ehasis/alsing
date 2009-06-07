namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeBuilderState
    {
        private readonly IDictionary<MethodInfo, Property> properties;

        public CompositeBuilderState(IDictionary<MethodInfo, Property> properties)
        {
            this.properties = properties;
        }


        public AbstractAssociation GetAssociation(MethodInfo qualifiedName)
        {
            throw new NotSupportedException("May not use Associations in Composites that are not accessed through a UnitOfWork");
        }

        public Property GetProperty(MethodInfo method)
        {
            return this.properties[method];
        }
    }
}