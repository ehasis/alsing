using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.DB2
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IIndex))]

	public class DB2Index : Index
	{
		public DB2Index()
		{

		}
	}
}
