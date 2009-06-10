namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    using Proxy;

    public class DefaultEntityStateHolder : EntityStateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractAssociation> associations = new Dictionary<MethodInfo, AbstractAssociation>();

        private readonly IDictionary<MethodInfo, AbstractProperty> properties = new Dictionary<MethodInfo, AbstractProperty>();

        public void AddProperty(MethodInfo propertyMethod, AbstractProperty property)
        {
            this.properties.Add(propertyMethod, property);
        }

        public AbstractAssociation GetAssociation(MethodInfo associationMethod)
        {
            if (!this.associations.ContainsKey(associationMethod))
            {
                //lazy build properties
                var association = ProxyInstanceBuilder.NewProxyInstance(associationMethod.ReturnType) as AbstractAssociation;
                this.associations.Add(associationMethod, association);
            }

            return this.associations[associationMethod];
        }

        public AbstractProperty GetProperty(MethodInfo propertyMethod)
        {
            if (!this.properties.ContainsKey(propertyMethod))
            {
                //lazy build properties
                var property = ProxyInstanceBuilder.NewProxyInstance(propertyMethod.ReturnType) as AbstractProperty;
                this.properties.Add(propertyMethod, property);
            }
            return this.properties[propertyMethod];
        }
    }
}