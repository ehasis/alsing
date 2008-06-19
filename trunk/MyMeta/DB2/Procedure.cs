using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.DB2
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedure))]

	public class DB2Procedure : Procedure
	{
		public DB2Procedure()
		{

		}
	}
}
