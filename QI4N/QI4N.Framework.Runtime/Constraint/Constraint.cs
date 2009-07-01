namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    public class Constraint
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        public bool IsValid(ConstraintAttribute annotation, object value)
        {
            return annotation.IsValid(value);
        }
    }
}