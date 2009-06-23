namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public abstract class AbstractPropertiesModel
    {
  //      private readonly IDictionary<string, MethodInfo> accessors = new Dictionary<string, MethodInfo>();

 //       private readonly IDictionary<MethodInfo, PropertyModel> mapMethodPropertyModel = new Dictionary<MethodInfo, PropertyModel>();

        private readonly IList<PropertyModel> propertyModels = new List<PropertyModel>();

        public void AddPropertyFor(MethodInfo method, Type compositeType)
        {
            if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
            {
                PropertyModel propertyModel = NewPropertyModel(method, compositeType);
                this.propertyModels.Add(propertyModel);
  //              this.accessors.Add(propertyModel.QualifiedName, method);
  //              this.mapMethodPropertyModel.Add(method, propertyModel);
            }
            else
            {
                //support for native .net properties
                if (!method.Name.StartsWith("get_"))
                    return;

                var propertyName = method.Name.Substring(4);
                var propertyInfo = (from type in compositeType.GetAllInterfaces()
                                    from prop in type.GetProperties()
                                    where prop.Name == propertyName
                                    select prop).FirstOrDefault();


                if (propertyInfo == null)
                    return;

                PropertyModel propertyModel = NewPropertyModel(propertyInfo, compositeType);
                this.propertyModels.Add(propertyModel);
                

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

                initialValue = CloneInitialValue(initialValue, false);

                // Create property instance
                AbstractProperty property = propertyModel.NewInstance(initialValue);
                properties.Add(propertyModel.Accessor, property);
            }
            return new PropertiesInstance(properties);
        }

        protected static PropertyModel NewPropertyModel(MethodInfo accessor, Type compositeType)
        {
            PropertyModel model = PropertyModelFactory.NewInstance(accessor);

            return model;
        }

        protected static PropertyModel NewPropertyModel(PropertyInfo accessor, Type compositeType)
        {
            PropertyModel model = PropertyModelFactory.NewInstance(accessor);

            return model;
        }

        private static object CloneInitialValue(object initialValue, bool p)
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