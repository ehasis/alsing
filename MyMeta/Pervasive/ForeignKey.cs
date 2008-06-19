using System.Runtime.InteropServices;

namespace MyMeta.Pervasive
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IForeignKey))]
    public class PervasiveForeignKey : ForeignKey
    {
        public override ITable ForeignTable
        {
            get
            {
                string catalog = ForeignKeys.Table.Database.Name;
                string schema = GetString(ForeignKeys.f_FKTableSchema);

                return dbRoot.Databases[0].Tables[GetString(ForeignKeys.f_FKTableName)];
            }
        }

        internal override void AddForeignColumn(string catalog, string schema, string physicalTableName,
                                                string physicalColumnName, bool primary)
        {
            var column = ForeignKeys.Table.Tables[physicalTableName].Columns[physicalColumnName] as Column;
            Column c = column.Clone();

            if (primary)
            {
                if (null == _primaryColumns)
                {
                    _primaryColumns = (Columns) dbRoot.ClassFactory.CreateColumns();
                    _primaryColumns.ForeignKey = this;
                }

                _primaryColumns.AddColumn(c);
            }
            else
            {
                if (null == _foreignColumns)
                {
                    _foreignColumns = (Columns) dbRoot.ClassFactory.CreateColumns();
                    _foreignColumns.ForeignKey = this;
                }

                _foreignColumns.AddColumn(c);
            }

            column.AddForeignKey(this);
        }
    }
}