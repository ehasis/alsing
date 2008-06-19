using System;
using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
#if ENTERPRISE

    ///<summary>
    ///</summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IColumn))]
#endif
    public class FirebirdColumn : Column
    {
        public override Boolean IsAutoKey
        {
            get
            {
                if (null != Table)
                {
                    if (Table.Properties.ContainsKey("GEN:I:" + Name) || Table.Properties.ContainsKey("GEN:I:T:" + Name))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override Int32 CharacterMaxLength
        {
            get
            {
                switch (DataTypeName)
                {
                    case "VARCHAR":
                    case "CHAR":
//						return (int)this._row["COLUMN_SIZE"];
                        return CharacterOctetLength;

                    default:
                        return GetInt32(Columns.f_MaxLength);
                }
            }
        }

        public override Int32 NumericPrecision
        {
            get
            {
                if (DataTypeName == "NUMERIC")
                {
                    switch ((int) _row["COLUMN_SIZE"])
                    {
                        case 2:
                            return 4;
                        case 4:
                            return 9;
                        case 8:
                            return 15;
                        default:
                            return 18;
                    }
                }
                return GetInt32(Columns.f_NumericScale);
            }
        }

        public override string DataTypeName
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.DataTypeName;
                        }
                    }
                }

                var cols = Columns as FirebirdColumns;
                return GetString(cols.f_TypeName);
            }
        }

        public override string DataTypeNameComplete
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.DataTypeNameComplete;
                        }
                    }
                }

                var cols = Columns as FirebirdColumns;
                return GetString(cols.f_TypeNameComplete);
            }
        }

        internal override Column Clone()
        {
            Column c = base.Clone();

            return c;
        }
    }
}