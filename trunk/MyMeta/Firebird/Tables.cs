using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (ITables))]
#endif
    public class FirebirdTables : Tables
    {
        internal override void LoadAll()
        {
            try
            {
                string type = dbRoot.ShowSystemData ? "SYSTEM TABLE" : "TABLE";

                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData = cn.GetSchema("Tables", new[] {null, null, null, type});
                cn.Close();

                PopulateArray(metaData);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}