using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GenArt.Classes;
using GenArt.Core.Classes;

namespace MpiLisa
{
    public static class NodeStarter
    {

        internal static void Run(MPI.Intracommunicator comm)
        {
            var info = new JobInfo
                           {
                               //hack, let every node init their own job data
                               Settings = new Settings(), //should be shared and sent to all workers later
                               SourceImage = GetSourceImage() //should also be sent to all workers later
                           };

            int nodes = comm.Size;
            int rank = comm.Rank;

            //setup partition size
            int partitionSize = info.SourceImage.Height / nodes;
            int partitionY = rank * partitionSize;

            if (comm.Rank == 0)
            {
                var instance = new MasterInstance(0, partitionY, partitionSize, info);
                instance.WorkLoop(comm);
            }
            else
            {
                var instance = new WorkerInstance(0, partitionY, partitionSize, info);
                instance.WorkLoop(comm);
            }
        }

        private static SourceImage GetSourceImage()
        {
            Bitmap bitmap = Properties.Resources.MonaLisa;
            var image = new SourceImage
                            {
                                Pixels = SetupSourceColorMatrix(bitmap),
                                Width = bitmap.Width,
                                Height = bitmap.Height
                            };

            return image;
        }

        //converts the source image to a Color[,] for faster lookup
        private static Pixel[] SetupSourceColorMatrix(Bitmap sourceBitmap)
        {
            var sourceColors = new Pixel[sourceBitmap.Width * sourceBitmap.Height];

            if (sourceBitmap == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            for (int y = 0; y < sourceBitmap.Height; y++)
            {
                for (int x = 0; x < sourceBitmap.Width; x++)
                {
                    Color c = sourceBitmap.GetPixel(x, y);
                    sourceColors[y * sourceBitmap.Width + x] = c;
                }
            }

            return sourceColors;
        }
    }
}
