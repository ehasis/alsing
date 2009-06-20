namespace QI4N.Framework.API
{
    public abstract class SideEffectOf<T>
    {
        [SideEffectFor]
        protected T next;
    }
}