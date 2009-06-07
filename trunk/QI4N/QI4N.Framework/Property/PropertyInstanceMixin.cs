namespace QI4N.Framework
{
    using System.Diagnostics;

    [DebuggerDisplay("Value = {Value}")]
    public class PropertyInstanceMixin<T> : Property<T>
    {
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

        object AbstractProperty.Get()
        {
            return this.Value;
        }

        void AbstractProperty.Set(object value)
        {
            this.Value = (T)value;
        }
    }
}