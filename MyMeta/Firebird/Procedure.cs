using System.Runtime.InteropServices;

namespace MyMeta.Firebird
{
    public class FirebirdProcedure : Procedure
    {
        public override string Alias
        {
            get
            {
                string[] name = base.Name.Split(';');

                return name[0];
            }
        }

        public override string Name
        {
            get
            {
                string[] name = base.Name.Split(';');

                return name[0];
            }
        }
    }
}