namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    [DebuggerDisplay("Value = {Value}")]
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

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
}