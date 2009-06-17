namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class MethodConcernsInstance : InvocationHandler
    {
        private readonly InvocationHandler firstConcern;

        private readonly FragmentInvocationHandler mixinInvocationHandler;

        private readonly ProxyReferenceInvocationHandler proxyHandler;

        public MethodConcernsInstance(InvocationHandler firstConcern, FragmentInvocationHandler mixinInvocationHandler, ProxyReferenceInvocationHandler proxyHandler)
        {
            this.firstConcern = firstConcern;
            this.mixinInvocationHandler = mixinInvocationHandler;
            this.proxyHandler = proxyHandler;
        }

        public bool IsEmpty
        {
            get
            {
                return this.firstConcern == this.mixinInvocationHandler;
            }
        }

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            this.proxyHandler.SetProxy(proxy);
            try
            {
                return this.firstConcern.Invoke(proxy, method, args);
            }
            catch (InvocationTargetException e)
            {
                throw e.TargetException;
            }
            finally
            {
                this.proxyHandler.ClearProxy();
            }
        }
    }

    internal class InvocationTargetException : Exception
    {
        public Exception TargetException
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}