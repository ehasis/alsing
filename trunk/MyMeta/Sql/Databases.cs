using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Sql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDatabases))]

	public class SqlDatabases : Databases
	{
		public SqlDatabases()
		{

		}

		override internal void LoadAll()
		{
			DataTable metaData  = this.LoadData(OleDbSchemaGuid.Catalogs, null);
		
			PopulateArray(metaData);
		}
	}
}
