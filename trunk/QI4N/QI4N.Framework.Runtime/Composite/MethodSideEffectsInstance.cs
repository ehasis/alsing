namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodSideEffectsInstance : InvocationHandler
    {
        private readonly InvocationHandler invoker;

        private readonly ProxyReferenceInvocationHandler proxyHandler;

        private readonly SideEffectInvocationHandlerResult resultInvocationHandler;

        private readonly List<InvocationHandler> sideEffects;

        public MethodSideEffectsInstance(List<InvocationHandler> sideEffects, SideEffectInvocationHandlerResult resultInvocationHandler, ProxyReferenceInvocationHandler proxyHandler, InvocationHandler invoker)
        {
            this.sideEffects = sideEffects;
            this.resultInvocationHandler = resultInvocationHandler;
            this.proxyHandler = proxyHandler;
            this.invoker = invoker;
        }

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            try
            {
                object result = this.invoker.Invoke(proxy, method, args);
                this.InvokeSideEffects(proxy, method, args, result, null);
                return result;
            }
            catch (Exception exception)
            {
                this.InvokeSideEffects(proxy, method, args, null, exception);
                throw;
            }
        }

        private static void InvokeSideEffect(object proxy, MethodInfo method, object[] args, Exception originalException, InvocationHandler sideEffect)
        {
            try
            {
                sideEffect.Invoke(proxy, method, args);
            }
            catch (Exception exception)
            {
                if (exception != originalException)
                {
                    //exception.printStackTrace();
                }
            }
        }

        private void InvokeSideEffects(object proxy, MethodInfo method, object[] args, object result, Exception exception)
        {
            this.proxyHandler.Proxy = proxy;
            this.resultInvocationHandler.SetResult(result, exception);

            try
            {
                foreach (InvocationHandler sideEffect in this.sideEffects)
                {
                    InvokeSideEffect(proxy, method, args, exception, sideEffect);
                }
            }
            finally
            {
                this.proxyHandler.ClearProxy();
                this.resultInvocationHandler.SetResult(null, null);
            }
        }
    }
}