using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Sql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDomains))]

	public class SqlDomains : Domains
	{
		public SqlDomains()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				string select = "SELECT * FROM INFORMATION_SCHEMA.DOMAINS";

				OleDbConnection cn = new OleDbConnection(dbRoot.ConnectionString);
				cn.Open();
				cn.ChangeDatabase("[" + this.Database.Name + "]");
	
				OleDbDataAdapter adapter = new OleDbDataAdapter(select, cn);
				DataTable metaData = new DataTable();

				adapter.Fill(metaData);
				cn.Close();

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
