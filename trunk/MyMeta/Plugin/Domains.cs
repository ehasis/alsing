using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Plugin
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDomains))]

	public class PluginDomains : Domains
	{
        private IMyMetaPlugin plugin;

        public PluginDomains(IMyMetaPlugin plugin)
        {
            this.plugin = plugin;
		}

		override internal void LoadAll()
        {
            DataTable metaData = this.plugin.GetDomains(this.Database.Name);
            PopulateArray(metaData);
		}
	}
}
