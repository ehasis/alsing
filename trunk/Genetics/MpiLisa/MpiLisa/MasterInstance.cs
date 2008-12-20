using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Classes;
using MPI;
using MpiLisa.DataContracts;
using System.Linq;

namespace MpiLisa
{
    public class MasterInstance : WorkerInstance
    {
        private readonly Random random = new Random();

        public MasterInstance(int randomSeed, int partitionY, int partitionHeight, JobInfo info)
            : base(randomSeed, partitionY, partitionHeight, info)
        {
        }

        public override void WorkLoop(Intracommunicator comm)
        {
            double currentTotalErrorLevel = int.MaxValue;
            var workers = new List<int>();
            for (int i = 1; i < comm.Size; i++)
            {
                workers.Add(i);
            }


            Console.WriteLine("Starting server (rank {0}) , Partition Y {1} , Partition Height {2}", comm.Rank,
                              partitionY, partitionHeight);
            int generations = 0;
            DateTime startTime = DateTime.Now;
            while (true)
            {
                //ignorant debugging of MPI, I just output console text and images in a folder
                NotifyProgress(generations, startTime, currentTotalErrorLevel);

                //fetch a mutated child of the current parent
                currentDrawing = GetMutatedSeedSyncedDrawing();

                double newErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, info.SourceImage,
                                                                           partitionY, partitionHeight);
                //start by posting the masters own partition
                var response = new MpiWorkerResponse
                {
                   ErrorLevel = newErrorLevel,
                };

                //fetch the partition results from each node
                var allResponses = comm.Gather(response, 0);

                //sum up the partitioned error levels
                double newTotalErrorLevel = allResponses.Sum(r => r.ErrorLevel);


                //if the new total errir is better, then flag this as a keeper
                bool accepted = (newTotalErrorLevel <= currentTotalErrorLevel);

                currentTotalErrorLevel = SendMasterCommand(comm, currentTotalErrorLevel, newTotalErrorLevel, accepted);

                generations++;
            }
        }

        private double SendMasterCommand(Intracommunicator comm, double currentErrorLevel, double newTotalErrorLevel,
                                         bool accepted)
        {
            var masterCommand = new MpiMasterResponse
                                    {
                                        Accepted = accepted,
                                        NewRandomSeed = random.Next(int.MinValue, int.MaxValue),
                                    };

            comm.Broadcast(ref masterCommand, 0);

            if (accepted)
            {
                currentErrorLevel = newTotalErrorLevel;
                parentDrawing = currentDrawing;
                info.InitRandom(masterCommand.NewRandomSeed);
            }
            return currentErrorLevel;
        }

        private void NotifyProgress(int generations, DateTime startTime, double currentErrorLevel)
        {
            if (generations%1000 == 1)
            {
                TimeSpan elapsedTime = DateTime.Now - startTime;

                Console.WriteLine("ErrorLevel = {0}, Elapsed = {1}", currentErrorLevel, elapsedTime);


                using (var bmp = new Bitmap(info.SourceImage.Width, info.SourceImage.Height, PixelFormat.Format24bppRgb)
                    )
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    Renderer.Render(parentDrawing, g, 1);
                    bmp.Save(@"C:\Render2\" + generations + ".gif");
                }
            }
        }
    }
}