namespace QI4N.Framework
{
    using System.Collections.Generic;
    using System.Reflection;

    public class DefaultEntityStateHolder : EntityStateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractAssociation> associations = new Dictionary<MethodInfo, AbstractAssociation>();

        private readonly IDictionary<MethodInfo, Property> properties = new Dictionary<MethodInfo, Property>();

        public AbstractAssociation GetAssociation(MethodInfo associationMethod)
        {
            return this.associations[associationMethod];
        }

        public Property GetProperty(MethodInfo propertyMethod)
        {
            return this.properties[propertyMethod];
        }
    }
}