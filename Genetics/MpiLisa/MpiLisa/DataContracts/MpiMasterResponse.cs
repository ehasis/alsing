using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpiLisa.DataContracts
{
    [Serializable]
    public class MpiMasterResponse
    {
        public bool Accepted { get; set; }
        public int NewRandomSeed { get; set; }
    }
}
