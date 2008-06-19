using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.MySql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedures))]

	public class MySqlProcedures : Procedures
	{
		public MySqlProcedures()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Procedures, 
					new Object[] {this.Database.Name, null, null});

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
