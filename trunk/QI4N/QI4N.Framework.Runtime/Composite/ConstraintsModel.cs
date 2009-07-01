namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class ConstraintsModel
    {
        private readonly List<ConstraintDeclaration> constraints = new List<ConstraintDeclaration>();

        private readonly Type declaringType;

        public ConstraintsModel(Type declaringType)
        {
            this.declaringType = declaringType;
            // Find constraint declarations
            IEnumerable<Type> interfaces = declaringType.GetAllInterfaces();

            foreach (Type type in interfaces)
            {
                this.AddConstraintDeclarations(type);
            }
        }

        public ValueConstraintsModel ConstraintsFor(ParameterInfo parameterInfo, string name, bool optional)
        {
            IEnumerable<ConstraintAttribute> constraintAnnotations = parameterInfo.GetAttributes<ConstraintAttribute>();
            var constraintModels = new List<AbstractConstraintModel>();
            foreach (ConstraintAttribute constraintAnnotation in constraintAnnotations)
            {
            }

            return new ValueConstraintsModel(constraintModels, name, optional);

            //    // Check composite declarations first
            //    Class<? extends Annotation> annotationType = constraintAnnotation.annotationType();
            //    for( ConstraintDeclaration constraint : constraints )
            //    {
            //        if( constraint.appliesTo( annotationType, valueType ) )
            //        {
            //            constraintModels.add( new ConstraintModel( constraintAnnotation, constraint.constraintClass() ) );
            //            continue nextConstraint;
            //        }
            //    }

            //    // Check the annotation itself
            //    Constraints constraints = annotationType.getAnnotation( Constraints.class );
            //    if( constraints != null )
            //    {
            //        for( Class<? extends Constraint<?, ?>> constraintClass : constraints.value() )
            //        {
            //            ConstraintDeclaration declaration = new ConstraintDeclaration( constraintClass, constraintAnnotation.annotationType() );
            //            if( declaration.appliesTo( annotationType, valueType ) )
            //            {
            //                constraintModels.add( new ConstraintModel( constraintAnnotation, declaration.constraintClass() ) );
            //                continue nextConstraint;
            //            }
            //        }
            //    }

            //    // No implementation found!

            //    // Check if if it's a composite constraints
            //    if( isCompositeConstraintAnnotation( constraintAnnotation ) )
            //    {
            //        ValueConstraintsModel valueConstraintsModel = constraintsFor( constraintAnnotation.annotationType().getAnnotations(), valueType, name, optional );
            //        CompositeConstraintModel compositeConstraintModel = new CompositeConstraintModel( constraintAnnotation, valueConstraintsModel );
            //        constraintModels.add( compositeConstraintModel );
            //        continue nextConstraint;
            //    }

            //    throw new ConstraintImplementationNotFoundException( declaringType, constraintAnnotation.annotationType(), valueType );
            //}
        }

        private void AddConstraintDeclarations(Type type)
        {
            IEnumerable<ConstraintAttribute> annotations = type.GetAttributes<ConstraintAttribute>();
            foreach (ConstraintAttribute annontation in annotations)
            {
                this.constraints.Add(new ConstraintDeclaration(annontation, type));
            }
        }
    }
}