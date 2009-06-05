namespace QI4N.Framework
{
    public class EntityBuilderMixin<T> : EntityBuilder<T>
    {
        private T stateFor;

        public T NewInstance()
        {
            T instance = default(T);
            return instance;
        }

        public T StateFor()
        {
            this.stateFor = default(T);
            return this.stateFor;
        }
    }
}