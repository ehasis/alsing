namespace QI4N.Framework.Runtime
{
    public class Constraint
    {
        public bool IsValid(ConstraintAttribute annotation, object value)
        {
            return annotation.IsValid(value);
        }
    }
}