namespace QI4N.Framework.Runtime
{
    //[DebuggerDisplay("Value = {Value}")]
    public class PropertyInstance : Property
    {
        private readonly PropertyModel model;

        private CompositePropertyInfo info;


        public PropertyInstance(CompositePropertyInfo info, object initialValue, PropertyModel model)
        {
            this.info = info;
            this.Value = initialValue;
            this.model = model;
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
                return false;
            }
        }

        public string QualifiedName
        {
            get
            {
                return this.model.QualifiedName;
            }
        }

        public object Value { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PropertyInstance);
        }


        public bool Equals(PropertyInstance obj)
        {
            return Equals(obj.Value, this.Value);
        }

        public override int GetHashCode()
        {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Value);
        }
    }
}