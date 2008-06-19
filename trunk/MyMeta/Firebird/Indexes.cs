using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IIndexes))]
#endif
    public class FirebirdIndexes : Indexes
    {
        internal override void LoadAll()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData = cn.GetSchema("Indexes", new[] {null, null, Table.Name});
                cn.Close();

                metaData.Columns["IS_UNIQUE"].ColumnName = "UNIQUE";
                metaData.Columns["INDEX_TYPE"].ColumnName = "TYPE";
                metaData.Columns["ORDINAL_POSITION"].ColumnName = "CARDINALITY";
                PopulateArray(metaData);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}