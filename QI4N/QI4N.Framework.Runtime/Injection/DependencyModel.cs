namespace QI4N.Framework.Runtime
{
    using System;

    public class DependencyModel
    {
        // Model

        private readonly Type injectedClass;

        private readonly InjectionAttribute injectionAnnotation;

        private readonly Type injectionClass;

        private readonly Type injectionType;

        private readonly bool optional;

        private readonly Type rawInjectionClass;

        // Binding
        //private InjectionProvider injectionProvider;

        public DependencyModel(InjectionAttribute attribute, Type injectionType, Type injectedType, bool optional)
        {
            this.injectionAnnotation = this.injectionAnnotation;
            this.injectedClass = injectedType;
            this.injectionType = injectionType;
            this.optional = optional;
            this.rawInjectionClass = injectionType; //MapPrimitiveTypes(ExtractRawInjectionClass(injectedType, injectionType));
            this.injectionClass = injectionType; //ExtractRawInjectionClass(injectionType);
        }

        public object Inject(InjectionContext context)
        {
            throw new NotImplementedException();
        }

        private static Type ExtractRawInjectionClass(Type injectionType)
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
    }
}