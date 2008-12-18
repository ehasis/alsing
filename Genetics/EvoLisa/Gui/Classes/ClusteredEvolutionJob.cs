using System.Collections.Generic;
using System.Threading;
using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class ClusteredEvolutionJob : IEvolutionJob
    {
        private readonly IList<ClusteredWorker> workers;
        private double currentErrorLevel = double.MaxValue;

        public ClusteredEvolutionJob(JobInfo info)
        {
            workers = new List<ClusteredWorker>();
            const int workerCount = 2;
            int partitionHeight = info.SourceImage.Height / workerCount;
            for (int i = 0; i < workerCount; i++)
            {
                var worker = new ClusteredWorker(1, i * partitionHeight, partitionHeight, info);
                workers.Add(worker);
            }

            foreach(ClusteredWorker worker in workers)
            {
                worker.StartWorking();
            }
        }

        public bool IsDirty { get; set; }

        #region IEvolutionJob Members

        public DnaDrawing GetDrawing()
        {
            return workers[0].GetCurrentDrawing();
        }

        public double GetNextErrorLevel()
        {

            int tailIndex = 0;

            while (true)
            {
                var results = new List<DnaPartitionResult>();

                foreach (ClusteredWorker worker in workers)
                {
                    DnaPartitionResult partitionResult = worker.GetNextResult();
                    results.Add(partitionResult);
                }

                double newErrorLevel = 0;
                foreach (DnaPartitionResult partitionResult in results)
                {
                    newErrorLevel += partitionResult.ErrorLevel;
                }

                if (newErrorLevel <= currentErrorLevel)
                {
                    currentErrorLevel = newErrorLevel;
                    int newSeed = Tools.GetRandomNumber(int.MinValue, int.MaxValue);
                    foreach (ClusteredWorker worker in workers)
                    {
                        worker.AcceptGoodDrawing(tailIndex, newSeed);
                    }
                    break;
                }

                tailIndex++;
              //  Thread.Sleep(10);
            }


            return currentErrorLevel;
        }

        #endregion
    }
}