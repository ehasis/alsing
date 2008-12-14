using System;
using System.Collections.Generic;
using System.Drawing;
using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class LayeredEvolutionJob : IEvolutionJob
    {
        private readonly SourceImage sourceImage;
        private readonly IList<LayeredWorker> workers;
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;

        public LayeredEvolutionJob(SourceImage sourceImage, int layerCount)
        {
            this.sourceImage = sourceImage;
            LayerCount = layerCount;
            workers = new List<LayeredWorker>();
            int range = 255/LayerCount;
            int workerMin = 0;
            for (int i = 0; i < LayerCount; i++)
            {
                SourceImage newSourceImage = GetIntensityMap(sourceImage, workerMin, range);

                var worker = new LayeredWorker(newSourceImage)
                                 {
                                     MinIntensity = workerMin,
                                     MaxIntensity = (workerMin + range),
                                 };

                workers.Add(worker);
                workerMin += range;
            }
        }

        public int LayerCount { get; set; }

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
            int i = 0;
            foreach (LayeredWorker worker in workers)
            {
                i++;

                if (i == 1)
                    continue;

                DnaDrawing workerDrawing = worker.GetDrawing();
                drawing.Polygons.AddRange(workerDrawing.Clone().Polygons);
                //drawing = worker.GetDrawing();
            }

            currentDrawing = drawing;

            currentErrorLevel = FitnessCalculator.GetDrawingFitness(drawing, sourceImage);
            return currentErrorLevel;
        }

        #endregion

        private static SourceImage GetIntensityMap(SourceImage sourceImage, int workerMin, int range)
        {
            var newSourceImage = new SourceImage();
            newSourceImage.Height = sourceImage.Height;
            newSourceImage.Width = sourceImage.Width;
            newSourceImage.Pixels = new Pixel[newSourceImage.Width*newSourceImage.Height];
            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Color c = sourceImage.Pixel(x, y);
                    var intensity = (int) (c.GetBrightness()*255);

                    if (intensity >= workerMin && intensity <= workerMin + range)
                    {
											newSourceImage.setPixel( x, y, c );
                    }
                    else
                    {
											newSourceImage.setPixel( x, y, Color.Black );
                    }
                }
            }

            return newSourceImage;
        }

        public double GetColorDist(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            double dist = Math.Sqrt(r*r + g*g + b*b);
            return dist;
        }
    }
}
