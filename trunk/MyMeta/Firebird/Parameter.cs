using System;
using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IParameter))]
#endif
    public class FirebirdParameter : Parameter
    {
        public override string DataTypeNameComplete
        {
            get
            {
                var parameters = Parameters as FirebirdParameters;
                return GetString(parameters.f_TypeNameComplete);
            }
        }


        public override Int32 CharacterMaxLength
        {
            get
            {
                switch (TypeName)
                {
                    case "VARCHAR":
                    case "CHAR":
                        return (Int32) _row["PARAMETER_SIZE"];

                    default:
                        return GetInt32(Parameters.f_CharMaxLength);
                }
            }
        }

        public override Int32 CharacterOctetLength
        {
            get { return (Int32) _row["PARAMETER_SIZE"]; }
        }

        public override Int32 NumericPrecision
        {
            get
            {
                if (TypeName == "NUMERIC")
                {
                    switch ((int) _row["PARAMETER_SIZE"])
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
                return GetInt32(Parameters.f_NumericScale);
            }
        }
    }
}