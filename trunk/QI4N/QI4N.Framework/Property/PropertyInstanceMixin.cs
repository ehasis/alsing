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

        #region Property<T> Members


        public void Set(T value)
        {
            Value = value;
        }

        public T Get()
        {
            return Value;
        }

        #endregion
    }
}