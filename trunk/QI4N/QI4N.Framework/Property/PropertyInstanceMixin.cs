namespace QI4N.Framework
{
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

        private T value;

        public T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

    }
}