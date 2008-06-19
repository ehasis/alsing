using System;
using System.Data;
using System.Data.OleDb;

namespace MyMeta.ISeries
{

	using System.Runtime.InteropServices;
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof(IIndex))]

	public class ISeriesIndex : Index
	{
		public ISeriesIndex()
		{

		}
	}
}
