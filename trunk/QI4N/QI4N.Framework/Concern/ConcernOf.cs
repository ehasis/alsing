namespace QI4N.Framework
{
    //Marker
    public abstract class ConcernOf
    {
        
    }

    public abstract class ConcernOf<T> : ConcernOf
    {
        [ConcernFor]
        protected T next;
    }
}