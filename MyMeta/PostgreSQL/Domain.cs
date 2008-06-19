using System;
using System.Data;
using Npgsql;

namespace MyMeta.PostgreSQL
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDomain))]

	public class PostgreSQLDomain : Domain
	{
		public PostgreSQLDomain()
		{

		}
	}
}
