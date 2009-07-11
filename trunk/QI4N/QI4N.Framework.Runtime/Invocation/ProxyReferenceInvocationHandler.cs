namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ProxyReferenceInvocationHandler : InvocationHandler
    {
        public object Proxy { get; set; }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public void ClearProxy()
        {
            this.Proxy = null;
        }


        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            try
            {
                InvocationHandler invocationHandler = JavaProxy.Proxy.GetInvocationHandler(this.Proxy);
                return invocationHandler.Invoke(this.Proxy, method, args);
            }
            catch (InvocationTargetException e)
            {
                throw e.TargetException;
            }
            catch (UndeclaredThrowableException e)
            {
                throw e.UndeclaredThrowable;
            }
        }
    }

    internal class UndeclaredThrowableException : Exception
    {
        public Exception UndeclaredThrowable;
    }
}