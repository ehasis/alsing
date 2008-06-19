using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.ISeries
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IResultColumns))]

	public class ISeriesResultColumns : ResultColumns
	{
		public ISeriesResultColumns()
		{

		}
	}
}
