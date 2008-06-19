using System.Runtime.InteropServices;
using ADODB;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IDatabase))]
#endif
    public class FirebirdDatabase : Database
    {
        internal string _desc = "";
        internal string _name = "";

        public override string Name
        {
            get { return _name; }
        }

        public override string Alias
        {
            get { return _name; }
        }

        public override string Description
        {
            get { return _desc; }
        }

        public override Recordset ExecuteSql(string sql)
        {
            var cn = new FbConnection(dbRoot.ConnectionString);
            cn.Open();
            //cn.ChangeDatabase(this.Name);

            return ExecuteIntoRecordset(sql, cn);
        }
    }
}