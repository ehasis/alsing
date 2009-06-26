namespace QI4N.Framework
{
    using System.Reflection;

    [AppliesTo(typeof(SetPropertyFilter))]
    public class PropertySetterMixin : InvocationHandler
    {
        [State]
        protected StateHolder state;

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            object value = args[0];
            Property property = this.state.GetProperty(method);

            property.Value = value;
            return null;
        }
    }
}