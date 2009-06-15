namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class PropertyHandler : InvocationHandler
    {
        private readonly AbstractProperty property;

        public PropertyHandler(AbstractProperty property)
        {
            this.property = property;
        }

        //[DebuggerStepThrough]
        //[DebuggerHidden]
        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return method.Invoke(this.property, args);
        }
    }
}