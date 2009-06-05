namespace QI4N.Framework
{
    using System.Reflection;

    public interface EntityStateHolder : StateHolder
    {
        AbstractAssociation GetAssociation(MethodInfo associationMethod);
    }
}