using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
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