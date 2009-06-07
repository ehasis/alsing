namespace QI4N.Framework
{
    using System.Reflection;

    public interface StateHolder
    {
        AbstractProperty GetProperty(MethodInfo propertyMethod);
    }
}