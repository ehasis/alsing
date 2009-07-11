namespace QI4N.Framework
{
    using System.Reflection;

    [AppliesTo(typeof(GetPropertyFilter))]
    public class PropertyGetterMixin : InvocationHandler
    {
        [State]
        protected StateHolder state;


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            Property property = this.state.GetProperty(method);
            object propertyValue = property.Value;
            return propertyValue;
        }
    }
}