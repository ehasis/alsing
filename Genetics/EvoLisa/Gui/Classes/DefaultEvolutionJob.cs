using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;

        public DefaultEvolutionJob(SourceImage sourceImage) : this(sourceImage, null)
        {
        }

        public DefaultEvolutionJob(SourceImage sourceImage, DnaDrawing drawing)
        {
            if (drawing == null)
                drawing = GetNewInitializedDrawing();
            lock (drawing)
            {
                currentDrawing = drawing.Clone();
            }
            currentDrawing.SourceImage = sourceImage;
            currentErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, currentDrawing.SourceImage);
        }

        #region IEvolutionJob Members

        public int Generations { get; set; }

        public DnaDrawing GetDrawing()
        {
            return currentDrawing;
        }

        public double GetNextErrorLevel()
        {
            Generations = 0;
            DnaDrawing newDrawing = currentDrawing.Clone();


            newDrawing.Mutate();
            Generations++;


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