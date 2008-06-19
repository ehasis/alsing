using System;
using System.Data;

namespace MyMeta.VistaDB
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedures))]

	public class VistaDBProcedures : Procedures
	{
		public VistaDBProcedures()
		{

		}

		override internal void LoadAll()
		{
			try
			{
//				DataTable metaData = this.LoadData(OleDbSchemaGuid.Procedures, null);
//
//				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
