namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class AbstractPropertiesModel
    {
        private readonly IDictionary<string, MethodInfo> accessors = new Dictionary<string, MethodInfo>();

        private readonly IDictionary<MethodInfo, PropertyModel> mapMethodPropertyModel = new Dictionary<MethodInfo, PropertyModel>();

        private readonly IList<PropertyModel> propertyModels = new List<PropertyModel>();

        public void AddPropertyFor(MethodInfo method, Type compositeType)
        {
            if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
            {
                PropertyModel propertyModel = this.NewPropertyModel(method, compositeType);
                this.propertyModels.Add(propertyModel);
                this.accessors.Add(propertyModel.QualifiedName, method);
                this.mapMethodPropertyModel.Add(method, propertyModel);
            }
        }

        public StateHolder NewBuilderInstance()
        {
            var properties = new Dictionary<MethodInfo, AbstractProperty>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                AbstractProperty property = propertyModel.NewBuilderInstance();
                properties.Add(propertyModel.Accessor, property);
            }

            return new PropertiesInstance(properties);
        }

        public StateHolder NewInitialInstance()
        {
            var properties = new Dictionary<MethodInfo, AbstractProperty>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                AbstractProperty property = propertyModel.NewInitialInstance();
                properties.Add(propertyModel.Accessor, property);
            }

            return new PropertiesInstance(properties);
        }

        public StateHolder NewInstance(StateHolder state)
        {
            var properties = new Dictionary<MethodInfo, AbstractProperty>();
            foreach (PropertyModel propertyModel in this.propertyModels)
            {
                object initialValue = state.GetProperty(propertyModel.Accessor).Value;

                initialValue = this.CloneInitialValue(initialValue, false);

                // Create property instance
                AbstractProperty property = propertyModel.NewInstance(initialValue);
                properties.Add(propertyModel.Accessor, property);
            }
            return new PropertiesInstance(properties);
        }

        protected PropertyModel NewPropertyModel(MethodInfo accessor, Type compositeType)
        {
            var model = PropertyModelFactory.NewInstance(accessor.ReturnType);

            return model;
        }

        private object CloneInitialValue(object initialValue, bool p)
        {
            return initialValue;
        }
    }
}