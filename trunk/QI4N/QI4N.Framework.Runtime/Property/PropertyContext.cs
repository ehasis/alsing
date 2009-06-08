using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    using Proxy;

    public class PropertyContext
    {
        private PropertyBinding propertyBinding;

        public PropertyContext(PropertyBinding propertyBinding)
        {
            this.propertyBinding = propertyBinding;
        }


        public PropertyBinding GetPropertyBinding()
        {
            return null;
        }

        public AbstractProperty NewInstance(ModuleInstance moduleInstance, object value, Type type)
        {
            var property = ProxyInstanceBuilder.NewProxyInstance(type) as AbstractProperty;
            property.Value = value;

            return property;
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
