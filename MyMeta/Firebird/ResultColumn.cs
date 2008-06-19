using System;
using System.Data;
using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
    public class FirebirdResultColumn : ResultColumn
    {
        #region Properties

        public override string Name
        {
            get { return _column.ColumnName; }
        }

        public override string DataTypeName
        {
            get { return _column.DataType.ToString(); }
        }

        public override Int32 Ordinal
        {
            get { return _column.Ordinal; }
        }

        #endregion

        internal DataColumn _column;
    }
}