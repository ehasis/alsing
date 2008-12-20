using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Classes;
using MPI;
using MpiLisa.DataContracts;

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
                NotifyProgress(generations, startTime, currentTotalErrorLevel);


                currentDrawing = GetMutatedSeedSyncedDrawing();

                double newErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, info.SourceImage,
                                                                           partitionY, partitionHeight);

                double newTotalErrorLevel = newErrorLevel;

                workers.ForEach(worker => newTotalErrorLevel += comm.Receive<MpiWorkerResponse>(worker, 0).ErrorLevel);

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
                Tools.InitRandom(masterCommand.NewRandomSeed);
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