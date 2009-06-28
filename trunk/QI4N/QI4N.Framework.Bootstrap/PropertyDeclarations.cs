namespace QI4N.Framework.Bootstrap
{
    using System.Reflection;

    public interface PropertyDeclarations
    {
        MetaInfo GetMetaInfo(PropertyInfo accessor);

        object GetInitialValue(PropertyInfo accessor);
    }
}