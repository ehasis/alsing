using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Advantage
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDatabase))]

	public class AdvantageDatabase : Database
	{
		public AdvantageDatabase()
		{

		}
	}
}
