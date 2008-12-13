using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GenArt.AST;

namespace GenArt.Core.Interfaces
{
    public interface IEvolutionJob
    {
        DnaDrawing GetBestDrawing();
        Color[,] SourceColors { get; set; }
    }
}
