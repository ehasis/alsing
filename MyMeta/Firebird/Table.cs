using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (ITable))]
#endif
    public class FirebirdTable : Table
    {
        public override IColumns PrimaryKeys
        {
            get
            {
                if (null == _primaryKeys)
                {
                    _primaryKeys = (Columns) dbRoot.ClassFactory.CreateColumns();
                    _primaryKeys.Table = this;
                    _primaryKeys.dbRoot = dbRoot;

                    try
                    {
                        var cn = new FbConnection(_dbRoot.ConnectionString);
                        cn.Open();
                        DataTable metaData = cn.GetSchema("PrimaryKeys", new[] {null, null, Name});
                        cn.Close();

                        string colName;
                        Column c;
                        foreach (DataRow row in metaData.Rows)
                        {
                            colName = row["COLUMN_NAME"] as string;

                            c = Columns[colName] as Column;

                            _primaryKeys.AddColumn(c);
                        }
                    }
                    catch (Exception ex)
                    {
                        string m = ex.Message;
                    }
                }

                return _primaryKeys;
            }
        }
    }
}