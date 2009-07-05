namespace QI4N.Framework
{
    using System;

    public abstract class InjectionAttribute : Attribute
    {
        public virtual bool IsOptional()
        {
            return false;
        }
    }
}