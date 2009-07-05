namespace QI4N.Framework
{
    public interface Application
    {
        string Name { get; }

        ApplicationMode Mode { get; }

        MetaInfo MetaInfo { get; }

        Layer FindLayer(string layerName);

        Module FindModule(string layerName, string moduleName);
    }

    public enum ApplicationMode
    {
        // Application modes
        Test,
        Development,
        Production
    }
}