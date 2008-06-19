using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IForeignKeys))]
#endif
    public class FirebirdForeignKeys : ForeignKeys
    {
        internal override void LoadAll()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData1 = cn.GetSchema("ForeignKeys", new[] {null, null, Table.Name});
                DataTable metaData2 = cn.GetSchema("ForeignKeys", new[] {null, null, null, null, null, Table.Name});
                cn.Close();

                DataRowCollection rows = metaData2.Rows;
                int count = rows.Count;
                for (int i = 0; i < count; i++)
                {
                    metaData1.ImportRow(rows[i]);
                }

                PopulateArrayNoHookup(metaData1);

                ForeignKey key = null;
                string keyName = "";

                foreach (DataRow row in metaData1.Rows)
                {
                    keyName = row["FK_NAME"] as string;

                    key = GetByName(keyName);

                    key.AddForeignColumn(null, null, (string) row["PK_TABLE_NAME"], (string) row["PK_COLUMN_NAME"], true);
                    key.AddForeignColumn(null, null, (string) row["FK_TABLE_NAME"], (string) row["FK_COLUMN_NAME"],
                                         false);
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}