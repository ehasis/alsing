using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;
        private JobInfo info;

        public DefaultEvolutionJob(JobInfo info)
            : this(null, info)
        {
        }

        public DefaultEvolutionJob(DnaDrawing drawing, JobInfo info)
        {
            this.info = info;

            if (drawing == null)
                drawing = GetNewInitializedDrawing(info);
            lock (drawing)
            {
                currentDrawing = drawing.Clone();
            }

            currentErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, info.SourceImage);
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
                newDrawing.Mutate(info);
                Generations++;
            }

            double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, info.SourceImage);

            if (newErrorLevel <= currentErrorLevel)
            {
                currentDrawing = newDrawing;
                currentErrorLevel = newErrorLevel;
            }

            return newErrorLevel;

        }

        #endregion

        private static DnaDrawing GetNewInitializedDrawing(JobInfo info)
        {
            var drawing = new DnaDrawing();
            drawing.Init(info);
            return drawing;
        }
    }
}