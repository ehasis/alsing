using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace MyMeta.SQLite
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IDatabases))]
    public class SQLiteDatabases : Databases
    {
        internal override void LoadAll()
        {
            try
            {
                var cn = new SQLiteConnection(dbRoot.ConnectionString);

                // We add our one and only Database
                var database = (SQLiteDatabase) dbRoot.ClassFactory.CreateDatabase();
                database.dbRoot = dbRoot;
                database.Databases = this;
                database._name = (cn.Database.Length == 0 ? "main" : cn.Database);
                _array.Add(database);
            }
            catch {}
        }
    }
}