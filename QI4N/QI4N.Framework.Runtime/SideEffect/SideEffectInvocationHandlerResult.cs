namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class SideEffectInvocationHandlerResult : InvocationHandler
    {
        private Exception exception;

        private object result;

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            if (this.exception != null)
            {
                throw this.exception;
            }
            return this.result;
        }

        public void SetResult(object result, Exception exception)
        {
            this.result = result;
            this.exception = exception;
        }
    }
}