using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;

        public DefaultEvolutionJob(SourceImage sourceImage)
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

            while (newDrawing.IsDirty == false)
                newDrawing.Mutate();

            double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);

            if (newErrorLevel <= currentErrorLevel)
            {
                currentDrawing = newDrawing;
                currentErrorLevel = newErrorLevel;
            }

            return newErrorLevel;

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