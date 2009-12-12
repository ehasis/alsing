using GenArt.AST;

namespace GenArt.Core.Interfaces
{
    public interface IEvolutionJob
    {
        DnaDrawing GetDrawing();

        double GetNextErrorLevel();
    }
}