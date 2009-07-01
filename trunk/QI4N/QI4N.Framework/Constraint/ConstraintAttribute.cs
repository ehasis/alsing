namespace QI4N.Framework
{
    using System;
    using System.Diagnostics;

    public abstract class ConstraintAttribute : Attribute
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        public abstract bool IsValid(object value);
    }
}