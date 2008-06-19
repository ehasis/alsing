using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Sql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IIndexes))]

	public class SqlIndexes : Indexes
	{
		public SqlIndexes()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Indexes, 
					new object[]{this.Table.Database.Name, this.Table.Schema, null, null, this.Table.Name});

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
