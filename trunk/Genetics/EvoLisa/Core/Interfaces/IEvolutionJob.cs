using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GenArt.AST;
using GenArt.Core.Classes;

namespace GenArt.Core.Interfaces
{
    public interface IEvolutionJob
    {
        DnaDrawing GetDrawing();

        double GetNextErrorLevel();
        bool IsDirty { get; }
    }
}
