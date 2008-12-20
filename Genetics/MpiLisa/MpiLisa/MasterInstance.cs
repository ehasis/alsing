using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

            Console.WriteLine("Starting server (rank {0}) , Partition Y {1} , Partition Height {2}", comm.Rank,
                              partitionY, partitionHeight);

            int generations = 0;
            DateTime startTime = DateTime.Now;
            while (true)
            {
                //ignorant debugging of MPI, I just output console text and images in a folder
                NotifyProgress(generations, startTime, currentTotalErrorLevel);

                double newErrorLevel = GetFitnessForNewChild();

                //send out own progress and receive all nodes total progress
                double newTotalErrorLevel = SenderWorkerResponse(comm, newErrorLevel);
                
                //if the new total errir is better, then flag this as a keeper
                bool accepted = EvaluateIfNewTotalFitnessIsBetter(currentTotalErrorLevel, newTotalErrorLevel);

                MpiMasterResponse masterCommand = GetMasterCommand(accepted);

                ReceiveMasterCommand(comm, masterCommand);

                //if the new total drawing is better, store its fitness
                if (accepted)
                {
                    currentTotalErrorLevel = newTotalErrorLevel;
                }

                generations++;
            }
        }

        private MpiMasterResponse GetMasterCommand(bool accepted)
        {
            return new MpiMasterResponse
                       {
                           Accepted = accepted,
                           NewRandomSeed = random.Next(int.MinValue, int.MaxValue),
                       };
        }

        private bool EvaluateIfNewTotalFitnessIsBetter(double currentTotalErrorLevel, double newTotalErrorLevel)
        {
            return (newTotalErrorLevel <= currentTotalErrorLevel);
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