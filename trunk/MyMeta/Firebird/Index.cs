using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
#if ENTERPRISE
    
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), ComDefaultInterface(typeof (IIndex))]
#endif
    public class FirebirdIndex : Index {}
}