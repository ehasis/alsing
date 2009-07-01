namespace QI4N.Framework.Runtime
{
    using System;

    public class ConstraintDeclaration
    {
        private readonly ConstraintAttribute annontation;

        private readonly Type declaringType;

        public ConstraintDeclaration(ConstraintAttribute annontation, Type declaringType)
        {
            this.annontation = annontation;
            this.declaringType = declaringType;
        }
    }
}