using System;
using GenArt.AST;

namespace MpiLisa.DataContracts
{
    [Serializable]
    internal struct MpiWorkerDrawingInfo
    {
        internal DnaDrawing Drawing { get; set; }
    }
}