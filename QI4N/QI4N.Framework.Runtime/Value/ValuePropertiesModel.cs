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
            object initialValue = propertyDeclarations.GetInitialValue(propertyInfo);
            bool immutable = this.immutable; // || this.metaInfo.Get( typeof(ImmutableAttribute)) != null;

            var model = new PropertyModel(propertyInfo, immutable, initialValue);

            return model;
        }
    }
}