using System;
using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
    public class FirebirdDomain : Domain
    {
        public override string DataTypeNameComplete
        {
            get
            {
                var domains = Domains as FirebirdDomains;
                return GetString(domains.f_TypeNameComplete);
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
                        return (int) _row["DOMAIN_SIZE"];

                    default:
                        return GetInt32(Domains.f_MaxLength);
                }
            }
        }

        public override Int32 NumericPrecision
        {
            get
            {
                switch (DataTypeName)
                {
                    case "VARCHAR":
                    case "CHAR":
                        return 0;

                    default:
                        return base.NumericPrecision;
                }
            }
        }
    }
}