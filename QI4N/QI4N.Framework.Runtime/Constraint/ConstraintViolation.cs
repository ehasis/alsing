namespace QI4N.Framework.Runtime
{
    public class ConstraintViolation
    {
        public ConstraintViolation(string name, ConstraintAttribute constraint, object value)
        {
            this.Name = name;
            this.Constraint = constraint;
            this.Value = value;
        }

        public ConstraintAttribute Constraint { get; private set; }

        public string Name { get; private set; }

        public object Value { get; private set; }
    }
}