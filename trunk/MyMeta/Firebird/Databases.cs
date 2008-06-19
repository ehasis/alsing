using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IDatabases))]
#endif
    public class FirebirdDatabases : Databases
    {
        internal override void LoadAll()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                string dbName = cn.Database;
                cn.Close();

                int index = dbName.LastIndexOfAny(new[] {'\\'});

                if (index >= 0)
                {
                    dbName = dbName.Substring(index + 1);
                }

                // We add our one and only Database
                var database = (FirebirdDatabase) dbRoot.ClassFactory.CreateDatabase();
                database._name = dbName;
                database.dbRoot = dbRoot;
                database.Databases = this;
                _array.Add(database);
            }
            catch {}
        }
    }
}