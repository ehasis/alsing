namespace QI4N.Framework.Bootstrap
{
    using System.Reflection;

    public interface PropertyDeclarations
    {
        MetaInfo GetMetaInfo(MethodInfo accessor);

        object GetInitialValue(MethodInfo accessor);
    }
}