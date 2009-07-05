namespace QI4N.Framework
{
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
                return this.target.QualifiedName;
            }
        }

        public object Value
        {
            [DebuggerStepThrough]
            //[DebuggerHidden]
            get
            {
                return this.target.Value;
            }
            [DebuggerStepThrough]
            //[DebuggerHidden]
            set
            {
                string message = string.Format("Property '{0}' is immutable", this.QualifiedName);
                throw new ReadOnlyException(message);
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ImmutablePropertyFacade);
        }

        public bool Equals(ImmutablePropertyFacade that)
        {
            return Equals(this.Value, that.Value);
        }

        public override int GetHashCode()
        {
            return this.target.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} r/o", this.target);
        }
    }
}