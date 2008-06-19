using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Plugin
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IForeignKey))]

	public class PluginForeignKey : ForeignKey
	{
        private IMyMetaPlugin plugin;

        public PluginForeignKey(IMyMetaPlugin plugin)
        {
            this.plugin = plugin;
        }

        public override object DatabaseSpecificMetaData(string key)
        {
            return this.plugin.GetDatabaseSpecificMetaData(this, key);
        }
	}
}
