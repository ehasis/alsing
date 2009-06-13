namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    [DebuggerDisplay("Value = {Value}")]
    public class PropertyInstance<T> : Property<T>
    {
        private PropertyInfo<T> info;

        private PropertyModel model;

        public PropertyInstance(PropertyInfo<T> info,T initialValue,PropertyModel model)
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

        public T Value { get; set; }

        public T Get()
        {
            return this.Value;
        }

        public void Set(T value)
        {
            this.Value = value;
        }


        object AbstractProperty.Value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = (T)value;
            }
        }
    }
}