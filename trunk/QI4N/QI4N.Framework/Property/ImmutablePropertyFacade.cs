namespace QI4N.Framework
{
    using System;
    using System.Data;

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
                throw new NotImplementedException();
            }
        }

        public object Value
        {
            get
            {
                return this.target.Value;
            }
            set
            {
                string message = string.Format("Property {0} is immutable", this.QualifiedName);
                throw new ReadOnlyException(message);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} r/o", this.target);
        }
    }
}