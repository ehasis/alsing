namespace QI4N.Framework
{
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

    public class DefaultEntityStateHolder : EntityStateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractAssociation> associations = new Dictionary<MethodInfo, AbstractAssociation>();

        private readonly IDictionary<MethodInfo, Property> properties = new Dictionary<MethodInfo, Property>();

        public void AddProperty(MethodInfo propertyMethod, Property property)
        {
            this.properties.Add(propertyMethod, property);
        }

        public AbstractAssociation GetAssociation(MethodInfo associationMethod)
        {
            if (!this.associations.ContainsKey(associationMethod))
            {
                //lazy build properties
                var association = ProxyGenerator.NewProxyInstance(associationMethod.ReturnType) as AbstractAssociation;
                this.associations.Add(associationMethod, association);
            }

            return this.associations[associationMethod];
        }

        public Property GetProperty(MethodInfo propertyMethod)
        {
            if (!this.properties.ContainsKey(propertyMethod))
            {
                //lazy build properties
                var property = ProxyGenerator.NewProxyInstance(propertyMethod.ReturnType) as Property;
                this.properties.Add(propertyMethod, property);
            }
            return this.properties[propertyMethod];
        }
    }
}