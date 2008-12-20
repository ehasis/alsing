using System;
using System.Collections.Generic;
using GenArt.AST;
using GenArt.Classes;
using MPI;
using MpiLisa.DataContracts;

namespace MpiLisa
{
    public class WorkerInstance
    {
        protected readonly JobInfo info;
        protected readonly int partitionHeight;
        protected readonly int partitionY;
        protected DnaDrawing currentDrawing;
        protected DnaDrawing parentDrawing;
        protected int randomSeed;

        public WorkerInstance(int randomSeed, int partitionY, int partitionHeight, JobInfo info)
        {
            this.randomSeed = randomSeed;
            this.partitionY = partitionY;
            this.partitionHeight = partitionHeight;
            this.info = info;
            info.InitRandom(randomSeed);
            parentDrawing = new DnaDrawing
                                {
                                    Polygons = new List<DnaPolygon>(),
                                };
        }

        public virtual void WorkLoop(Intracommunicator comm)
        {
            Console.WriteLine("Starting worker {0} , Partition Y {1} , Partition Height {2}", comm.Rank,partitionY,partitionHeight);
            while (true)
            {
                currentDrawing = GetMutatedSeedSyncedDrawing();

                double newErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, info.SourceImage,
                                                                           partitionY, partitionHeight);

                SenderWorkerResponse(comm, currentDrawing, newErrorLevel);

                ReceiveMasterCommand(comm, null);
            }
        }

        protected void SenderWorkerResponse(Intracommunicator comm, DnaDrawing newDrawing, double newErrorLevel)
        {
            var result = new MpiWorkerResponse
                             {
                                 ErrorLevel = newErrorLevel,
                             };

            currentDrawing = newDrawing;

            comm.Gather(result, 0);
            //comm.Send(result, 0, 0);
        }

        protected void ReceiveMasterCommand(Intracommunicator comm, MpiMasterResponse masterCommand)
        {
            comm.Broadcast(ref masterCommand, 0); //receive data from master

            if (masterCommand.Accepted)
            {
                randomSeed = masterCommand.NewRandomSeed;
                info.InitRandom(randomSeed);
                parentDrawing = currentDrawing;
            }
        }

        protected DnaDrawing GetMutatedSeedSyncedDrawing()
        {
            DnaDrawing newDrawing = parentDrawing.Clone();

            newDrawing.Mutate(info);
            return newDrawing;
        }
    }
}