using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Access
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(ITables))]

	public class AccessTables : Tables
	{
		public AccessTables()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				string type = this.dbRoot.ShowSystemData ? "SYSTEM TABLE" : "TABLE";
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Tables, new Object[] {null, null, null, type});

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
