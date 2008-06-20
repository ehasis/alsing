using System.Runtime.InteropServices;

namespace MyMeta.SQLite
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IResultColumn))]
    public class SQLiteResultColumn : ResultColumn {}
}