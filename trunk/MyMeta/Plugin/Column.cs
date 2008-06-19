
namespace MyMeta.Plugin
{
    public class PluginColumn : Column
    {
        private readonly IMyMetaPlugin plugin;

        public PluginColumn(IMyMetaPlugin plugin)
        {
            this.plugin = plugin;
        }

        public override string DataTypeName
        {
            get
            {
                var cols = Columns as PluginColumns;
                return GetString(cols.f_extTypeName);
            }
        }

        public override string DataTypeNameComplete
        {
            get
            {
                var cols = Columns as PluginColumns;
                return GetString(cols.f_extTypeNameComplete);
            }
        }

        public override object DatabaseSpecificMetaData(string key)
        {
            return plugin.GetDatabaseSpecificMetaData(this, key);
        }
    }
}