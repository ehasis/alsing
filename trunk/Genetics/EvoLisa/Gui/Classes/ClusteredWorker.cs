using System.Collections.Generic;
using System.Threading;
using GenArt.AST;

namespace GenArt.Classes
{
    public class ClusteredWorker
    {
        private class WorkerData
        {
            public Queue<DnaPartitionResult> workerTail;
            public List<DnaPartitionResult> workerUsedTail;
            public int randomSeed;
            public bool hasNewParent;
        }


        private readonly JobInfo info;
        private readonly int partitionHeight;
        private readonly int partitionY;
        private readonly object syncRoot = new object();

        private WorkerData data;

        
        private bool isRunning;
        private DnaDrawing parentDrawing;

        private Thread workerThread;


        public ClusteredWorker(int randomSeed, int partitionY, int partitionHeight, JobInfo info)
        {
            data = new WorkerData
                       {
                           randomSeed = randomSeed,
                           workerTail = new Queue<DnaPartitionResult>(),
                           workerUsedTail = new List<DnaPartitionResult>(),
                       };
 
            this.partitionY = partitionY;
            this.partitionHeight = partitionHeight;
            this.info = info;

        }

        public void StartWorking()
        {
            workerThread = new Thread(WorkerThreadStart)
                               {
                                   IsBackground = true,
                                   Priority = ThreadPriority.BelowNormal
                               };

            isRunning = true;
            workerThread.Start();
        }

        private void WorkerThreadStart()
        {
            Initialize();

            while (isRunning)
            {

                if (data.hasNewParent)
                {
                    Tools.InitRandom(data.randomSeed);
                    data.hasNewParent = false;
                }


                DnaDrawing newDrawing = GetMutatedSeedSyncedDrawing();

                double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, info.SourceImage,
                                                                           partitionY, partitionHeight);

                var result = new DnaPartitionResult
                                 {
                                     Drawing = newDrawing,
                                     ErrorLevel = newErrorLevel,
                                 };


                data.workerTail.Enqueue(result);

            }
        }

        private DnaDrawing GetMutatedSeedSyncedDrawing()
        {
            DnaDrawing newDrawing = parentDrawing.Clone();

            newDrawing.Mutate(info);
            return newDrawing;
        }

        private void Initialize()
        {
            Tools.InitRandom(data.randomSeed);
            parentDrawing = new DnaDrawing
                                {
                                    Polygons = new List<DnaPolygon>(),
                                };
        }

        public DnaPartitionResult GetNextResult()
        {
            while (data.workerTail.Count == 0)
                Thread.Sleep(2); //only happens at startup and on rare occasions

            DnaPartitionResult result = data.workerTail.Dequeue();
            data.workerUsedTail.Add(result);
            return result;
        }

        public void AcceptGoodDrawing(int tailIndex, int newSeed)
        {
            DnaPartitionResult result = data.workerUsedTail[tailIndex];
            parentDrawing = result.Drawing.Clone();
            WorkerData newData = new WorkerData
                                     {
                                         randomSeed = newSeed,
                                         hasNewParent = true,
                                         workerUsedTail = new List<DnaPartitionResult>(),
                                         workerTail = new Queue<DnaPartitionResult>(),

                                     };
            data = newData;
        }

        public DnaDrawing GetCurrentDrawing()
        {
            return parentDrawing;
        }
    }
}