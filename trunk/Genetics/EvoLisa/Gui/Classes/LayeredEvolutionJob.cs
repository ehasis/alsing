using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class LayeredEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;

        public LayeredEvolutionJob(SourceImage sourceImage)
        {
            currentDrawing = GetNewInitializedDrawing();
            currentDrawing.SourceImage = sourceImage;
            currentErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, currentDrawing.SourceImage);
        }

        #region IEvolutionJob Members

        public bool IsDirty { get; set; }

        public DnaDrawing GetDrawing()
        {
            return currentDrawing;
        }

        public double GetNextErrorLevel()
        {
            DnaDrawing newDrawing = currentDrawing.Clone();

            newDrawing.Mutate();

            //TODO: Why not loop until we get a mutation - that way we don't waste lots of clones ^^
            if (newDrawing.IsDirty)
            {
                double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);

                if (newErrorLevel <= currentErrorLevel)
                {
                    currentDrawing = newDrawing;
                    currentErrorLevel = newErrorLevel;
                }

                IsDirty = true;
                return newErrorLevel;
            }

            IsDirty = false;
            return currentErrorLevel;
        }

        #endregion

        private static DnaDrawing GetNewInitializedDrawing()
        {
            var drawing = new DnaDrawing();
            drawing.Init();
            return drawing;
        }
    }
}