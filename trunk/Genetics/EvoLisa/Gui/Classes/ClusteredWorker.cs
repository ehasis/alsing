using System;
using System.Collections.Generic;
using System.Threading;
using GenArt.AST;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    public class ClusteredWorker
    {
        private DnaDrawing parentDrawing;
        private bool isRunning;
        private Thread workerThread;
        private Settings settings;
        private SourceImage sourceImage;
        private int randomSeed;
        private readonly int partitionY;
        private readonly int partitionHeight = 0;
        private readonly Queue<DnaPartitionResult> workerTail;
        private readonly List<DnaPartitionResult> workerUsedTail;
        private readonly object syncRoot = new object();
        private bool hasNewParent = false;

        public ClusteredWorker(int randomSeed,int partitionY,int partitionHeight,SourceImage sourceImage,Settings settings)
        {
            this.randomSeed = randomSeed;
            this.partitionY = partitionY;
            this.partitionHeight = partitionHeight;
            this.settings = settings;
            this.sourceImage = sourceImage;
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
                    if(hasNewParent)
                    {
                        Tools.InitRandom(randomSeed);
                        hasNewParent = false;
                    }

                    DnaDrawing newDrawing = GetMutatedSeedSyncedDrawing();

                    double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage,
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

            newDrawing.Mutate(settings);
            return newDrawing;
        }

        private void Initialize()
        {
            Tools.InitRandom(randomSeed);
            parentDrawing = new DnaDrawing()
                                {
                                    Polygons = new List<DnaPolygon>(),
                                    SourceImage = sourceImage,
                                };
        }

        public DnaPartitionResult GetNextResult()
        {
            while (workerTail.Count == 0)
                Thread.Sleep(2);

            lock(syncRoot)
            {
                var result = workerTail.Dequeue();
                workerUsedTail.Add(result);
                return result;
            }
        }

        public void AcceptGoodDrawing(int tailIndex,int newSeed)
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