using System;
using System.Collections.Generic;
using System.Text;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPartitionResult
    {
        public DnaDrawing Drawing { get; set; }
        public double ErrorLevel { get; set; }
    }

    
}
