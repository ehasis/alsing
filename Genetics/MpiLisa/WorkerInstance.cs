using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Classes;
using MPI;
using MpiLisa.DataContracts;
using System.Drawing;

namespace MpiLisa
{
    internal class WorkerInstance
    {
        protected readonly JobInfo info;
        protected int generation;
        protected DnaDrawing parentDrawing;
        protected long parentErrorLevel = long.MaxValue;
        protected int randomSeed;
        protected int repeats = 1;

        private DateTime startTime;

        private Bitmap fgImage;
        private Bitmap bgImage;

        internal WorkerInstance(int randomSeed, JobInfo info)
        {
            this.randomSeed = randomSeed;
            this.info = info;
            info.InitRandom(randomSeed);
            parentDrawing = new DnaDrawing
                                {
                                    Polygons = new List<DnaPolygon>(),
                                };
        }

        internal virtual void WorkLoop(Intracommunicator comm)
        {
            startTime = DateTime.Now;
            Console.WriteLine("Starting worker {0} ", comm.Rank);
            var timer = new Stopwatch();


            int partitionHeight = info.SourceImage.Height / comm.Size;
            int partitionY = partitionHeight * comm.Rank;

            for (int i = 0; i < info.Settings.PolygonsMax; i++)
            {
                var polygon = new DnaPolygon();
                parentDrawing.Polygons.Add(polygon);
                polygon.Init(parentDrawing, info);

                //foreach (DnaPoint p in polygon.Points)
                //{
                //    p.Y = partitionY + partitionHeight / 2;
                //}
            }

            int waitTimer = 0;
            while (true)
            {
                if (comm.Rank == 0)
                    NotifyProgress(generation);

                timer.Reset();
                timer.Start();

                while (timer.ElapsedMilliseconds < waitTimer)
                {                   
                    
                    var currentDrawing = parentDrawing.GetMutatedChild(info);
                    var currentErrorLevel = GetFitnessForDrawing(currentDrawing);

                    if (currentErrorLevel < parentErrorLevel)
                    {
                        parentErrorLevel = currentErrorLevel;
                        parentDrawing = currentDrawing;
                    }
                }

                if (waitTimer < 6000)
                    waitTimer += 500;

                timer.Stop();

                generation++;               

                var drawingInfo = new MpiWorkerDrawingInfo
                                      {
                                          Drawing = parentDrawing
                                      };


            //    Stopwatch swSync = new Stopwatch();
            //    swSync.Start();
                MpiWorkerDrawingInfo[] allResults = comm.Allgather(drawingInfo);

                if (bgImage != null)
                    bgImage.Dispose();

                bgImage = new Bitmap(info.SourceImage.Width, info.SourceImage.Height,
                                         PixelFormat.Format32bppArgb);
                var bgGraphics = Graphics.FromImage(bgImage);

                bgGraphics.Clear(Color.Black);
                bgGraphics.SmoothingMode = SmoothingMode.HighQuality;
                for (int i = 0; i < comm.Rank; i++)
                {
                    Renderer.Render(allResults[i].Drawing, bgGraphics, 1);
                }

                if (fgImage != null)
                    fgImage.Dispose();

                fgImage = new Bitmap(info.SourceImage.Width, info.SourceImage.Height,
                                         PixelFormat.Format32bppArgb);
                var fgGraphics = Graphics.FromImage(fgImage);

                fgGraphics.Clear(Color.Transparent);
                fgGraphics.SmoothingMode = SmoothingMode.HighQuality;
                for (int i = comm.Rank + 1; i < comm.Size; i++)
                {
                    Renderer.Render(allResults[i].Drawing, fgGraphics, 1);
                }

                fgGraphics.Dispose();
                bgGraphics.Dispose();
               

                //recalc the new parent error level
                parentErrorLevel = GetFitnessForDrawing(parentDrawing);
            //    swSync.Stop();

            //    Console.WriteLine("sync {0}", swSync.Elapsed);
            }
        }

        protected long GetFitnessForDrawing(DnaDrawing drawing)
        {
            return FitnessCalculator.GetDrawingFitness(drawing, info.SourceImage,bgImage,fgImage);
        }

        protected void NotifyProgress(int generations)
        {

            TimeSpan elapsedTime = DateTime.Now - startTime;

            Console.WriteLine("ErrorLevel = {0}, Elapsed = {1}", parentErrorLevel, elapsedTime);


            using (var bmp = new Bitmap(info.SourceImage.Width, info.SourceImage.Height, PixelFormat.Format24bppRgb)
                )
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                if (bgImage != null)
                    g.DrawImage(bgImage, 0, 0);

                Renderer.Render(parentDrawing, g, 1);

                if (fgImage != null)
                    g.DrawImage(fgImage, 0, 0);

                bmp.Save(@"C:\Render2\" + generations + ".gif");
            }
            // startTime = DateTime.Now;

        }
    }
}