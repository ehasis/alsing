namespace QI4N.Framework
{
    public abstract class SideEffectOf
    {
    }
    public abstract class SideEffectOf<T> : SideEffectOf
    {
        [SideEffectFor]
        protected T next;
    }
}