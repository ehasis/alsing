namespace QI4N.Framework
{
    public abstract class SideEffectOf<T>
    {
        [SideEffectFor]
        protected T next;
    }
}