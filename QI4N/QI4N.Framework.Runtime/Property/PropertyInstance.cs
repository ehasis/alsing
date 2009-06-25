namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    [DebuggerDisplay("Value = {Value}")]
    public class PropertyInstance: Property
    {
        private CompositePropertyInfo info;

        private PropertyModel model;

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

        public bool IsMutable
        {
            get
            {
                return true;
            }
        }

        public object Value { get; set; }        
    }
}