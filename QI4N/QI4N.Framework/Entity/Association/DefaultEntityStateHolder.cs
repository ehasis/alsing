namespace QI4N.Framework
{
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

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
            if (!this.properties.ContainsKey(propertyMethod))
            {
                //lazy build properties
                var proxyBuilder = new ProxyInstanceBuilder();
                var property = proxyBuilder.NewInstance(propertyMethod.ReturnType) as Property;
                this.properties.Add(propertyMethod, property);
            }
            return this.properties[propertyMethod];
        }

        public void AddProperty(MethodInfo propertyMethod, Property property)
        {
            properties.Add(propertyMethod, property);
        }
    }
}