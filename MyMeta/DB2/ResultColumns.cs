using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.DB2
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IResultColumns))]

	public class DB2ResultColumns : ResultColumns
	{
		public DB2ResultColumns()
		{

		}
	}
}
