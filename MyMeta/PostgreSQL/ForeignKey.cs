using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.PostgreSQL
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IForeignKey))]

	public class PostgreSQLForeignKey : ForeignKey
	{
		public PostgreSQLForeignKey()
		{

		}
	}
}
