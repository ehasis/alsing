namespace QI4N.Framework
{
    using System;

    public abstract class ConstraintAttribute : Attribute
    {
        public abstract bool IsValid(object value);
    }
}