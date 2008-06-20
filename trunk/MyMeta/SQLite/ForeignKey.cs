using System.Runtime.InteropServices;

namespace MyMeta.SQLite
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IForeignKey))]
    public class SQLiteForeignKey : ForeignKey {}
}