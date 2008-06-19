using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.DB2
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IParameter))]

	public class DB2Parameter : Parameter
	{
		public DB2Parameter()
		{

		}
	}
}
