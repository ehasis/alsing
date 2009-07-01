namespace QI4N.Framework.Runtime
{
    public class ConstraintInstance
    {
        private readonly Constraint constraint;

        public ConstraintInstance(Constraint constraint, ConstraintAttribute annotation)
        {
            this.constraint = constraint;
            this.Annotation = annotation;
        }

        public ConstraintAttribute Annotation { get; private set; }


        public bool IsValid(object value)
        {
            return this.constraint.IsValid(this.Annotation, value);
        }
    }
}