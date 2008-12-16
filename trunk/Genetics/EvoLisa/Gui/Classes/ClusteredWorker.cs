using System.Threading;
using GenArt.AST;

namespace GenArt.Classes
{
    public class ClusteredWorker
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;
        private bool isRunning;
        private Thread workerThread;
        private Settings settings;

        internal void SetJob(DnaDrawing drawing, double errorLevel, Settings settings)
        {
            currentErrorLevel = errorLevel;
            currentDrawing = drawing;
            this.settings = settings;
        }

        private void WorkerThreadStart()
        {
            while (isRunning)
            {
                DnaDrawing newDrawing = currentDrawing.Clone();

                newDrawing.Mutate(settings);

                double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);
                if (newErrorLevel < currentErrorLevel)
                {
                    currentDrawing = newDrawing;
                    currentErrorLevel = newErrorLevel;
                }
            }
        }

        internal void StartWorking()
        {
            workerThread = new Thread(WorkerThreadStart)
                               {
                                   IsBackground = true,
                                   Priority = ThreadPriority.Normal
                               };
            isRunning = true;
            workerThread.Start();
        }

        internal void StopWorking()
        {
            isRunning = false;
            workerThread.Join();
        }

        internal double GetErrorLevel()
        {
            return currentErrorLevel;
        }

        internal DnaDrawing GetDrawing()
        {
            return currentDrawing;
        }
    }
}