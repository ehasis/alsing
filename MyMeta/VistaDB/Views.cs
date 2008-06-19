using System;
using System.Data;

namespace MyMeta.VistaDB
{
	public class VistaDBViews : Views
	{

		override internal void LoadAll()
		{
			try
			{
//				string type = this.dbRoot.ShowSystemData ? "SYSTEM VIEW" : "VIEW";
//				DataTable metaData = this.LoadData(OleDbSchemaGuid.Tables, new Object[] {null, null, null, type});
//
//				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
