using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GenArt.AST;

namespace GenArt.Classes
{
    public class ClusteredWorker
    {
        private Thread workerThread;
        private double currentErrorLevel;
        private DnaDrawing currentDrawing;
        public ClusteredWorker()
        {

        }

        internal void SetJob(DnaDrawing drawing, double errorLevel)
        {
            currentErrorLevel = errorLevel;
            currentDrawing = drawing;
        }

        private void WorkerThreadStart()
        {
            while(isRunning)
            {
                var newDrawing = currentDrawing.Clone();
                newDrawing.Mutate();
                if (newDrawing.IsDirty)
                {
                    var newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);
                    if (newErrorLevel < currentErrorLevel)
                    {
                        currentDrawing = newDrawing;
                        currentErrorLevel = newErrorLevel;
                    }
                }
            }
        }

        private bool isRunning;
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
