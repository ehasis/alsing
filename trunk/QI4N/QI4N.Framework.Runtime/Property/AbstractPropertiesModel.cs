namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Bootstrap;

    public abstract class AbstractPropertiesModel
    {
        protected readonly AbstractConstraintsModel constraintsModel;

        protected readonly PropertyDeclarations propertyDeclarations;

        protected readonly bool immutable;

        protected readonly IList<PropertyModel> propertyModels = new List<PropertyModel>();

        protected AbstractPropertiesModel(AbstractConstraintsModel constraintsModel, PropertyDeclarations propertyDeclarations, bool immutable)
        {
            this.constraintsModel = constraintsModel;
            this.propertyDeclarations = propertyDeclarations;
            this.immutable = immutable;
        }

        public void AddPropertyFor(PropertyInfo propertyInfo, Type compositeType)
        {
            PropertyModel propertyModel = NewPropertyModel(propertyInfo, compositeType);
            this.propertyModels.Add(propertyModel);
        }

        public StateHolder NewBuilderInstance()
        {
            var properties = new Dictionary<PropertyInfo, Property>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                Property property = propertyModel.NewBuilderInstance();
                properties.Add(propertyModel.PropertyInfo, property);
            }

            return new PropertiesInstance(properties);
        }

        public StateHolder NewInitialInstance()
        {
            var properties = new Dictionary<PropertyInfo, Property>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                Property property = propertyModel.NewInitialInstance();
                properties.Add(propertyModel.PropertyInfo, property);
            }

            return new PropertiesInstance(properties);
        }

        public StateHolder NewInstance(StateHolder state)
        {
            var properties = new Dictionary<PropertyInfo, Property>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                var originalProperty = state.GetProperty(propertyModel.PropertyInfo);
                object initialValue = originalProperty.Value;

                initialValue = CloneInitialValue(initialValue, false);

                // Create property instance
                Property property = propertyModel.NewInstance(initialValue);
                properties.Add(propertyModel.PropertyInfo, property);
            }
            return new PropertiesInstance(properties);
        }


        protected abstract PropertyModel NewPropertyModel(PropertyInfo propertyInfo, Type compositeType);


        private static object CloneInitialValue(object initialValue, bool immutable)
        {
            if (initialValue is ICloneable)
            {
                var c = initialValue as ICloneable;
                return c.Clone();
            }

            return initialValue;
        }
    }
}