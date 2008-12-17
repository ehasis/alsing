using System.Collections.Generic;
using System.Threading;
using GenArt.AST;

namespace GenArt.Classes
{
    public class ClusteredWorker
    {
        private readonly JobInfo info;
        private readonly int partitionHeight;
        private readonly int partitionY;
        private readonly object syncRoot = new object();
        private readonly Queue<DnaPartitionResult> workerTail;
        private readonly List<DnaPartitionResult> workerUsedTail;
        private bool hasNewParent;
        private bool isRunning;
        private DnaDrawing parentDrawing;
        private int randomSeed;
        private Thread workerThread;


        public ClusteredWorker(int randomSeed, int partitionY, int partitionHeight, JobInfo info)
        {
            this.randomSeed = randomSeed;
            this.partitionY = partitionY;
            this.partitionHeight = partitionHeight;
            this.info = info;
            workerTail = new Queue<DnaPartitionResult>();
            workerUsedTail = new List<DnaPartitionResult>();
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
                lock (syncRoot)
                {
                    if (hasNewParent)
                    {
                        Tools.InitRandom(randomSeed);
                        hasNewParent = false;
                    }

                    DnaDrawing newDrawing = GetMutatedSeedSyncedDrawing();

                    double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, info.SourceImage,
                                                                               partitionY, partitionHeight);

                    var result = new DnaPartitionResult
                                     {
                                         Drawing = newDrawing,
                                         ErrorLevel = newErrorLevel,
                                     };


                    workerTail.Enqueue(result);
                }
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
            Tools.InitRandom(randomSeed);
            parentDrawing = new DnaDrawing
                                {
                                    Polygons = new List<DnaPolygon>(),
                                };
        }

        public DnaPartitionResult GetNextResult()
        {
            while (workerTail.Count == 0)
                Thread.Sleep(2);

            lock (syncRoot)
            {
                DnaPartitionResult result = workerTail.Dequeue();
                workerUsedTail.Add(result);
                return result;
            }
        }

        public void AcceptGoodDrawing(int tailIndex, int newSeed)
        {
            lock (syncRoot)
            {
                DnaPartitionResult result = workerUsedTail[tailIndex];
                parentDrawing = result.Drawing.Clone();
                randomSeed = newSeed;
                //Tools.InitRandom(randomSeed); //must be done in the correct thread
                hasNewParent = true;
                workerUsedTail.Clear();
                workerTail.Clear();
            }
        }

        public DnaDrawing GetCurrentDrawing()
        {
            return parentDrawing;
        }
    }
}