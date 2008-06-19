using System;
using System.Data;
using System.Data.SQLite;

namespace MyMeta.SQLite
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDomains))]

	public class SQLiteDomains : Domains
	{
		public SQLiteDomains()
		{

		}
	}
}
