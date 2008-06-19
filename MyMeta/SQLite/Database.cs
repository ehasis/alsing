using System;
using System.Data;
using System.Data.OleDb;

using System.Data.SQLite;
using ADODB;

namespace MyMeta.SQLite
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDatabase))]

	public class SQLiteDatabase : Database
	{
		internal string _name = "";

		public SQLiteDatabase()
		{

		}

		override public string Name
		{
			get
			{
				return _name;
			}
		}

		override public string Alias
		{
			get
			{
				return _name;
			}
		}

		override public ADODB.Recordset ExecuteSql(string sql)
		{
			SQLiteConnection cn = ConnectionHelper.CreateConnection(dbRoot);

			return this.ExecuteIntoRecordset(sql, cn);
		}
	}
}
