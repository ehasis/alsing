using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class LayeredEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;
        public int LayerCount { get; set; }
        private readonly IList<LayeredWorker> workers;
        private readonly SourceImage sourceImage;

        public LayeredEvolutionJob(SourceImage sourceImage,int layerCount)
        {
            this.sourceImage = sourceImage;
            LayerCount = layerCount;
            workers = new List<LayeredWorker>();
            int range = 255/LayerCount;
            int workerMin = 0;
            for(int i=0;i<LayerCount;i++)
            {
                var background = GetIntensityMap(sourceImage, workerMin, range);

                var worker = new LayeredWorker (sourceImage)
                {
                    MinIntensity = workerMin, 
                    MaxIntensity = (workerMin + range),
                    Background = background,
                };

                workers.Add(worker);
                workerMin += range;
            }
        }

        private static Bitmap GetIntensityMap(SourceImage sourceImage, int workerMin, int range)
        {
            var background = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);

            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Color c = sourceImage.Colors[x, y];
                    int intensity = (c.R + c.G + c.B)/3;

                    if (intensity >= workerMin && intensity <= workerMin + range)
                    {
                        //remove pixels that are supposed to be drawn by this layer
                        background.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        //colors outside the layers range is included in the background for this layer
                        background.SetPixel(x, y, c);
                    }
                }
            }

            return background;
        }

        #region IEvolutionJob Members

        public DnaDrawing GetDrawing()
        {
            return currentDrawing;
        }

        public double GetNextErrorLevel()
        {
            var drawing = new DnaDrawing();
            drawing.SourceImage = sourceImage;
            drawing.Polygons = new List<DnaPolygon>();
            foreach(LayeredWorker worker in workers)
            {
                DnaDrawing workerDrawing = worker.GetDrawing();
                drawing.Polygons.AddRange(workerDrawing.Clone().Polygons);
                //drawing = worker.GetDrawing();
            }
            currentDrawing = drawing;

            currentErrorLevel = FitnessCalculator.GetDrawingFitness(drawing, sourceImage);
            return currentErrorLevel;
        }

        #endregion
    }
}