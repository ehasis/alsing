using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Sql
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IForeignKey))]

	public class SqlForeignKey : ForeignKey
	{
		public SqlForeignKey()
		{

		}
	}
}
