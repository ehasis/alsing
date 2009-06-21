namespace QI4N.Framework
{
    using System.Reflection;

    public abstract class GenericSideEffect : SideEffectOf<InvocationHandler>, InvocationHandler
    {
        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            this.Invoke(method, args);
            return null;
        }

        protected abstract void Invoke(MethodInfo method, object[] args);
    }
}