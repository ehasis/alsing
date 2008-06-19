using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Plugin
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDomain))]

	public class PluginDomain : Domain	
	{
        private IMyMetaPlugin plugin;

        public PluginDomain(IMyMetaPlugin plugin)
        {
            this.plugin = plugin;
        }

        public override object DatabaseSpecificMetaData(string key)
        {
            return this.plugin.GetDatabaseSpecificMetaData(this, key);
        }
	}
}
