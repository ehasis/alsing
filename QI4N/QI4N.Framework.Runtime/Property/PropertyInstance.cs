namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;

    [DebuggerDisplay("Value = {Value}")]
    public class PropertyInstance<T> : Property<T>
    {
        private CompositePropertyInfo info;

        private PropertyModel model;

        public PropertyInstance(CompositePropertyInfo info, T initialValue, PropertyModel model)
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

        private T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        object AbstractProperty.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = (T)value;
            }
        }

        public T Get()
        {
            return this.Value;
        }

        public void Set(T value)
        {
            this.Value = value;
        }
    }
}