using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
    public class FirebirdViews : Views
    {
        internal override void LoadAll()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData = cn.GetSchema("Views", new string[] {null, null, null});
                cn.Close();

                metaData.Columns["VIEW_NAME"].ColumnName = "TABLE_NAME";

                PopulateArray(metaData);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}