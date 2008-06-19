using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Pervasive
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IProcedures))]

	public class PervasiveProcedures : Procedures
	{
		public PervasiveProcedures()
		{

		}

		override internal void LoadAll()
		{
			try
			{
				DataTable metaData = this.LoadData(OleDbSchemaGuid.Procedures, null);

				PopulateArray(metaData);
			}
			catch {}
		}
	}
}
