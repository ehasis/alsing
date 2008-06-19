using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
    public class FirebirdProcedures : Procedures
    {
        public override IProcedure this[object name]
        {
            get { return base[name]; }
        }

        internal override void LoadAll()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData = cn.GetSchema("Procedures", new[] {Database.Name});
                cn.Close();

                PopulateArray(metaData);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}