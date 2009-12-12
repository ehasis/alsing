using System;
using System.Drawing;
using GenArt.Classes;
using GenArt.Core.Classes;
using MPI;
using MpiLisa.Properties;
using Settings=GenArt.Classes.Settings;

namespace MpiLisa
{
    internal static class NodeStarter
    {
        internal static void Run(Intracommunicator comm)
        {
            var info = new JobInfo
                           {
                               //hack, let every node init their own job data
                               Settings = new Settings(),
                               //should be shared and sent to all workers later
                               SourceImage = GetSourceImage() //should also be sent to all workers later
                           };

            info.Settings.PolygonsMax = 50/comm.Size;
            var instance = new WorkerInstance(comm.Rank*10, info);
            instance.WorkLoop(comm);
        }

        private static SourceImage GetSourceImage()
        {
            Bitmap bitmap = Resources.MonaLisa;
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
            var sourceColors = new Pixel[sourceBitmap.Width*sourceBitmap.Height];

            if (sourceBitmap == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            for (int y = 0; y < sourceBitmap.Height; y++)
            {
                for (int x = 0; x < sourceBitmap.Width; x++)
                {
                    Color c = sourceBitmap.GetPixel(x, y);
                    sourceColors[y*sourceBitmap.Width + x] = c;
                }
            }

            return sourceColors;
        }
    }
}