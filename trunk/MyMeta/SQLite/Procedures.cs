using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.SQLite
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedures))]

	public class SQLiteProcedures : Procedures
	{
		public SQLiteProcedures()
		{

		}
	}
}
