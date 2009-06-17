namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface PropertyDeclarations
    {
        MetaInfo GetMetaInfo(MethodInfo accessor);

        object GetInitialValue(MethodInfo accessor);
    }
}