using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IResultColumns))]
#endif
    public class FirebirdResultColumns : ResultColumns
    {
        internal override void LoadAll()
        {
            try
            {
                //DataTable metaData = this.LoadData(OleDbSchemaGuid.Procedure_Columns, 
                //	new object[]{null, null, this.Procedure.Name, null});

                //PopulateArray(metaData);
            }
            catch {}
        }
    }
}