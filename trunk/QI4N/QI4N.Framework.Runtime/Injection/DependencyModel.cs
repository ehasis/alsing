using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public class DependencyModel
    {
        // Model
        private readonly InjectionAttribute injectionAnnotation;
        private readonly Type injectionType;
        private readonly Type injectedClass;
        private readonly Type rawInjectionClass;
        private readonly Type injectionClass;
        private readonly bool optional;

        // Binding
        //private InjectionProvider injectionProvider;

        public DependencyModel(InjectionAttribute attribute, Type injectionType, Type injectedType, bool optional)
        {
            this.injectionAnnotation = injectionAnnotation;
            this.injectedClass = injectedType;
            this.injectionType = injectionType;
            this.optional = optional;
            this.rawInjectionClass = injectionType; //MapPrimitiveTypes(ExtractRawInjectionClass(injectedType, injectionType));
            this.injectionClass = injectionType; //ExtractRawInjectionClass(injectionType);
        }

        private static Type ExtractRawInjectionClass( Type injectionType)
        {
            // Calculate raw injection type
            if (injectionType.IsClass)
            {
                return injectionType;
            }
            //else if( injectionType instanceof ParameterizedType )
            //{
            //    return (Class<?>) ( (ParameterizedType) injectionType ).getRawType();
            //}
            //else if( injectionType instanceof TypeVariable )
            //{
            //    return extractRawInjectionClass( injectedClass, (TypeVariable<?>) injectionType );
            //}
            //throw new IllegalArgumentException( "Could not extract the rawInjectionClass of " + injectedClass + " and " + injectionType );

            throw new Exception("Not implemented");
        }

        public object Inject(InjectionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
