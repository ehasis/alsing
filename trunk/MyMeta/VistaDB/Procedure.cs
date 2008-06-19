using System;
using System.Data;

namespace MyMeta.VistaDB
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedure))]

	public class VistaDBProcedure : Procedure
	{
		public VistaDBProcedure()
		{

		}
	}
}
