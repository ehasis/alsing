namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class AbstractPropertiesModel
    {
        private readonly IList<PropertyModel> propertyModels = new List<PropertyModel>();

        private readonly IDictionary<string, MethodInfo> accessors = new Dictionary<string, MethodInfo>();

        private readonly IDictionary<MethodInfo, PropertyModel> mapMethodPropertyModel = new Dictionary<MethodInfo, PropertyModel>();

        public PropertiesInstance NewBuilderInstance()
        {
            var properties = new Dictionary<MethodInfo, AbstractProperty>();
            foreach (var propertyModel in propertyModels)
            {
                AbstractProperty property = propertyModel.NewBuilderInstance();
                properties.Add(propertyModel.Accessor, property);
            }

            return new PropertiesInstance( properties );
        }

        public StateHolder NewInitialInstance()
        {
            return new DefaultEntityStateHolder();
        }

        public StateHolder NewInstance(StateHolder state)
        {
            var newState = new DefaultEntityStateHolder();

            return newState;
        }

        public void AddPropertyFor(MethodInfo method, Type compositeType)
        {
            if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
            {
                PropertyModel propertyModel = NewPropertyModel(method, compositeType);
                propertyModels.Add(propertyModel);
                accessors.Add(propertyModel.QualifiedName, method);
                mapMethodPropertyModel.Add(method, propertyModel);
            }
        }

        protected PropertyModel NewPropertyModel(MethodInfo accessor, Type compositeType)
        {

            //var model = new PropertyModel(accessor);

            //return model;
            return null;
        }
    }


}