using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IForeignKey))]
#endif
    public class FirebirdForeignKey : ForeignKey
    {
        public override ITable ForeignTable
        {
            get
            {
                string catalog = ForeignKeys.Table.Database.Name;
                string schema = GetString(ForeignKeys.f_FKTableSchema);

                return dbRoot.Databases[0].Tables[GetString(ForeignKeys.f_FKTableName)];
            }
        }
    }
}