using System.Collections.Generic;
using System.Threading;
using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class ClusteredEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;
        private readonly IList<ClusteredWorker> workers;

        public ClusteredEvolutionJob(SourceImage sourceImage)
        {
            workers = new List<ClusteredWorker>();
            for (int i = 0; i < 2;i++ )
            {
                var worker = new ClusteredWorker();
                workers.Add(worker);
            }
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
            foreach(var worker in workers)
            {
                worker.SetJob(currentDrawing,currentErrorLevel);
            }

            foreach (var worker in workers)
            {
                worker.StartWorking();
            }

            Thread.Sleep(1);

            foreach (var worker in workers)
            {
                worker.StopWorking();
            }

            IsDirty = false;
            foreach (var worker in workers)
            {
                double workerError = worker.GetErrorLevel();

                if (workerError < currentErrorLevel)
                {
                    currentDrawing = worker.GetDrawing();
                    currentErrorLevel = workerError;
                    IsDirty = true;
                }
            }

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