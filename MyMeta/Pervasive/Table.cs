using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace MyMeta.Pervasive
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (ITable))]
    public class PervasiveTable : Table
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
                        string select = "SELECT Xe$Name AS COLUMN_NAME FROM X$File,X$Index,X$Field " +
                                        "WHERE Xf$Id=Xi$File and Xi$Field=Xe$Id and Xf$Name = '" + Name +
                                        "' AND Xi$Flags > 16384 " + "ORDER BY Xi$Number,Xi$Part";

                        var adapter = new OleDbDataAdapter(select, dbRoot.ConnectionString);
                        var dataTable = new DataTable();

                        adapter.Fill(dataTable);

                        int count = dataTable.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string colName = dataTable.Rows[i]["COLUMN_NAME"] as string;
                            _primaryKeys.AddColumn((Column) Columns[colName.Trim()]);
                        }
                    }
                    catch {}
                }

                return _primaryKeys;
            }
        }
    }
}