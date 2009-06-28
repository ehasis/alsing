namespace QI4N.Framework
{
    using System;
    using System.Data;
    using System.Diagnostics;

    public class ImmutablePropertyFacade : Property
    {
        private readonly Property target;

        public ImmutablePropertyFacade(Property target)
        {
            this.target = target;
        }

        public bool IsComputed
        {
            get
            {
                return false;
            }
        }

        public bool IsImmutable
        {
            get
            {
                return true;
            }
        }

        public string QualifiedName
        {
            get
            {
                return target.QualifiedName;
            }
        }

        public object Value
        {
            [DebuggerStepThrough]
            [DebuggerHidden]
            get
            {
                return this.target.Value;
            }
            [DebuggerStepThrough]
            [DebuggerHidden]
            set
            {
                string message = string.Format("Property '{0}' is immutable", this.QualifiedName);
                throw new ReadOnlyException(message);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} r/o", this.target);
        }
    }
}