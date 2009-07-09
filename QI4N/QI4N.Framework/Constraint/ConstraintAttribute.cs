namespace QI4N.Framework
{
    using System;
    using System.Diagnostics;

    public abstract class ConstraintAttribute : Attribute, ConstraintSource
    {
        public abstract string GetConstraintName();

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public abstract bool IsValid(object value);
    }
}