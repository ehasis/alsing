using System;
using System.Runtime.InteropServices;

namespace MyMeta.Pervasive
{

    public class PervasiveColumn : Column
    {
        public override Boolean IsInPrimaryKey
        {
            get
            {
                if (null == Columns.Table) return false;

                IColumn col = Columns.Table.PrimaryKeys[Name];

                return (null == col) ? false : true;
            }
        }

        public override Boolean IsAutoKey
        {
            get
            {
                var cols = Columns as PervasiveColumns;
                return GetBool(cols.f_AutoKey);
            }
        }

        public override Int32 AutoKeyIncrement
        {
            get
            {
                var cols = Columns as PervasiveColumns;
                return GetInt32(cols.f_AutoKeyIncrement);
            }
        }

        public override Int32 AutoKeySeed
        {
            get
            {
                var cols = Columns as PervasiveColumns;
                return GetInt32(cols.f_AutoKeySeed);
            }
        }

        public override string DataTypeName
        {
            get
            {
                var cols = Columns as PervasiveColumns;
                string type = GetString(cols.f_TypeName);

                switch (type)
                {
                    case "adWChar":
                    case "adVarWChar":
                        return "Text";
                    case "adLongVarWChar":
                        return "Memo";
                    case "adUnsignedTinyInt":
                        return "Byte";
                    case "adCurrency":
                        return "Currency";
                    case "adDate":
                        return "DateTime";
                    case "adBoolean":
                        return @"Yes/No";
                    case "adLongVarBinary":
                        return "OLE Object";
                    case "adInteger":
                        return "Long";
                    case "adDouble":
                        return "Double";
                    case "adGUID":
                        return "Replication ID";
                    case "adSingle":
                        return "Single";
                    case "adNumeric":
                        return "Decimal";
                    case "adSmallInt":
                        return "Integer";
                    case "adVarBinary":
                        return "Binary";
                    case "Hyperlink":
                        return "Hyperlink";
                    default:
                        return type;
                }
            }
        }

        public override string DataTypeNameComplete
        {
            get
            {
                var cols = Columns as PervasiveColumns;
                string type = GetString(cols.f_TypeName);

                switch (type)
                {
                    case "adWChar":
                    case "adVarWChar":
                        return "Text";
                    case "adLongVarWChar":
                        return "Memo";
                    case "adUnsignedTinyInt":
                        return "Byte";
                    case "adCurrency":
                        return "Currency";
                    case "adDate":
                        return "DateTime";
                    case "adBoolean":
                        //return @"Yes/No";
                        return "Bit";
                    case "adLongVarBinary":
                        //return "OLE Object";
                        return "LongBinary";
                    case "adInteger":
                        return "Long";
                    case "adDouble":
                        return "IEEEDouble";
                    case "adGUID":
                        //return "Replication ID";
                        return "Guid";
                    case "adSingle":
                        return "IEEESingle";
                    case "adNumeric":
                        return "Decimal";
                    case "adSmallInt":
                        return "Integer";
                    case "adVarBinary":
                        return "Binary";
                    case "Hyperlink":
                        return "Text (255)";
                    default:
                        return type;
                }
            }
        }

        internal override Column Clone()
        {
            Column c = base.Clone();

            return c;
        }
    }
}