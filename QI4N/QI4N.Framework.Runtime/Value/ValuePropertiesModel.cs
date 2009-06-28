namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Bootstrap;


    public class ValuePropertiesModel : AbstractPropertiesModel
    {
        public ValuePropertiesModel(ValueConstraintsModel constraintsModel, PropertyDeclarations propertyDeclarations, bool immutable)
            : base(constraintsModel,propertyDeclarations,immutable)
        {
        }

        protected override PropertyModel NewPropertyModel(PropertyInfo propertyInfo, Type compositeType)
        {
            PropertyModel model = new PropertyModelImpl(propertyInfo);

            return model;
        }
    }
}