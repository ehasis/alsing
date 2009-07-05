namespace QI4N.Framework
{
    public interface Layer
    {
        string Name { get; }

        MetaInfo MetaInfo { get; }

        Module FindModule(string moduleName);

        Application Application { get; }
    }
}