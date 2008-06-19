using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.SQLite
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IView))]

	public class SQLiteView : View
	{
		public SQLiteView()
		{

		}
	}
}
