using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;
        private Settings settings;

        public DefaultEvolutionJob(SourceImage sourceImage, Settings settings)
            : this(sourceImage, null, settings)
        {
        }

        public DefaultEvolutionJob(SourceImage sourceImage, DnaDrawing drawing, Settings settings)
        {
            this.settings = settings;

            if (drawing == null)
                drawing = GetNewInitializedDrawing(settings);
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

            while (newDrawing.IsDirty == false)
            {
                newDrawing.Mutate(settings);
                Generations++;
            }

            double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);

            if (newErrorLevel <= currentErrorLevel)
            {
                currentDrawing = newDrawing;
                currentErrorLevel = newErrorLevel;
            }

            return newErrorLevel;

        }

        #endregion

        private static DnaDrawing GetNewInitializedDrawing(Settings settings)
        {
            var drawing = new DnaDrawing();
            drawing.Init(settings);
            return drawing;
        }
    }
}