namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    public class ConstraintInstance
    {
        private readonly Constraint constraint;

        public ConstraintInstance(Constraint constraint, ConstraintAttribute annotation)
        {
            this.constraint = constraint;
            this.Annotation = annotation;
        }

        public ConstraintAttribute Annotation { get; private set; }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public bool IsValid(object value)
        {
            return this.constraint.IsValid(this.Annotation, value);
        }
    }
}