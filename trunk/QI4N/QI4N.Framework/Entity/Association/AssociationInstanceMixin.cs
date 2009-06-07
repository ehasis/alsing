namespace QI4N.Framework
{
    public class AssociationInstanceMixin<T> : Association<T>
    {
        private T value;

        public T Get()
        {
            return this.value;
        }

        public void Set(T value)
        {
            this.value = value;
        }
    }
}