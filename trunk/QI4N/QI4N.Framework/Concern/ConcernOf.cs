namespace QI4N.Framework
{
    public abstract class ConcernOf<T>
    {
        [ConcernFor]
        protected T next;
    }
}