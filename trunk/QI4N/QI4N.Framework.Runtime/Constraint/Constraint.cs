namespace QI4N.Framework.Runtime
{
    public class Constraint
    {
        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        //[DebuggerNonUserCode]
        public bool IsValid(ConstraintAttribute annotation, object value)
        {
            return annotation.IsValid(value);
        }
    }
}