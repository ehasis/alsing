using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.Advantage
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IDatabases))]

	public class AdvantageDatabases : Databases
	{
		public AdvantageDatabases()
		{

		}

		override internal void LoadAll()
		{
			DataTable metaData  = this.LoadData(OleDbSchemaGuid.Catalogs, null);
		
			PopulateArray(metaData);
		}
	}
}
