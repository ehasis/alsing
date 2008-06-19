using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Pervasive
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IViews))]

	public class PervasiveViews : Views
	{
		public PervasiveViews()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				string type = this.dbRoot.ShowSystemData ? "SYSTEM VIEW" : "VIEW";
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Views, new Object[] {this.Database.Name, null, null, "TABLE"}); //type});

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
