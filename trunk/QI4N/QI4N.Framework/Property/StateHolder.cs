namespace QI4N.Framework
{
    using System.Reflection;

    public interface StateHolder
    {
        Property GetProperty(MethodInfo propertyMethod);
    }
}