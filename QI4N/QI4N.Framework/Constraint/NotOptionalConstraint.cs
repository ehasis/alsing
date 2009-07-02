namespace QI4N.Framework
{
    public sealed class NotOptionalConstraint : ConstraintSource
    {
        public static NotOptionalConstraint Instance = new NotOptionalConstraint();

        private NotOptionalConstraint()
        {
        }

        public string GetConstraintName()
        {
            return "Not optional";
        }
    }
}