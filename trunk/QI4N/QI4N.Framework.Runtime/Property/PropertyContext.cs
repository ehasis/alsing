using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    using Proxy;

    public class PropertyContext
    {
        private readonly PropertyBinding propertyBinding;

        public PropertyContext(PropertyBinding propertyBinding)
        {
            this.propertyBinding = propertyBinding;
        }


        public PropertyBinding GetPropertyBinding()
        {
            return propertyBinding;
        }

        public AbstractProperty NewInstance(ModuleInstance moduleInstance, object value, Type propertyType)
        {
            if (typeof(Composite).IsAssignableFrom(propertyType))
            {
                Type propertyCompositeType = propertyType;

                var cb = moduleInstance.GetStructureContext().GetCompositeBuilderFactory().NewCompositeBuilder(propertyCompositeType);
                cb.Use(value);
                cb.Use(propertyBinding);
                var property = cb.NewInstance() as AbstractProperty;

                return property;
            }
            else
            {
                var property = ProxyInstanceBuilder.NewProxyInstance(propertyType) as AbstractProperty;
                if (property == null)
                {
                    throw new NotSupportedException("Type is not a property");
                }
                property.Value = value;

                return property;
            }
        }

        public AbstractProperty newEntityInstance(ModuleInstance moduleInstance, EntityState entityState)
        {
            return null;
        }

    //        public Property newInstance( ModuleInstance moduleInstance, Object value )
    //{
    //    try
    //    {
    //        Class propertyType = propertyBinding.getPropertyResolution().getPropertyModel().getAccessor().getReturnType();

    //        if( Composite.class.isAssignableFrom( propertyType ) )
    //        {
    //            Class<? extends Composite> propertyCompositeType = (Class<? extends Composite>) propertyType;
    //            CompositeBuilder<? extends Composite> cb = moduleInstance.getStructureContext().getCompositeBuilderFactory().newCompositeBuilder( propertyCompositeType );
    //            cb.use( value );
    //            cb.use( propertyBinding );
    //            return Property.class.cast( cb.newInstance() );
    //        }
    //        else
    //        {
    //            Property instance;
    //            if( ImmutableProperty.class.isAssignableFrom( propertyType ) )
    //            {
    //                instance = new ImmutablePropertyInstance<Object>( propertyBinding, value );
    //            }
    //            else
    //            {
    //                instance = new PropertyInstance<Object>( propertyBinding, value );
    //            }

    //            return instance;
    //        }
    //    }
    //    catch( Exception e )
    //    {
    //        throw new InvalidPropertyException( "Could not instantiate property", e );
    //    }
    //}

    //public Property newEntityInstance( ModuleInstance moduleInstance, EntityState entityState )
    //{
    //    try
    //    {
    //        Class propertyType = propertyBinding.getPropertyResolution().getPropertyModel().getAccessor().getReturnType();

    //        if( Composite.class.isAssignableFrom( propertyType ) )
    //        {
    //            Class<? extends Composite> propertyCompositeType = (Class<? extends Composite>) propertyType;
    //            CompositeBuilder<? extends Composite> cb = moduleInstance.getStructureContext().getCompositeBuilderFactory().newCompositeBuilder( propertyCompositeType );
    //            cb.use( entityState );
    //            cb.use( propertyBinding );
    //            return Property.class.cast( cb.newInstance() );
    //        }
    //        else
    //        {
    //            Property instance;
    //            if( ImmutableProperty.class.isAssignableFrom( propertyType ) )
    //            {
    //                instance = new ImmutablePropertyInstance<Object>( propertyBinding, entityState.getProperty( propertyBinding.qualifiedName() ) );
    //            }
    //            else
    //            {
    //                instance = new EntityPropertyInstance<Object>( propertyBinding, entityState );
    //            }

    //            return instance;
    //        }
    //    }
    //    catch( Exception e )
    //    {
    //        throw new InvalidPropertyException( "Could not instantiate property", e );
    //    }
    //}


    }

    public class EntityState
    {
    }
}
