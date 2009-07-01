namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Bootstrap;

    public class ServicePropertiesModel : AbstractPropertiesModel
    {
        public ServicePropertiesModel(ConstraintsModel constraintsModel, PropertyDeclarations declarations, bool immutable)
                : base(constraintsModel, declarations, immutable)
        {
        }

        protected override PropertyModel NewPropertyModel(PropertyInfo propertyInfo, Type compositeType)
        {
            object initialValue = this.propertyDeclarations.GetInitialValue(propertyInfo);
            bool immutable = this.immutable; // || this.metaInfo.Get( typeof(ImmutableAttribute)) != null;

            var model = new PropertyModel(propertyInfo, immutable, initialValue);

            return model;

            //Annotation[] annotations = Annotations.getMethodAndTypeAnnotations( method );
            //boolean optional = Annotations.getAnnotationOfType( annotations, Optional.class ) != null;
            //ValueConstraintsModel valueConstraintsModel = constraints.constraintsFor( annotations, GenericPropertyInfo.getPropertyType( method ), method.getName(), optional );
            //ValueConstraintsInstance valueConstraintsInstance = null;
            //if( valueConstraintsModel.isConstrained() )
            //{
            //    valueConstraintsInstance = valueConstraintsModel.newInstance();
            //}
            //MetaInfo metaInfo = propertyDeclarations.getMetaInfo( method );
            //Object initialValue = propertyDeclarations.getInitialValue( method );
            //boolean immutable = this.immutable || metaInfo.get( Immutable.class ) != null;
            //PropertyModel propertyModel = new PropertyModel( method, immutable, valueConstraintsInstance, metaInfo, initialValue );
            //return propertyModel;
        }
    }
}