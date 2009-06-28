namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Bootstrap;

    public class TransientPropertiesModel : AbstractPropertiesModel
    {
        public TransientPropertiesModel(AbstractConstraintsModel constraintsModel, PropertyDeclarations declarations, bool immutable)
            : base(constraintsModel, declarations, immutable)
        {

        }

        protected override PropertyModel NewPropertyModel(PropertyInfo propertyInfo, Type compositeType)
        {
            PropertyModel model = new PropertyModelImpl(propertyInfo);

            return model;
        }
    }
}